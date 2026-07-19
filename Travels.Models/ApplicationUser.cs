using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Travels.Utility.Enumerations;

namespace Travels.Models
{
	public class ApplicationUser:IdentityUser {
		[Required]
        public string Name { get; set; }
		public string? StreetAddress { get; set; }
		public string? StreetAddress1 { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set; }
		public string? ImageUrl { get; set; }
		public Status Status { get; set; }
		[NotMapped]
        public string Role { get; set; }
    }
}
