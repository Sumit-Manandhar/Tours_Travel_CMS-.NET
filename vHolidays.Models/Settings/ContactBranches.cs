using vHolidays.Models.Base;

namespace vHolidays.Models.Settings
{
    public class ContactBranches : BaseEntity
    {
        public string BranchName { get; set; } = null!;
        public string BranchType { get; set; } = null!;
        public string BranchCountry { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string OptionalNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string? CompanyImage { get; set; } 
        public virtual ICollection<BranchAgents> BranchAgents { get; set; } = new List<BranchAgents>();

    }
}
