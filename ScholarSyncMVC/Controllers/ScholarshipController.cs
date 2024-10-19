using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;
using System.Diagnostics;
using ScholarSyncMVC.Helper;
using System.Collections.Generic;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace ScholarSyncMVC.Controllers
{
    
    public class ScholarshipController : Controller
    {
        private readonly IScholarship _scholarship;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<University> _university;
        private readonly IGenericRepository<Country> _country;
        private readonly IGenericRepository<Department> _department;
        private readonly IGenericRepository<Category> _category;
        private readonly IWebHostEnvironment _environment;
        private readonly IRequirement _requirement;

        public ScholarshipController(IScholarship scholarship , IMapper mapper,
            IGenericRepository<University> university, IGenericRepository<Country> country
            ,IGenericRepository<Department> department , IGenericRepository<Category> category, 
            IWebHostEnvironment environment , IRequirement requirement)
        {
            _scholarship = scholarship;
            _mapper = mapper;
            _university = university;
            _country = country;
            _department = department;
            _category = category;
            _environment = environment;
            _requirement = requirement;
        }
        [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        public async Task<IActionResult> Index()
        {
            var list = await _scholarship.GetAllWithTables();
            if (list == null)
            { return NotFound(); }
            return View(list);
        }
        [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _scholarship.GetByIdInclude(id);
            if (item == null) return RedirectToAction(nameof(Index));
            var itemMapped = _mapper.Map<Scholarship, ScholarshipVM>(item);
            return View(itemMapped);
        }
      //  [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var UniList = await _university.GetAll();
            var CatList = await _category.GetAll();
            var CouList = await _country.GetAll();
            var DepList = await _department.GetAll();
            ScholarshipVM scholarshipVM = new ScholarshipVM()
            {
                Categories = CatList,
                Universities = UniList,
                Countries = CouList,
                Departments = DepList
            };
            return View(scholarshipVM);
        }

        // POST: University/Create
        [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult Create(ScholarshipVM scholarshipVM)
        {
	        if (ModelState.IsValid)
	        {
		        try
		        {

			        scholarshipVM.PhotoURL = scholarshipVM.PhotoFile?.FileName;

			        if (scholarshipVM.PhotoFile != null)
			        {
				        scholarshipVM.PhotoURL = DocumentSetting.UploadFile(scholarshipVM.PhotoFile, "scholarship");
			        }
			        else
			        {
				        ModelState.AddModelError("PhotoURL", "Please Enter Photo");
			        }

			        var schMapped = _mapper.Map<ScholarshipVM, Scholarship>(scholarshipVM);

			        //	schMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\category", schMapped.PhotoURL);

			        schMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\scholarship",
				        schMapped.PhotoURL);

			        _scholarship.Add(schMapped);

			        var count = _scholarship.Complet();

			        if (count > 0)
			        {
				        TempData["message"] = "Scholarship Added Successfully";
			        }
			        else
			        {
				        TempData["message"] = "Failed Add Operation";
			        }

			        return RedirectToAction(nameof(Index));
		        }
		        catch (Exception ex)
		        {
			        ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
		        }
	        }

	        return View(scholarshipVM);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        public async Task<IActionResult> Edit(int id)
        {
            var UniList = await _university.GetAll();
            var CatList = await _category.GetAll();
            var CouList = await _country.GetAll();
            var DepList = await _department.GetAll();
            var item = await _scholarship.GetByIdInclude(id);
            if (item == null) return RedirectToAction(nameof(Index));
            var itemMapped = _mapper.Map<Scholarship, ScholarshipVM>(item);
            itemMapped.Categories = CatList;
            itemMapped.Countries = CouList;
            itemMapped.Universities = UniList;
            itemMapped.Departments = DepList;
            return View(itemMapped);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ScholarshipVM scholarshipVM)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (scholarshipVM.PhotoFile != null)
                    {
                        if (System.IO.File.Exists(scholarshipVM.FilePath))
                        {
                            System.IO.File.Delete(scholarshipVM.FilePath);
                        }
                        scholarshipVM.PhotoURL = DocumentSetting.UploadFile(scholarshipVM.PhotoFile, "scholarship");
                    }

                    var schMapped = _mapper.Map<ScholarshipVM, Scholarship>(scholarshipVM);
                    schMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\scholarship", schMapped.PhotoURL);

                    _scholarship.Update(schMapped);
                    var count = _scholarship.Complet();

                    if (count > 0)
                    {
                        TempData["message"] = "Scholarship Updated Successfully";
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }

            return View(scholarshipVM);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        public async Task<IActionResult> Delete(int id)
        {
			var item = await _scholarship.GetByIdInclude(id);
			if (item == null) return RedirectToAction(nameof(Index));
			var itemMapped = _mapper.Map<Scholarship, ScholarshipVM>(item);
			return View(itemMapped);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id ,ScholarshipVM scholarshipVM)
        {
			try
			{
                var scholarship = _mapper.Map<ScholarshipVM, Scholarship>(scholarshipVM);
                if (System.IO.File.Exists(scholarship.FilePath))
                {
                    System.IO.File.Delete(scholarship.FilePath);
                }

				scholarship.IsDeleted = true; // Assuming you have a soft delete flag
				_scholarship.Update(scholarship);
				var count = _scholarship.Complet();

				if (count > 0)
				{
					TempData["message"] = "Scholarship Deleted Successfully";
				}

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["message"] = "Delete Operation Failed";
				return View(scholarshipVM);
			}
		}

        // scholarship viewcard
        public async Task<IActionResult> viewcardscolarship(int page = 1, int pageSize = 4)
        {
            var list = await _scholarship.GetAllWithTables();
            if (list == null || !list.Any())
            {
                return Content("No scholarships found.");
            }

            var filteredList = list.Where(s => s.CategoryId == 1).ToList();

            var paginatedList = filteredList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)filteredList.Count() / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.CategoryId = 1;

            return View(paginatedList);

        }
      
        public async Task<IActionResult> viewcardExchangeprogram(int page = 1, int pageSize = 4)
        {

            var list = await _scholarship.GetAllWithTables();

            if (list == null || !list.Any())
            {
                return Content("No scholarships found.");
            }

            var filteredList = list.Where(s => s.CategoryId == 3).ToList();

            var paginatedList = filteredList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)filteredList.Count() / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.CategoryId = 2;

            return View(paginatedList);



        }

    [Authorize(AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> ScholarshipDetails(int id)
        {
            var scholarship = await _scholarship.GetByIdInclude(id);
            if (scholarship == null)
            {
                return RedirectToAction("Index");
            }
            var requirements = await _requirement.RequirementsOfScholarship(scholarship.Id);
            var sclMapped = _mapper.Map<Scholarship, ScholarshipDetailsVM>(scholarship);
            foreach (var requirement in requirements)
            {
                sclMapped.requirements.Add(requirement.Requirement);
            }
            
            return View(sclMapped);
            
        }
    }
}
