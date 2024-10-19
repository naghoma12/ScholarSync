using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;
using ScholarSyncMVC.ViewModels;
using System.Diagnostics;

namespace ScholarSyncMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _department;
        private readonly IGenericRepository<Country> _country;
        private readonly IReviewRepository _review;
        private readonly IMapper _mapper;
        private readonly IScholarship _scholarship;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger , IGenericRepository<Department> department 
            , IGenericRepository<Country> country , IReviewRepository review , IMapper mapper ,
            IScholarship scholarship, IConfiguration configuration)
        {
            _logger = logger;
            _department = department;
            _country = country;
            _review = review;
            _mapper = mapper;
            _scholarship = scholarship;
            _configuration = configuration;
        }
        // [Authorize(AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> Index()
        {
            try
            {
              
                var departments = await _department.GetAll();
                var deptMapped = _mapper.Map<IEnumerable<Department>, IEnumerable<SimpleDept>>(departments);
                foreach (var dept in deptMapped)
                {
                    var Scholarships = await _scholarship.GetAllInDept(dept.Id);
                    dept.ScholarshipCount = Scholarships.Count();
                }
                var Reviews = await _review.GetAllReviews();

                var ReviewMapped = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewVM>>(Reviews);
                var Countries = await _country.GetAll();
                HomeVM homeVM = new HomeVM()
                {
                    Departments = deptMapped.ToList(),
                    countries = Countries.ToList(),
                   reviews = ReviewMapped
                };
                return View(homeVM);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.InnerException?.Message ?? ex.Message;
                return View();
            }
        }
        [Authorize(AuthenticationSchemes = "Cookies", Roles = ("Admin"))]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var Reviews = await _review.GetAllReviews();
            var ReviewMapped = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewVM>>(Reviews);
            
            return View(ReviewMapped);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
