namespace Travels.Models.Package.DatabaseModel
{
    public class PackageImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;

        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;
    }
}
