using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ScholarSyncMVC.Controllers
{
    public class ProfileController : Controller
    {
        
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Department> _department;
        private readonly IGenericRepository<EduLevel> _eduLevel;
        private readonly IGenericRepository<Education> _edu;
        private readonly IGenericRepository<Country> _nationality;
        private readonly IWebHostEnvironment _environment;
        private readonly SignInManager<AppUser> signInManager;

        public ProfileController(UserManager<AppUser> userManager, IMapper mapper, IGenericRepository<Department> department, 
            IGenericRepository<EduLevel> eduLevel,IGenericRepository<Education>education, IGenericRepository<Country> nationality,
            IWebHostEnvironment environment, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _mapper = mapper;
            _department = department;
            _eduLevel = eduLevel;
            _edu = education;
            _nationality = nationality;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var userid = userManager.GetUserId(HttpContext.User);

            var user = await userManager.Users
        .Where(u => u.Id == userid)
        .Include(u => u.Nationality) // Eager load Nationality
        .Include(u => u.Department)  // Eager load Department
        .Include(u => u.Educations)
         .ThenInclude(e => e.EduLevel)  
        .FirstOrDefaultAsync();

           
        
        

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
             
                ProfileVM profile=_mapper.Map<AppUser,ProfileVM>(user);
              

                return View(profile);
            }
        }

        
        public async Task<IActionResult> Edit()
        {
            var DeptList = await _department.GetAll();
            var LevelList=await _eduLevel.GetAll();
            var NatList=await _nationality.GetAll();

            

            var userId = userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login","Account");
            }
            
                AppUser user= userManager.FindByIdAsync(userId).Result;
                ProfileVM profile = _mapper.Map<AppUser, ProfileVM>(user);
                profile.Departments = DeptList;
                profile.EduLevels = LevelList;
                profile.Nationalities = NatList;
           
            return View(profile);
            
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileVM profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = userManager.GetUserId(HttpContext.User);
                    if (userId == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    AppUser user = userManager.FindByIdAsync(userId).Result;

                    if (profile.PhotoFile != null)
                    {
                        if (System.IO.File.Exists(profile.FilePath))
                        {
                            System.IO.File.Delete(profile.FilePath);
                        }
                        profile.PhotoURL = DocumentSetting.UploadFile(profile.PhotoFile, "User");
                    }
                    
                    //AppUser user = _mapper.Map<ProfileVM, AppUser>(profile);
                    user.FirstName = profile.FirstName;
                    user.LastName = profile.LastName;
                    user.Email = profile.Email;
                    user.UserName = profile.UserName;
                    user.PhoneNumber = profile.PhoneNumber;
                    user.Gender = profile.Gender;
                    user.NationalityId = profile.NationalityId;
                    user.DepartmentId=profile.DepartmentId;
                    user.Description = profile.Description;
                    user.BirthDate = profile.BirthDate;
                    if(profile.PhotoURL != null)
                    { user.PhotoURL = profile.PhotoURL; }
                    
                    int id = (int)user.NationalityId;

                    user.Nationality =await _nationality.GetAsync(id);

                    id = (int)user.DepartmentId;
                    user.Department = await _department.GetAsync(id);
                    


                    user.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\User", user.PhotoURL);
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        ViewData["Message"] = "Profile Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    return View(profile);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            return View(profile);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordChangeVM model)
        {
            if (ModelState.IsValid)
            {

                var userId = userManager.GetUserId(HttpContext.User);
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                AppUser user = await userManager.FindByIdAsync(userId);
               
                if (user != null)
                {
                    IdentityResult result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    signInManager.SignInAsync(user, true);

                    if (result.Succeeded)
                    {
                        ViewData["Message"] = "Password Updated Successfully";
                       
                        return View("Index");
                    }


                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                  
                    return View(model);
                }

            }

            return View(model);
        }


        // GET: Profile/Education/Create
        public async Task<IActionResult> CreateEducation()
        {
            var eduList = await _eduLevel.GetAll();
            EducationVM educationVM = new EducationVM()
            {
                eduLevels = eduList
            };
            return View(educationVM);
        }

        // POST: Profile/Education/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEducation(EducationVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {var education = new Education
                {
                    Institution = model.Institution,
                    StartDate = model.StartDate, // Use StartYear
                    EndDate = model.EndDate, // Use EndYear
                    EduLevelId = model.EduLevelId,
                    DidGraduate = model.DidGraduate,
                    AppUserId = userManager.GetUserId(HttpContext.User)
                };

                _edu.Add(education);
                var count = _edu.Complet();

                if (count > 0)
                {
                    TempData["message"] = "Education Added Successfully";
                }
                else
                {
                    TempData["message"] = "Failed Add Operation";
                }


                    return RedirectToAction("Index"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            return View(model);

        }

    

        // GET: Profile/Education/Edit/5
        public async Task<IActionResult> EditEducation(int id)
        {
        
          var eduList = await _eduLevel.GetAll();
            var education = await _edu.GetAsync(id);
            if (education == null)
            {
                RedirectToAction(nameof(Index));
            }

            

            var model = new EducationVM
            {
                Id = education.Id,
                Institution = education.Institution,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                EduLevelId = education.EduLevelId
            };


            model.eduLevels = eduList;
            return View(model);
        }

        // POST: Profile/Education/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEducation(EducationVM model)
        {
            if (ModelState.IsValid)
            {
                try  
                {var education = await _edu.GetAsync(model.Id);
                if (education == null)
                {
                    return RedirectToAction("Index");
                }

                education.Institution = model.Institution;
                education.StartDate = model.StartDate; // Update StartYear
                education.EndDate = model.EndDate; // Update EndYear
                education.EduLevelId = model.EduLevelId;
                education.DidGraduate = model.DidGraduate;
                _edu.Update(education);
                var count = _edu.Complet();

                if (count > 0)
                {
                    TempData["message"] = "Education Updated Successfully";
                }

                    return RedirectToAction("Index"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _edu.GetAsync(id);
            if (education == null) return RedirectToAction(nameof(Index));
            var model = new EducationVM
            {
                Id = education.Id,
                Institution = education.Institution,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                EduLevelId = education.EduLevelId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int id, EducationVM model)
        {
            var education=await _edu.GetAsync(id);
            if(education == null)return RedirectToAction(nameof(Index));

            education.IsDeleted = true;
            
            _edu.Update(education);
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            AppUser user = await userManager.FindByIdAsync(userId);
            var educationToRemove = user.Educations.FirstOrDefault(e => e.Id == id); // Retrieve the education by Id

            if (educationToRemove != null)
            {
                user.Educations.Remove(educationToRemove);
                
            }


                var count = _edu.Complet();

           

            return RedirectToAction("Index");
        }
    }




}

