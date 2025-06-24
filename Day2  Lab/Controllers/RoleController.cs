using System.Threading.Tasks;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Day2__Lab.Controllers
{
    [Authorize(Roles = "admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(RoleVM role)
        {
            if (ModelState.IsValid) 
            {
                if (User.Identity.IsAuthenticated)
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = role.RoleName;
                    IdentityResult created=  await roleManager.CreateAsync(identityRole);
                    if (created.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    foreach(var item in created.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AssignRoleToInstructor()
        {
            UserRoleVM roleData = new UserRoleVM()
            {
                RoleName = roleManager.Roles.ToList(),
                UserName = userManager.Users.Where(r=>r.UserName!="admin").ToList(),

            };
            
            return View( roleData);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToInstructor(UserRoleVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.SelectedRoleId == "0" || user.SelectedUserId == "0")
            {
                ModelState.AddModelError("", "Please select User and Role");
                return View(user);
            }

            var currentUser = await userManager.FindByIdAsync(user.SelectedUserId);
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Selected user not found");
                return View(user);
            }

            var role = await roleManager.FindByIdAsync(user.SelectedRoleId);
            if (role == null)
            {
                ModelState.AddModelError("", "Selected role not found");
                return View(user);
            }

            var currentRoles = await userManager.GetRolesAsync(currentUser);
            if (currentRoles.Any())
            {
                await userManager.RemoveFromRolesAsync(currentUser, currentRoles);
            }

            var result = await userManager.AddToRoleAsync(currentUser, role.Name);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }

            return RedirectToAction("Privacy", "Home");
        }
    }
}
