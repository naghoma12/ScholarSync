using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
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
            return PartialView();
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
                    return PartialView(model);
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


                     return RedirectToAction(nameof(SecurePage));

                }
                else
                {
                    foreach(var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password",errorItem.Description);
                    }
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Login() 
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
                
                return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
              AppUser user=  await userManager.FindByNameAsync(model.UserNameOrEmail);
                var role = await userManager.GetRolesAsync(user);
                if (user == null) 
                { user = await userManager.FindByEmailAsync(model.UserNameOrEmail); }


                if (user != null) 
                {
                    var found = await signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                   
                    if(found.Succeeded)
                    {

                        List<Claim>? claims = new List<Claim>()
                    {
                         new Claim(ClaimTypes.Name, model.UserNameOrEmail),
                    new Claim(ClaimTypes.Role, role.FirstOrDefault())
                    };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = model.RememberMe
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authenticationProperties);

                        return RedirectToAction(nameof(SecurePage));
                        
                    }
                  
                }
                ModelState.AddModelError("", "Username/Email or Password not correct");
            }
            return View(model);
        }
        
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(AuthenticationSchemes = "Cookies")]
        public IActionResult SecurePage()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
