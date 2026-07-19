using vHolidays.Models.Base;

namespace vHolidays.Models
{
    public class ContactUsMessages : BaseEntity
    {
        public string SenderName { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string SenderPhoneNumber { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;

    }
}
