using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mission7.Models;
using Mission7.Models.ViewModels;

namespace Mission7.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> si)
        {
            userManager = um;
            signInManager = si;
        }

        //get request that pulls up the login form
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(login.Username);

                if(user != null)
                {
                    //sign out before signing in
                    await signInManager.SignOutAsync();

                    if((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                    {
                        //if login is successfull redirect them to the admin page
                        return Redirect(login?.ReturnUrl ?? "/admin");
                    }
                } 
            }

            ModelState.AddModelError("", "Invalid Name Or Password");
            return View(login);
        }

        //this handles logging out and redircts to a return url which is usually the home page
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
