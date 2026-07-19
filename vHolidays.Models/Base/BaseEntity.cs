using System.ComponentModel.DataAnnotations;

namespace vHolidays.Models.Base
{
	public class BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; } =DateTime.Now;
		public string? ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}
