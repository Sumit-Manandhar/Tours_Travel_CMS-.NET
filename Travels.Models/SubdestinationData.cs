namespace Travels.Models
{
    public class SubdestinationData
    {
        public int Id { get; set; }
        public int SubdestinationId { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? TopDestination { get; set; }
        public virtual Subdestinations Subdestination { get; set; } = null!;
    }
}
