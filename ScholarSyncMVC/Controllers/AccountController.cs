using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.ViewModels;
using System.Security.Claims;

namespace ScholarSyncMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager= userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration( RegistrationVM model)
        {
            
            if (ModelState.IsValid)
            { 
                 
                AppUser account= new AppUser();
                account.Email = model.Email;
                account.UserName = model.UserName;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                //account.DepartmentId = 1;
                //account.Description = "Ay hga";
                //account.Nationality = "Egyption";

                AppUser user= new AppUser();
                user = await userManager.FindByEmailAsync(account.Email);

                if ( user!=null)
                { 
                    return View(model);
                }

               IdentityResult result= await userManager.CreateAsync(account,model.Password);
               if(result.Succeeded)
                {
                    //create cookie
                    // var claims = new List<Claim>
                    //{
                    //    new Claim (ClaimTypes.Name, user.Email),
                    //    new Claim("Name",user.FirstName),
                    //    new Claim(ClaimTypes.Role,"User")
                    //};
                   await signInManager.SignInAsync(account,false);


                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";
                     return View();

                }
                else
                {
                    foreach(var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password",errorItem.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
              AppUser user=  await userManager.FindByNameAsync(model.UserNameOrEmail);
                if (user == null) 
                { user = await userManager.FindByEmailAsync(model.UserNameOrEmail); }


                if (user != null) 
                {
                 bool found= await userManager.CheckPasswordAsync(user,model.Password);
                   
                    if(found==true)
                    {
                        signInManager.SignInAsync(user,model.RememberMe);
                        return RedirectToAction("SecurePage");
                    }
                  
                }
                ModelState.AddModelError("", "Username/Email or Password not correct");
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
          signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            
            return RedirectToAction("Index","Profile");
        }
    }
}
