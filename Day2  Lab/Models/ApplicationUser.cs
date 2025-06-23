using Microsoft.AspNetCore.Identity;

namespace Day2__Lab.Models
{
    public class ApplicationUser:IdentityUser
    {
        public  string Address { get; set; }
    }
}
