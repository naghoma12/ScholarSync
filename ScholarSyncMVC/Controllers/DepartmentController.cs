using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
    public class DepartmentController : Controller
    {
        private readonly IGenericRepository<Department> _department;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IGenericRepository<Department> department,IMapper mapper
            ,IWebHostEnvironment environment)
        {
            _department = department;
           _mapper = mapper;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _department.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }


        public async Task<IActionResult> Details(int id)
        {
            var item = await _department.GetAsync(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            var itemMapped = _mapper.Map<Department, CounryDeptEditVM>(item);
            return View(itemMapped);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryDepartmentVM departmentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
					departmentVM.PhotoURL = departmentVM.PhotoFile?.FileName;
					if (departmentVM.PhotoFile != null)
					{
						departmentVM.PhotoURL = DocumentSetting.UploadFile(departmentVM.PhotoFile, "department");
					}
					else
					{
						ModelState.AddModelError("PhotoURL", "Please Enter Photo");
					}
					var deptMapped = _mapper.Map<CountryDepartmentVM, Department>(departmentVM);
					deptMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\department", deptMapped.PhotoURL);

					_department.Add(deptMapped);
                    var count = _department.Complet();
                    if (count > 0)
                    {
                        ViewData["Message"] = "Department Created Successfully";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CounryDeptEditVM departmentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
					if (departmentVM.PhotoFile != null)
					{
						if (System.IO.File.Exists(departmentVM.FilePath))
						{
							System.IO.File.Delete(departmentVM.FilePath);
						}
						departmentVM.PhotoURL = DocumentSetting.UploadFile(departmentVM.PhotoFile, "department");
					}
					var deptMapped = _mapper.Map<CounryDeptEditVM, Department>(departmentVM);

                    deptMapped.FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\department", deptMapped.PhotoURL);

                    _department.Update(deptMapped);
                    var count = _department.Complet();
                    if (count > 0)
                    {
                        ViewData["Message"] = "Department Updated Successfully";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);

                }

            }
            return View(departmentVM);

        }
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, CounryDeptEditVM departmentVm)
        {
            try
            {
                var department = _mapper.Map<CounryDeptEditVM, Department>(departmentVm);
                if (System.IO.File.Exists(department.FilePath))
                {
                    System.IO.File.Delete(department.FilePath);
                }
                department.IsDeleted = true; // Assuming you have a soft delete flag
                _department.Update(department);
                var count = _department.Complet();

                if (count > 0)
                {
                    TempData["message"] = "Department Deleted Successfully";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Delete Operation Failed";
                return View(departmentVm);
            }
        }
    }
}