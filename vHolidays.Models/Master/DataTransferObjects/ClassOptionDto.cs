namespace vHolidays.Models.Master.DataTransferObjects
{
    public class ClassOptionDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
