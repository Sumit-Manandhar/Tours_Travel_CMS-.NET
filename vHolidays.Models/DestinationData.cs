using System.ComponentModel.DataAnnotations.Schema;
using vHolidays.Models;
using vHolidays.Models.Regions;

namespace vHolidays.DataAccess.Context;

public partial class DestinationData
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? Overview { get; set; }

    public int DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;

    public int CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;
}
