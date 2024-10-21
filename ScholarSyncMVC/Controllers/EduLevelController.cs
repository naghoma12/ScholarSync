using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Controllers
{
	public class EduLevelController : Controller
	{
		private readonly IGenericRepository<EduLevel> _eduLevel;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _environment;

		public EduLevelController(IGenericRepository<EduLevel> eduLevel, IMapper mapper, IWebHostEnvironment environment)
		{
			_eduLevel = eduLevel;
			_mapper = mapper;
			_environment = environment;
		}

		public async Task<IActionResult> Index()
		{
			var list = await _eduLevel.GetAll();
			if (list == null)
			{
				return NotFound();
			}

			return View(list);
		}

		public async Task<IActionResult> Details(int id)
		{
			var item = await _eduLevel.GetAsync(id);
			if (item == null)
			{
				return RedirectToAction("Index");
			}
			
			return View(item);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(EduLevelVM eduLevelVM)
		{
			if (ModelState.IsValid)
			{
				try
				{
					EduLevel level=new EduLevel();
					level.Name=eduLevelVM.Name;
					
					_eduLevel.Add(level);
					var count = _eduLevel.Complet();
					if (count > 0)
					{
						ViewData["Message"] = "Education Level Created Successfully";
					}
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
				}
			}
			return View(eduLevelVM);
		}

		public async Task<IActionResult> Edit(int id)
		{
			return await Details(id);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EduLevelVM eduLevelVM)
		{
			if (ModelState.IsValid)
			{
				try
				{
					
					var levelMapped = _mapper.Map<EduLevelVM, EduLevel>(eduLevelVM);

					_eduLevel.Update(levelMapped);
					var count = _eduLevel.Complet();
					if (count > 0)
					{
						ViewData["Message"] = "Education Level Updated Successfully";
					}
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);

				}

			}
			return View(eduLevelVM);

		}

		public async Task<IActionResult> Delete(int id)
		{
			return await Details(id);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id, EduLevelVM eduLevelVM)
		{
			try
			{
				var eduLevel = _mapper.Map<EduLevelVM, EduLevel>(eduLevelVM);
				eduLevel.IsDeleted = true; 
				_eduLevel.Update(eduLevel);
				var count = _eduLevel.Complet();

				if (count > 0)
				{
					TempData["message"] = "Education Level Deleted Successfully";
				}

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["message"] = "Delete Operation Failed";
				return View(eduLevelVM);
			}
		}
	}
}
