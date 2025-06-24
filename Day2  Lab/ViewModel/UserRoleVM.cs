using Day2__Lab.Models;
using Microsoft.AspNetCore.Identity;

namespace Day2__Lab.ViewModel
{
    public class UserRoleVM
    {   
        public List<IdentityRole> RoleName { get; set; }
        public List<ApplicationUser> UserName { get; set; }

        public string SelectedRoleId { get; set; }
        public string SelectedUserId { get; set; }

    }
}
