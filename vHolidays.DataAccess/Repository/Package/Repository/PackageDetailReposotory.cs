using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;
using vHolidays.Utility.Enumerations;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackageDetailReposotory : Repository<PackageDetail>, IPackageDetailReposotory
    {
        private readonly ApplicationDbContext _db;
        public PackageDetailReposotory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(PackageDetailAddUpdateModel model)
        {
            try
            {
                PackageDetail data = new()
                {
                    Id = model.Id,
                    Code = model.Code,
                    Name = model.Name,
                    CountryId = model.CountryId,
                    Day = model.Day,
                    Night = model.Night,
                    MinAge = model.MinAge,
                    Validity = model.Validity,
                    Rating = model.Rating,
                    PackageImgUrl = model.PackageImgUrl,
                    StartDate = model.StartDate,
                    PackageSummary = model.PackageSummary,
                    TermsAndCondition = model.TermsAndCondition,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = model.IsDeleted,
                };
                data.IsGroupBooking = model.PackageType != GroupPackageTypeEnum.Both && model.PackageType != GroupPackageTypeEnum.Package;

                Update(data);
                await _db.SaveChangesAsync();

                await AddUpdatePackageType(data.Id, model);
                await AddUpdatePackageClassOption(data.Id, model);

                await _db.SaveChangesAsync();

                if (model.PackageType == GroupPackageTypeEnum.Both)
                {
                    model.PackageType = GroupPackageTypeEnum.GroupBooking;
                    await CreateUpdate(model);
                }

                return new ResponseModel<int>
                {
                    Data = data.Id,
                    Succeeded = true,
                    Message = "Package Saved",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }

        }

        private async Task AddUpdatePackageType(int Id, PackageDetailAddUpdateModel model)
        {
            var toRemove = _db.PackageType.Where(a => a.PackageDetailId == Id).ToList();
            _db.RemoveRange(toRemove);
            var toAdd = model.TourTypeId.Select(a => new PackageType { TourTypeId = a, PackageDetailId = Id });
            _db.AddRange(toAdd);
        }

        private async Task AddUpdatePackageClassOption(int Id, PackageDetailAddUpdateModel model)
        {
            var classOptionData = _db.ClassOptions.ToList();
            foreach (var classOption in classOptionData)
            {
                var data = await _db.PackageClassOption.Where(pd => pd.ClassOptionId == classOption.Id && pd.PackageDetailId == Id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.IsSelected = model.ClassOptionId.Any(optId => optId == classOption.Id);
                    _db.PackageClassOption.Update(data);
                }
                else
                    _db.PackageClassOption.Add(new PackageClassOption
                    {
                        PackageDetailId = Id,
                        ClassOptionId = classOption.Id,
                        IsSelected = model.ClassOptionId.Any(optId => optId == classOption.Id),
                        Name = classOption.Name
                    });
            }
        }

        public async Task<PackageDetailAddUpdateModel> Detail(int Id)
        {
            var model = Get(x => x.Id == Id);
            return new PackageDetailAddUpdateModel()
            {
                Id = Id,
                Code = model.Code,
                Name = model.Name,
                CountryId = model.CountryId,
                Day = model.Day,
                Night = model.Night,
                MinAge = model.MinAge,
                Validity = model.Validity,
                Rating = model.Rating,
                PackageImgUrl = model.PackageImgUrl,
                PackageSummary = model.PackageSummary,
                TermsAndCondition = model.TermsAndCondition,
                StartDate = model.StartDate,
                PackageType = model.IsGroupBooking ? GroupPackageTypeEnum.GroupBooking : GroupPackageTypeEnum.Package,
                IsActive = model.IsActive,
                IsPublished = model.IsPublished,
                IsDeleted = model.IsDeleted,
                TourTypeId = _db.PackageType.Where(a => a.PackageDetailId == Id).Select(a => a.TourTypeId),
                ClassOptionId = _db.PackageClassOption.Where(a => a.PackageDetailId == Id).Select(a => a.ClassOptionId)
            };
        }

        public async Task<List<PackageDetailViewModel>> GetList(bool isFeatured, bool isGroup)
        {
            var query = _db.PackageDetails.Where(x => x.IsPublished && x.IsActive && !x.IsDeleted).Include(p => p.Country)
                            .Include(p => p.PackageImage)
                            .Select(model => new PackageDetailViewModel()
                            {
                                Id = model.Id,
                                Code = model.Code,
                                Name = model.Name,
                                Day = model.Day,
                                Night = model.Night,
                                MinAge = model.MinAge,
                                Validity = model.Validity,
                                Rating = model.Rating,
                                PackageSummary = model.PackageSummary,
                                TermsAndCondition = model.TermsAndCondition,
                                Country = model.Country.CountryName,
                                ImgUrl = model.PackageImgUrl,
                                StartDate = model.StartDate,
                                IsGroupBooking = model.IsGroupBooking,
                                Images = model.PackageImage.Select(x => x.ImageUrl).ToList()
                            });
            query = isGroup ? query.Where(x => x.IsGroupBooking) : query.Where(x => !x.IsGroupBooking);
            if (isFeatured)
            {
                query.OrderByDescending(x => x.Id).Take(5);
            }
            return await query.ToListAsync();
        }

        public async Task<List<PackageDetailViewModel>> GetListByCountry(bool isFeatured, bool isGroup, int countryId)
        {
            var query = _db.PackageDetails.Where(x => x.CountryId==countryId && x.IsPublished && x.IsActive && !x.IsDeleted).Include(p => p.Country)
                            .Include(p => p.PackageImage)
                            .Select(model => new PackageDetailViewModel()
                            {
                                Id = model.Id,
                                Code = model.Code,
                                Name = model.Name,
                                Day = model.Day,
                                Night = model.Night,
                                MinAge = model.MinAge,
                                Validity = model.Validity,
                                Rating = model.Rating,
                                PackageSummary = model.PackageSummary,
                                TermsAndCondition = model.TermsAndCondition,
                                Country = model.Country.CountryName,
                                ImgUrl = model.PackageImgUrl,
                                StartDate = model.StartDate,
                                IsGroupBooking = model.IsGroupBooking,
                                Images = model.PackageImage.Select(x => x.ImageUrl).ToList()
                            });
            query = isGroup ? query.Where(x => x.IsGroupBooking) : query.Where(x => !x.IsGroupBooking);
            if (isFeatured)
            {
                query.OrderByDescending(x => x.Id).Take(5);
            }
            return await query.ToListAsync();
        }

        public async Task<PackageDetailViewModel> PackageDetail(int Id)
        {
            var data = await _db.PackageDetails.Where(x => x.Id == Id).Include(p => p.Country)
                .Include(a => a.PackageType).ThenInclude(c => c.TourType)
                .Include(p => p.PackageImage)
                .Include(p => p.PackageFlight)
                .Select(model => new PackageDetailViewModel()
                {
                    Id = model.Id,
                    Code = model.Code,
                    Name = model.Name,
                    Day = model.Day,
                    Night = model.Night,
                    MinAge = model.MinAge,
                    Validity = model.Validity,
                    Rating = model.Rating,
                    PackageSummary = model.PackageSummary,
                    TermsAndCondition = model.TermsAndCondition,
                    Country = model.Country.CountryName,
                    CountryId = model.Country.Id,
                    ImgUrl = model.PackageImgUrl,
                    StartDate = model.StartDate,
                    IsGroupBooking = model.IsGroupBooking,
                    PackageTypes = model.PackageType.Select(x => new PackageTypesViewModel()
                    {
                        Id = x.Id,
                        Name = x.TourType.Name,
                    }).ToList(),
                    Images = model.PackageImage.Select(x => x.ImageUrl).ToList(),
                    Flights = model.PackageFlight.Select(x=> new PackageFlightViewModel
                    {
                        Origin = x.Origin,
                        Destination = x.Destination,
                        FlightNumber = x.FlightNumber,
                        PNRNumber = x.PNRNumber,
                        DepartureDate = x.DepartureDate,
                        ReturnDate = x.ReturnDate,
                        IsReturn = x.IsReturn,
                        IsMultiCity = x.IsMultiCity,
                    }).ToList()
                }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<ResponseModel<int>> SyncToGroupBooking(SyncPackageDTO model)
        {
            var id = model.Id;
            var package = _db.PackageDetails.Where(a => a.Id == id)
                .Include(inc => inc.Inclusion)
                .Include(iti => iti.Itinerary)
                .Include(htl => htl.PackageHotel)
                 .Include(im => im.PackageImage)
                 .Include(ty => ty.PackageType)
                 .Include(grp => grp.PackageGroup)
                 .Include(pr => pr.PackagePrice)
                 .Include(cl => cl.PackageClassOption)
                 .Include(cl => cl.PackageFlight)
                 .FirstOrDefault();

            if (package == null)
            {
                return new ResponseModel<int> { Succeeded = false, Message = "Invalid package" };
            }

            var defaltClassOp = package.PackageClassOption.Select(a => new PackageClassOption
            {
                Id = a.Id,
                ClassOptionId = a.ClassOptionId
            });
            var defaultGroup = package.PackageGroup.Select(a => new PackageGroup { Id = a.Id, Adult = a.Adult, FreeOfCost = a.FreeOfCost, SingleSupliment = a.SingleSupliment });
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var GroupBook = new PackageDetail
                    {
                        Name = package.Name,
                        Code = package.Code,
                        Day = package.Day,
                        Night = package.Night,
                        MinAge = package.MinAge,
                        Validity = package.Validity,
                        Rating = package.Rating,
                        StartDate = package.StartDate,
                        IsGroupBooking = true,
                        PackageSummary = package.PackageSummary,
                        TermsAndCondition = package.TermsAndCondition,
                        PackageImgUrl = package.PackageImgUrl,
                        CountryId = package.CountryId,
                        IsActive = true,
                        IsDeleted = false,
                        IsPublished = true
                    };
                    await _db.PackageDetails.AddAsync(GroupBook);
                    await _db.SaveChangesAsync();

                    var packageId = GroupBook.Id;
                    var inclusions = new List<PackageInclusions>();
                    var itinerary = new List<PackageItinerary>();
                    var hotels = new List<PackageHotel>();
                    var prices = new List<PackagePrice>();
                    var images = new List<PackageImage>();
                    var types = new List<PackageType>();
                    var groups = new List<PackageGroup>();
                    var classes = new List<PackageClassOption>();
                    var flights = new List<PackageFlight>();

                    if (model.CopyInclusion)
                    {
                        inclusions.AddRange(package.Inclusion.Select(a => new PackageInclusions
                        {
                            Name = a.Name,
                            IsIncluded = a.IsIncluded,
                            PackageDetailId = packageId,
                            IsActive = a.IsActive,
                            IsDeleted = a.IsDeleted,
                            IsPublished = a.IsPublished,
                        }));
                        await _db.AddRangeAsync(inclusions);

                    }
                    if (model.CopyItinerary)
                    {
                        itinerary.AddRange(package.Itinerary.Select(a => new PackageItinerary
                        {
                            Name = a.Name,
                            Day = a.Day,
                            Description = a.Description,
                            PackageDetailId = packageId,
                            IsActive = a.IsActive,
                            IsDeleted = a.IsDeleted,
                            IsPublished = a.IsPublished,
                        }));
                        await _db.AddRangeAsync(itinerary);
                    }
                    types.AddRange(package.PackageType.Select(a => new PackageType
                    {
                        TourTypeId = a.TourTypeId,
                        PackageDetailId = packageId,
                        IsActive = a.IsActive,
                        IsDeleted = a.IsDeleted,
                        IsPublished = a.IsPublished,
                    }));
                    if (model.CopyHotel)
                    {
                        hotels.AddRange(package.PackageHotel.Select(a => new PackageHotel
                        {
                            HotelId = a.HotelId,
                            PackageId = packageId,
                            IsActive = a.IsActive,
                            IsDeleted = a.IsDeleted,
                            IsPublished = a.IsPublished,
                        }));
                        await _db.AddRangeAsync(hotels);
                    }
                    if (model.CopyGroup)
                    {
                        groups.AddRange(package.PackageGroup.Select(a => new PackageGroup
                        {
                            Adult = a.Adult,
                            FreeOfCost = a.FreeOfCost,
                            SingleSupliment = a.SingleSupliment,
                            Order = a.Order,
                            PackageDetailId = packageId,
                            IsActive = a.IsActive,
                            IsDeleted = a.IsDeleted,
                            IsPublished = a.IsPublished,
                        }));
                        await _db.AddRangeAsync(groups);
                    }


                    classes.AddRange(package.PackageClassOption.Select(a => new PackageClassOption
                    {
                        ClassOptionId = a.ClassOptionId,
                        Name = a.Name,
                        IsSelected = a.IsSelected,
                        PackageDetailId = packageId,
                        IsActive = a.IsActive,
                        IsDeleted = a.IsDeleted,
                        IsPublished = a.IsPublished
                    }));
                    if (model.CopyImages)
                    {
                        images.AddRange(package.PackageImage.Select(a => new PackageImage
                        {
                            ImageUrl = a.ImageUrl,
                            PackageDetailId = packageId,
                        }));
                        await _db.AddRangeAsync(images);
                    }
                    if (model.CopyFlight)
                    {
                        flights.AddRange(package.PackageFlight.Select(a => new PackageFlight
                        {
                            Origin = a.Origin,
                            DepartureDate = a.DepartureDate,
                            Destination = a.Destination,
                            FlightNumber = a.FlightNumber,
                            PNRNumber = a.Origin,
                            ReturnDate = a.ReturnDate,
                            IsReturn = a.IsReturn,
                            IsMultiCity = a.IsMultiCity,
                            PackageDetailId = packageId,
                        }));
                        await _db.AddRangeAsync(flights);
                    }

                    await _db.AddRangeAsync(types);
                    await _db.AddRangeAsync(classes);
                    await _db.SaveChangesAsync();

                    if (model.CopyGroup)
                    {
                        foreach (var i in package.PackagePrice)
                        {
                            i.Id = 0;
                            i.PackageDetailId = GroupBook.Id;
                            var orgClassop = defaltClassOp.FirstOrDefault(a => a.Id == i.PackageClassOptionId);
                            var newClassOp = classes.FirstOrDefault(a => a.ClassOptionId == orgClassop?.ClassOptionId);
                            i.PackageClassOptionId = newClassOp?.Id ?? 0;

                            var orgGroup = defaultGroup.FirstOrDefault(a => a.Id == i.PackageGroupId);
                            var newGroup = groups.FirstOrDefault(a => a.SingleSupliment == orgGroup?.SingleSupliment && a.Adult == orgGroup?.Adult && a.FreeOfCost == orgGroup?.FreeOfCost);
                            i.PackageGroupId = newGroup?.Id ?? 0;
                            prices.Add(i);
                        }

                        await _db.AddRangeAsync(prices);
                        await _db.SaveChangesAsync();
                    }

                    // Commit transaction
                    await transaction.CommitAsync();
                    return new ResponseModel<int> { Succeeded = true, Message = "Your Package has been Synced" };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseModel<int> { Succeeded = false, Message = "Could not Sync package" };
                }
            }


        }

    }
}
