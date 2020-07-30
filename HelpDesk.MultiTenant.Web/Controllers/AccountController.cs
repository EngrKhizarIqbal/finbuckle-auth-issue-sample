using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HelpDesk.MultiTenant.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AspNetUserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, AspNetUserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            var userName = "multi@no.io";
            var user = await _userManager.FindByEmailAsync(userName) ?? await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddPasswordAsync(user, "Admin@1234_");
            }

            await _signInManager.SignOutAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName = null, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(userName) ?? await _userManager.FindByNameAsync(userName);

            if (user == null) return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, "Admin@1234_", true, false);

            if (!result.Succeeded)
            {
                return Json(result);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
