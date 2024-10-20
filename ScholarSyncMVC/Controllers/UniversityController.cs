using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
	public class UniversityController : Controller
	{
		private readonly IGenericRepository<University> _uniRepo;
		private readonly IMapper _mapper;

		public UniversityController(IGenericRepository<University> uniRepo, IMapper mapper)
		{
			_uniRepo = uniRepo;
			_mapper = mapper;
		}

		// GET: University
		public async Task<IActionResult> Index()
		{
			var list = await _uniRepo.GetAll();
			if (list == null)
			{
				return NotFound();
			}

			return View(list);
		}

		// GET: University/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var item = await _uniRepo.GetAsync(id);
			if (item == null)
			{
				return RedirectToAction(nameof(Index));
			}

			var itemMapped = _mapper.Map<University, UniversityVM>(item);
			return View(itemMapped);
		}

		// GET: University/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		// POST: University/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(UniversityVM universityVM)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var uniMapped = _mapper.Map<UniversityVM, University>(universityVM);
					_uniRepo.Add(uniMapped);
					var count = _uniRepo.Complet();

					if (count > 0)
					{
						TempData["message"] = "University Added Successfully";
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

			return View(universityVM);
		}

		// GET: University/Edit/5
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var item = await _uniRepo.GetAsync(id);
			if (item == null)
			{
				return RedirectToAction(nameof(Index));
			}

			var itemMapped = _mapper.Map<University, UniversityVM>(item);
			return View(itemMapped);
		}

		// POST: University/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, UniversityVM universityVM)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var uniMapped = _mapper.Map<UniversityVM, University>(universityVM);
					uniMapped.Id = id; // Ensure the ID is set for the update
					_uniRepo.Update(uniMapped);
					var count = _uniRepo.Complet();

					if (count > 0)
					{
						TempData["message"] = "University Updated Successfully";
					}

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
				}
			}

			return View(universityVM);
		}

		// GET: University/Delete/5
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var item = await _uniRepo.GetAsync(id);
			if (item == null)
			{
				return RedirectToAction(nameof(Index));
			}

			var itemMapped = _mapper.Map<University, UniversityVM>(item);
			return View(itemMapped);
		}

		// POST: University/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id, UniversityVM universityVM)
		{
			try
			{
				var university = _mapper.Map<UniversityVM, University>(universityVM);
				university.IsDeleted = true; // Assuming you have a soft delete flag
				_uniRepo.Update(university);
				var count = _uniRepo.Complet();

				if (count > 0)
				{
					TempData["message"] = "University Deleted Successfully";
				}

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["message"] = "Delete Operation Failed";
				return View(universityVM);
			}
		}
	
}
}
