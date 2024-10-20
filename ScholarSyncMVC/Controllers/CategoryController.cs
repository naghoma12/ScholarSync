using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies",Roles =("Admin"))]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _catRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public CategoryController(IGenericRepository<Category> catRepo,
            IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _catRepo = catRepo;
            _environment = webHostEnvironment;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _catRepo.GetAll();
            if(list == null) 
                { return NotFound(); }
            return View(list);
        }

        // Get Category By Id --GET
        public async Task<IActionResult> Details(int id)
        {
            var item = await _catRepo.GetAsync(id);
            if (item == null) return RedirectToAction(nameof(Index));
            var itemMapped = _mapper.Map<Category, CategoryVM>(item);
            return View(itemMapped);
        }

        //Open the form --Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //Create New Category --post  Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreatedVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categoryVM.PhotoURL = categoryVM.PhotoFile?.FileName;

                    if (categoryVM.PhotoFile != null)
                    {
                        categoryVM.PhotoURL = DocumentSetting.UploadFile(categoryVM.PhotoFile, "category");
                    }
                    else
                    {
                        ModelState.AddModelError("PhotoURL", "Please Enter Photo");
                    }

                    var CatMapped = _mapper.Map<CategoryCreatedVM, Category>(categoryVM);
                    CatMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\category", CatMapped.PhotoURL);

                    _catRepo.Add(CatMapped);
                    var count = _catRepo.Complet();

                    if (count > 0)
                        TempData["message"] = "Category Added Successfully";
                    else
                        TempData["message"] = "Failed Add Operation";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);

                }
            }

            return View(categoryVM);
        }

        //Open the form of edit --get
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id);
        }

        //Edit category --post Category/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (categoryVM.PhotoFile != null)
                    {
                        if (System.IO.File.Exists(categoryVM.FilePath))
                        {
                            System.IO.File.Delete(categoryVM.FilePath);
                        }
                        categoryVM.PhotoURL = DocumentSetting.UploadFile(categoryVM.PhotoFile, "category");
                    }
                    var catMapped = _mapper.Map<CategoryVM, Category>(categoryVM);
                    catMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\category", catMapped.PhotoURL);
                    _catRepo.Update(catMapped);
                    var count = _catRepo.Complet();
                    if (count > 0)
                    {
                        TempData["Message"] = $"Category Updated Successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);

                }
            }
            return View(categoryVM);
        }
        //Open form of delete --Get Category/delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id);
        }

        //Delete category --post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, CategoryVM categoryVM)
        {
            try
            {
                var category = _mapper.Map<CategoryVM, Category>(categoryVM);
                if (System.IO.File.Exists(category.FilePath))
                {
                    System.IO.File.Delete(category.FilePath);
                }
                category.IsDeleted = true;
                _catRepo.Update(category);
                var count = _catRepo.Complet();
                if (count > 0)
                {
                   
                    TempData["Message"] = "Category Deleted Successfully";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Delete Operation Failed";
                return View(categoryVM);
            }
        }
    }
}
