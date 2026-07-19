using Travels.Models.Base;

namespace Travels.Models
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
