using Microsoft.AspNetCore.Identity;

namespace CMCS.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom properties (if any)
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
