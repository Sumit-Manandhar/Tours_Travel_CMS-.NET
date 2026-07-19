using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.DataAccess.Context;
using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models;
using Travels.Models.ViewModels;

namespace Travels.DataAccess.Repository
{
    public class SubdestinationsRepository : Repository<Subdestinations>, ISubdestinationsRepository
    {
        private readonly ApplicationDbContext _db;

        public SubdestinationsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Add(SaveSubdestinationModel obj)
        {
            var subdestinationId = obj.Id;
            if (obj.Id > 0)
            {
                await Update(obj);
            }
            else
            {
                var destination = new Subdestinations
                {
                    Name = obj.SubdestinationName,
                    DestinationId = obj.DestinationId,
                    IsDeleted = obj.IsDeleted,
                    IsActive = obj.IsActive,
                    IsPublished = obj.IsPublished,
                    CreatedDate = DateTime.UtcNow,
                    CityId = obj.CityId,
                };
                _db.Add(destination);
                _db.SaveChanges();
                subdestinationId = destination.Id;
                var destinatindata = new SubdestinationData
                {
                    Description = obj.Description,
                    ShortDescription = obj.ShortDesc,
                    TopDestination = obj.TopDestination,
                    SubdestinationId = subdestinationId,

                };
                await _db.AddAsync(destinatindata);
            }
            if (!string.IsNullOrEmpty(obj.DisplayImageUrl))
            {
                var primaryImages = _db.SubdestinationImage.Where(a => a.SubdestinationId == subdestinationId && a.IsPrimary).ToArray();
                if (primaryImages is not null && primaryImages.Count() > 0)
                {
                    if (primaryImages.Count() > 1)
                    {
                        var bannerList = primaryImages.ToList();

                        // Remove all but the last item
                        bannerList.Remove(primaryImages[primaryImages.Count() - 1]);
                        _db.RemoveRange(bannerList);

                    }
                    var banner = primaryImages[primaryImages.Count() - 1];
                    banner.ImageUrl = obj.DisplayImageUrl;
                    _db.Update(banner);
                }
                else
                {
                    var banner = new SubdestinationImage()
                    {
                        SubdestinationId = subdestinationId,
                        ImageUrl = obj.DisplayImageUrl,
                        IsPrimary = true,
                        IsBanner = false
                    };

                    await _db.AddAsync(banner);
                }
            }
            if (!string.IsNullOrEmpty(obj.BannerImageUrl))
            {
                var bannerImages = _db.SubdestinationImage.Where(a => a.SubdestinationId == subdestinationId && a.IsBanner).ToArray();
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
                    var banner = new SubdestinationImage()
                    {
                        SubdestinationId = subdestinationId,
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
                    var img = new SubdestinationImage()
                    {
                        SubdestinationId = subdestinationId,
                        ImageUrl = a,
                        IsPrimary = false,
                        IsBanner = false
                    };


                    await _db.AddAsync(img);
                }
            }

            if(obj.toDeleteImages is not null && obj.toDeleteImages.Any())
            {
                _db.SubdestinationImage.RemoveRange(_db.SubdestinationImage.Where(a => obj.toDeleteImages.Contains(a.Id)));
            }
            await _db.SaveChangesAsync();
        }

        public SubdestinationDetailsModel GetById(int Id)
        {
            var destination = _db.Subdestinations.Include(d => d.SubdestinationImage)
                .Include(d => d.City)
                .Include(d => d.Destination).ThenInclude(dd=> dd.DestinationData) // Include DestinationData
                .Include(d => d.SubestinationData)
                .AsNoTracking()
                .FirstOrDefault(d => d.Id == Id);

            var otherDestinations = _db.Subdestinations.Where(x => x.Id != Id && (x.IsPublished &&x.IsActive && !x.IsDeleted) && x.Destination.DestinationData.CountryId == destination.Destination.DestinationData.CountryId).Select(sd => new SubDestinationListViewModel
            {
                Id = sd.Id,
                Name = sd.Name,
                ImageURL = sd.SubdestinationImage.Where(img => img.IsPrimary).Select(img => img.ImageUrl).FirstOrDefault()
            }).ToList();

            var data = new SubdestinationDetailsModel()
            {
                Id = destination.Id,
                DestinationId = destination.DestinationId,
                DestinationName = destination.Destination.Name,
                CityId = destination.CityId,
                IsActive = destination.IsActive,
                IsDeleted = destination.IsDeleted,
                IsPublished = destination.IsPublished,
                Name = destination.Name,
                Cityname = destination.City.CityName,
                ShortDescription = destination.SubestinationData.ShortDescription ?? "",
                Description = destination.SubestinationData.Description ?? "",
                TopDestination = destination.SubestinationData.TopDestination ?? "",
                Images = destination.SubdestinationImage.Select(im => new ImageViewModel
                {
                    Id = im.Id,
                    ImageUrl = im.ImageUrl,
                    IsPrimary = im.IsPrimary,
                    IsBanner = im.IsBanner
                }).ToList(),
                OtherDestinations = otherDestinations
            };
            return data;
        }

        public async Task Update(SaveSubdestinationModel obj)
        {
            var subdestination = _db.Subdestinations.FirstOrDefault(c => c.Id == obj.Id);
            if (subdestination is null)
                throw new KeyNotFoundException("invalid Id");
            subdestination.Id = obj.Id;
            subdestination.IsActive = obj.IsActive;
            subdestination.IsDeleted = obj.IsDeleted;
            subdestination.IsPublished = obj.IsPublished;
            subdestination.Name = obj.SubdestinationName;
            subdestination.CityId = obj.CityId;
            _db.Subdestinations.Update(subdestination);
            var desc = _db.SubdestinationData.FirstOrDefault(a => a.SubdestinationId == obj.Id);
            if (desc is not null)
            {
                desc.ShortDescription = obj.ShortDesc;
                desc.Description = obj.Description;
                desc.TopDestination = obj.TopDestination;
                _db.SubdestinationData.Update(desc);
            }
            await _db.SaveChangesAsync();

        }
        public async Task OnOffDelete(int SubdestinationId)
        {
            var obj = _db.Subdestinations.FirstOrDefault(a => a.Id == SubdestinationId);
            if (obj is null)
                throw new KeyNotFoundException("invalid Id");
            obj.IsDeleted = !obj.IsDeleted;
            _db.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
