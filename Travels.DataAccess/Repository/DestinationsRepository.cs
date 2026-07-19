using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Context;
using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models;
using Travels.Models.ViewModels;

namespace Travels.DataAccess.Repository
{
    public class DestinationsRepository : Repository<Destination>, IDestinationsRepository
    {
        private readonly ApplicationDbContext _db;
        public DestinationsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> Add(SaveDestinationModel obj)
        {
            var destinationId = obj.Id;
            if (obj.Id > 0)
            {
                await Update(obj);
            }
            else
            {
                var destinatindata = new DestinationData
                {
                    CountryId = obj.CountryId,
                    Description = obj.Description,
                    ShortDescription = obj.ShortDesc,
                    Overview = obj.Overview,
                };
                var destination = new Destination
                {
                    Name = obj.DestinationName,
                    DestinationData = destinatindata,
                    IsDeleted = obj.IsDeleted,
                    IsActive = obj.IsActive,
                    IsPublished = obj.IsPublished,
                    CreatedDate = DateTime.UtcNow
                };
                await _db.AddAsync(destination);
                await _db.SaveChangesAsync();
                destinationId = destination.Id;
            }
            var imgs = new List<DestinationImage>();
            if (!string.IsNullOrEmpty(obj.DisplayImageUrl))
            {
                var disp = new DestinationImage()
                {
                    DestinationId = destinationId,
                    ImageUrl = obj.DisplayImageUrl,
                    IsPrimary = true,
                    IsBanner = false
                };
                await _db.AddAsync(disp);
            }
            if (!string.IsNullOrEmpty(obj.BannerImageUrl))
            {
                var bannerImages = _db.DestinationImages.Where(a => a.DestinationId == destinationId && a.IsBanner).ToArray();
                if (bannerImages is not null && bannerImages.Count() > 0)
                {
                    if (bannerImages.Count() > 1)
                    {
                        var bannerList = bannerImages.ToList();

                        // Remove all but the last item
                        bannerList.Remove(bannerImages[bannerImages.Count() - 1]);
                        _db.RemoveRange(bannerList);

                    }
                    var banner = bannerImages[bannerImages.Count() - 1];
                    banner.ImageUrl = obj.BannerImageUrl;
                    _db.Update(banner);
                }
                else
                {
                    var banner = new DestinationImage()
                    {
                        DestinationId = destinationId,
                        ImageUrl = obj.BannerImageUrl,
                        IsPrimary = false,
                        IsBanner = true
                    };
                    await _db.AddAsync(banner);
                }

            }

            if (obj.ImageUrls is not null && obj.ImageUrls.Count > 0)
            {
                foreach (var a in obj.ImageUrls)
                {
                    var img = new DestinationImage()
                    {
                        DestinationId = destinationId,
                        ImageUrl = a,
                        IsPrimary = false,
                        IsBanner = false
                    };


                    await _db.AddAsync(img);
                }

                //imgs.AddRange(obj.ImageUrls.Select(a => new DestinationImage()
                //{
                //    DestinationId = destinationId,
                //    ImageUrl = a,
                //    IsPrimary = false,
                //    IsBanner = false
                //}));
            }
            //await _db.DestinationImages.AddRangeAsync(imgs);

            await _db.SaveChangesAsync();
            return destinationId;
        }

        public DestinationDetailsModel GetById(int Id)
        {
            var destination = _db.Destinations.Include(d => d.Subdestination).ThenInclude(sd => sd.SubdestinationImage.Where(img => img.IsPrimary)) // Include Subdestination images
                                .Include(d => d.DestinationImages) // Include Destination images
                                .Include(d => d.DestinationData) // Include DestinationData
                                .Include(d => d.DestinationData.Country) // Include Country
                                .AsNoTracking()
                                .FirstOrDefault(d => d.Id == Id); // Fetch destination by Id

            if (destination == null)
                return new DestinationDetailsModel(); // Handle case where no destination is found

            // Projecting the result
            var result = new DestinationDetailsModel
            {
                Id = destination.Id,
                CountryId = destination.DestinationData.CountryId,
                IsActive = destination.IsActive,
                IsDeleted = destination.IsDeleted,
                IsPublished = destination.IsPublished,
                Name = destination.Name,
                CountryName = destination.DestinationData.Country.CountryName,
                ShortDescription = destination.DestinationData.ShortDescription ?? "",
                Description = destination.DestinationData.Description ?? "",
                Overview = destination.DestinationData.Overview ?? "",
                Images = destination.DestinationImages.Select(img => new ImageViewModel
                {
                    ImageUrl = img.ImageUrl,
                    IsBanner = img.IsBanner,
                    IsPrimary = img.IsPrimary
                }).ToList(),
                SubDestinations = destination.Subdestination.Where(x => x.IsPublished && x.IsActive && !x.IsDeleted).Select(sd => new SubDestinationListViewModel
                {
                    Id = sd.Id,
                    Name = sd.Name,
                    ImageURL = sd.SubdestinationImage.Where(img => img.IsPrimary).Select(img => img.ImageUrl).FirstOrDefault()
                }).ToList()
            };

            return result;
        }

        public async Task Update(SaveDestinationModel obj)
        {
            var destination = _db.Destinations.FirstOrDefault(c => c.Id == obj.Id);
            if (destination is null)
                throw new KeyNotFoundException("invalid Id");
            destination.Id = obj.Id;
            destination.IsActive = obj.IsActive;
            destination.IsDeleted = obj.IsDeleted;
            destination.IsPublished = obj.IsPublished;
            destination.Name = obj.DestinationName;
            _db.Destinations.Update(destination);
            var desc = _db.DestinationData.FirstOrDefault(a => a.DestinationId == obj.Id);
            if (desc is not null)
            {
                desc.ShortDescription = obj.ShortDesc;
                desc.Description = obj.Description;
                desc.Overview = obj.Overview;
                desc.CountryId = obj.CountryId;
                _db.DestinationData.Update(desc);
            }
            await _db.SaveChangesAsync();

        }

        public async Task OnOffDelete(int DestinationId)
        {
            var obj = _db.Destinations.FirstOrDefault(a => a.Id == DestinationId);
            if (obj is null)
                throw new KeyNotFoundException("invalid Id");
            obj.IsDeleted = !obj.IsDeleted;
            _db.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
