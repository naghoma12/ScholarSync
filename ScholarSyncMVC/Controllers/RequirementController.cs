using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
    public class RequirementController : Controller
    {
        private readonly IRequirement _requirements;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Scholarship> _scholarship;

        public RequirementController(IRequirement requirements
            ,IMapper mapper , IGenericRepository<Scholarship> scholarship)
        {
            _requirements = requirements;
           _mapper = mapper;
           _scholarship = scholarship;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _requirements.GetRequirementsAsync();
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _requirements.GetRequiredRequirementsAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var itemMapped = _mapper.Map<Requirements, RequirementVM>(item);
            return View(itemMapped);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           var SchList = await _scholarship.GetAll();
            RequirementVM vm = new RequirementVM();
            vm.scholarships = SchList;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RequirementVM requirementVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RequirMapped = _mapper.Map<RequirementVM, Requirements>(requirementVM);
                    _requirements.Add(RequirMapped);
                    var count = _requirements.Complet();

                    if (count > 0)
                    {
                        TempData["message"] = "Requirement Added Successfully";
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
            return View(requirementVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _requirements.GetRequiredRequirementsAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var itemMapped = _mapper.Map<Requirements, RequirementVM>(item);
            var schList = await _scholarship.GetAll();
            itemMapped.scholarships = schList;
            return View(itemMapped);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequirementVM requirementVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RequirMapped = _mapper.Map<RequirementVM, Requirements>(requirementVM);
                    _requirements.Update(RequirMapped);
                    var count = _requirements.Complet();

                    if (count > 0)
                    {
                        TempData["message"] = "Requirement Updated Successfully";
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }

            return View(requirementVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, RequirementVM requirementVM)
        {
            try
            {
                var requirement = _mapper.Map<RequirementVM, Requirements>(requirementVM);
                requirement.IsDeleted = true; // Assuming you have a soft delete flag
                _requirements.Update(requirement);
                var count = _requirements.Complet();

                if (count > 0)
                {
                    TempData["message"] = "Requirement Deleted Successfully";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Delete Operation Failed";
                return View(requirementVM);
            }
        }
    }
}
