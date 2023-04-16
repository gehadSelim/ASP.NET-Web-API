using Microsoft.AspNetCore.Identity;

namespace Day3.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Department { get; set; } = string.Empty;
    }
}
