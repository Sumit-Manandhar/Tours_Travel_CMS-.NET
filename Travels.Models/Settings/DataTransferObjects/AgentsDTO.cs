namespace Travels.Models.Settings.DataTransferObjects
{
    public class AgentsDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public int ContactBranchId { get; set; } = 0;
        public string AgentName { get; set; } = string.Empty;
        public string LiscenceNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
