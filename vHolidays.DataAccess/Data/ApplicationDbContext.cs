using vHolidays.Models;
using vHolidays.Models.Regions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Context;
using vHolidays.Models.Settings;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Master.DatabaseModel;
using vHolidays.Models.Hotel;
using vHolidays.Models.TailorTrip.DatabaseModel;
using vHolidays.Models.SupplyPartner.DatabaseModel;

namespace vHolidays.DataAcess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Country> Country => Set<Country>();
        public DbSet<City> City => Set<City>();
        public DbSet<State> State => Set<State>();
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<ContactUsMessages> ContactUsMessages { get; set; }
        public DbSet<ContactBranches> ContactBranches { get; set; }
        public DbSet<BranchAgents> BranchAgents { get; set; }
        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SubdestinationData> SubdestinationData { get; set; }
        public DbSet<SubdestinationImage> SubdestinationImage { get; set; }
        public DbSet<Subdestinations> Subdestinations { get; set; }

        //public DbSet<Product> Products{ get; set; }
        //public DbSet<Company> Companies { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        //public DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<DestinationData> DestinationData { get; set; }
        public virtual DbSet<DestinationImage> DestinationImages { get; set; }
        public virtual DbSet<PackageBooking> PackageBookings { get; set; }
        public virtual DbSet<PackageFlight> PackageFlight { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<PackageHotel> PackageHotel { get; set; }
        #region Master
        public virtual DbSet<TourType> TourType { get; set; }
        public virtual DbSet<ClassOption> ClassOptions { get; set; }

        #endregion
        #region Package
        public virtual DbSet<PackageDetail> PackageDetails { get; set; }
        public virtual DbSet<PackageInclusions> PackageInclusions { get; set; }
        public virtual DbSet<PackageItinerary> PackageItineraryss { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
        public virtual DbSet<PackageImage> PackageImage { get; set; }
        public virtual DbSet<PackageGroup> PackageGroup { get; set; }
        public virtual DbSet<PackageClassOption> PackageClassOption { get; set; }
        public virtual DbSet<PackagePrice> PackagePrice { get; set; }

        #endregion

        #region Tailor Made Trip
        public virtual DbSet<TailorMadeTrip> TailorMadeTrip { get; set; }
        public virtual DbSet<TripPersonalDetail> TripPersonalDetail { get; set; }
        #endregion

        #region Vendor Supplier
        public virtual DbSet<SupplierDetail> SupplierDetail { get; set; }
        public virtual DbSet<SupplierDestination> SupplierDestination { get; set; }
        public virtual DbSet<SupplierDestinationCity> SupplierDestinationCity { get; set; }
        public virtual DbSet<SupplierProducts> SupplierProducts { get; set; }
        public virtual DbSet<SupplierContact> SupplierContact { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchAgents>()
             .HasOne(ba => ba.ContactBranch)
             .WithMany(cb => cb.BranchAgents)
             .HasForeignKey(ba => ba.ContactBranchId);
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Contacts>().HasData(
                new Contacts
                {
                    Id = 1,
                    ContactNumber = " +65 81619081",
                    ContactEmail = "enquiry@vietjetholidays.com",
                    ContactAddress = "101 Kitchener Road, #03-38 Jalan Besar Plaza, Singapore 208511",
                    MapLinks= "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.7775084830796!2d103.8554412749657!3d1.3087497986788301!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31da19e1d186e02d%3A0x2242714927df7e83!2sJalan%20Besar%20Plaza!5e0!3m2!1sen!2snp!4v1728475580324!5m2!1sen!2snp",
                    FacebookLink= "https://www.facebook.com/",
                    InstagramLink= "https://www.instagram.com/",
                    TwitterLink= "https://www.twitter.com/",
                    YoutubeLink= "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    IsDeleted=false,
                    IsActive = true,
                    IsPublished=true,
                    CreatedBy = "admin@vietjet.com",
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
