using Books.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> UserManager { get; set; }
        private SignInManager<IdentityUser> SignManger { get; set; }
        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            UserManager = um;
            SignManger = sm;
        }

        [HttpGet]
        public IActionResult Login(string ru)
        {
            return View(new LoginModel { ReturnUrl = ru });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel lm)
        {
            if(ModelState.IsValid)
            {
                IdentityUser u = await UserManager.FindByNameAsync(lm.Username);

                if(u != null)
                {
                    await SignManger.SignOutAsync();
                    if ((await SignManger.PasswordSignInAsync(u, lm.Password, false, false)).Succeeded)
                    {
                        return Redirect(lm.ReturnUrl ?? "/Admin");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(lm);
        }

        public async Task<IActionResult> Logout (string returnUrl = "/")
        {
            await SignManger.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
