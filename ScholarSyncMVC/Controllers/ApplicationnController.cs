using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ScholarSyncMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class ApplicationnController : Controller
    {

        private readonly IScholarship _scholarship;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<University> _university;
        private readonly IGenericRepository<Country> _country;
        private readonly IGenericRepository<Department> _department;
        private readonly IGenericRepository<Applicationn> _applicationn;
        private readonly ILogger<ApplicationnController> _logger;


        private readonly IWebHostEnvironment _environment;

        public ApplicationnController(IGenericRepository<Applicationn> applicationn, IScholarship scholarship,
            IMapper mapper,
            IGenericRepository<University> university, IGenericRepository<Country> country
            , IWebHostEnvironment environment, IGenericRepository<Department> department ,ILogger<ApplicationnController> logger)
        {
            _applicationn = applicationn;
            _scholarship = scholarship;
            _mapper = mapper;
            _university = university;
            _country = country;
            _department = department;
            _environment = environment;
            _logger = logger;


        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> SaveData()
        {
            var UniList = await _university.GetAll();
            var CouList = await _country.GetAll();
            var DepList = await _department.GetAll();

			applicationnVM applicationnVM = new applicationnVM()
            {

                Universities = UniList,
                Countries = CouList,
                Departments = DepList

			};
            return View(applicationnVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveData(applicationnVM applicationnVM)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    #region IFELSe
                    applicationnVM.AcademicTranscripts_FileName = applicationnVM.AcademicTranscripts.FileName;

                    applicationnVM.LanguageProficiencyLevel_FileName = applicationnVM.LanguageProficiencyLevel?.FileName;
                    applicationnVM.CV_FileName = applicationnVM.CV?.FileName;
                    applicationnVM.MotivationLetter_FileName = applicationnVM.MotivationLetter?.FileName;
                    applicationnVM.Recommendationletters_FileName = applicationnVM.Recommendationletters?.FileName;
                    applicationnVM.Passport_FileName = applicationnVM.Passport?.FileName;

                    applicationnVM.ProofOfFinancialAbility_FileName = applicationnVM.ProofOfFinancialAbility?.FileName;
                    applicationnVM.FundingSources_FileName = applicationnVM.FundingSources?.FileName;
                    applicationnVM.ProofOfHealthInsurance_FileName = applicationnVM.ProofOfHealthInsurance?.FileName;
                    var AppMapped = _mapper.Map<applicationnVM, Applicationn>(applicationnVM);

					if (applicationnVM.AcademicTranscripts != null)
                    {

                        applicationnVM.AcademicTranscripts_FileName =
                            DocumentSetting.UploadFile(applicationnVM.AcademicTranscripts, "Filespath");
                        applicationnVM.AcademicTranscripts_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.AcademicTranscripts_FileName);
                    }
                    else
                    {
                        ModelState.AddModelError("Filepath", "Please Enter File");
                    }

                    if (applicationnVM.LanguageProficiencyLevel != null)
                    {

                        applicationnVM.LanguageProficiencyLevel_FileName =
                            DocumentSetting.UploadFile(applicationnVM.LanguageProficiencyLevel, "Filespath");
                        applicationnVM.LanguageProficiencyLevel_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.LanguageProficiencyLevel_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.CV != null)
                    {


                        applicationnVM.CV_FileName = DocumentSetting.UploadFile(applicationnVM.CV, "Filespath");
                        applicationnVM.CV_FilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads\\Filespath",
                            applicationnVM.CV_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.MotivationLetter != null)
                    {


                        applicationnVM.MotivationLetter_FileName =
                            DocumentSetting.UploadFile(applicationnVM.MotivationLetter, "Filespath");
                        applicationnVM.MotivationLetter_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.MotivationLetter_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.Recommendationletters != null)
                    {



                        applicationnVM.Recommendationletters_FileName =
                            DocumentSetting.UploadFile(applicationnVM.Recommendationletters, "Filespath");
                        applicationnVM.Recommendationletters_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.Recommendationletters_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.Passport != null)
                    {



                        applicationnVM.Passport_FileName =
                            DocumentSetting.UploadFile(applicationnVM.Passport, "Filespath");
                        applicationnVM.Passport_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.Passport_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.ProofOfFinancialAbility != null)
                    {



                        applicationnVM.ProofOfFinancialAbility_FileName =
                            DocumentSetting.UploadFile(applicationnVM.ProofOfFinancialAbility, "Filespath");
                        applicationnVM.ProofOfFinancialAbility_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.ProofOfFinancialAbility_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.FundingSources != null)
                    {



                        applicationnVM.FundingSources_FileName =
                            DocumentSetting.UploadFile(applicationnVM.FundingSources, "Filespath");
                        applicationnVM.FundingSources_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.FundingSources_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

                    if (applicationnVM.ProofOfHealthInsurance != null)
                    {

                        applicationnVM.ProofOfHealthInsurance_FileName =
                            DocumentSetting.UploadFile(applicationnVM.ProofOfHealthInsurance, "Filespath");
                        applicationnVM.ProofOfHealthInsurance_FilePath = Path.Combine(_environment.ContentRootPath,
                            "wwwroot\\Uploads\\Filespath", applicationnVM.ProofOfHealthInsurance_FileName);
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("Filepath", "Please Enter File");
                    //}

					#endregion


					_applicationn.Add(AppMapped);

                    var count = _applicationn.Complet();

                    if (count > 0)
                    {
                        TempData["message"] = "application Sent Successfully";
                    }
                    else
                    {
                        TempData["message"] = "Failed Sent Operation";
                    }

                   // return Content("kkk");
                       return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while saving the application.");
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }

            return View(applicationnVM);
        }

    }
}