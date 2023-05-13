using Microsoft.AspNetCore.Identity;

namespace hospital.Models
{
	public class AppUser:IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public bool IsDeactive { get; set; }
	}
}
