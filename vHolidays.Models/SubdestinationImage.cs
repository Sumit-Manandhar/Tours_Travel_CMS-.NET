namespace vHolidays.Models
{
    public class SubdestinationImage
    {
        public int Id { get; set; }
        public int SubdestinationId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsBanner { get; set; }
        public virtual Subdestinations Subdestination { get; set; } = null!;

    }
}
