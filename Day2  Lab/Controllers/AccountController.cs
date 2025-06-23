using System.Security.Claims;
using System.Threading.Tasks;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Day2__Lab.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterVM registerData )
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName=registerData.Email,
                    Email = registerData.Email,
                    PasswordHash = registerData.Password,
                    PhoneNumber=registerData.PhoneNumber,
                    Address = registerData.Address,
                };
              IdentityResult created=  await userManager.CreateAsync(user, registerData.Password);
                if(created.Succeeded)
                {
                   await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }
                foreach(var item in created.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View("Register", registerData);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginVM loginData)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser usre = await userManager.FindByNameAsync(loginData.Email);
                if(usre!=null)
                {
                    bool found = await userManager.CheckPasswordAsync(usre, loginData.Password);
                    if(found)
                    {
                        await signInManager.SignInAsync(usre, loginData.RemberMe);
                        return RedirectToAction("Privacy", "Home");
                    }
                }
                ModelState.AddModelError("","Invalid Account");
            }
                return View("Login",loginData);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> profile()
        {
            AccountRegisterVM regData=null;
            if (User.Identity.IsAuthenticated)
            {
                //Claim IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                //string id = IdClaim.Value;
                ApplicationUser data =await userManager.FindByNameAsync(User.Identity.Name);
                 regData = new AccountRegisterVM()
                {
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    Address=data.Address,
                };
            }
            return View("profile", regData);
        }
        [HttpPost]
        public async Task<IActionResult> profile(AccountRegisterVM profileData)
        {
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Password");
            ModelState.Remove("OldPassword");

            if (!ModelState.IsValid)
                return View("profile", profileData);

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");

            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name); 
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View("profile", profileData);
            }

            if (profileData.OldPassword != "0")
            {
                var found = await userManager.CheckPasswordAsync(user, profileData.OldPassword);
                if (!found)
                {
                    ModelState.AddModelError("", "Wrong Password");
                    return View("profile", profileData);
                }

                if (profileData.Password == profileData.ConfirmPassword && profileData.Password != "0")
                {
                    var changeResult = await userManager.ChangePasswordAsync(user, profileData.OldPassword, profileData.Password);
                    if (!changeResult.Succeeded)
                    {
                        foreach (var error in changeResult.Errors)
                            ModelState.AddModelError("", error.Description);

                        return View("profile", profileData);
                    }
                }
            }

            user.Address = profileData.Address;
            user.PhoneNumber = profileData.PhoneNumber;

            var updated = await userManager.UpdateAsync(user);
            if (!updated.Succeeded)
            {
                foreach (var error in updated.Errors)
                    ModelState.AddModelError("", error.Description);

                return View("profile", profileData);
            }

            await signInManager.SignInAsync(user, true); // refresh login
            return RedirectToAction("Privacy", "Home");
        }

    }
}
