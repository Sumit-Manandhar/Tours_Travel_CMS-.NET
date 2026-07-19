using vHolidays.Models;

namespace vHolidays.DataAccess.Context;

public partial class DestinationImage
{
    public int Id { get; set; }

    public int DestinationId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsPrimary { get; set; }
    public bool IsBanner { get; set; }

    public virtual Destination Destination { get; set; } = null!;
}
