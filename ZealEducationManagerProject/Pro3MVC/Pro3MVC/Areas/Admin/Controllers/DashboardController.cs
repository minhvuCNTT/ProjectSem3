using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        private IFacultyService iFacultyservice;
        private IWebHostEnvironment webHostEnvironment;
        public DashboardController(IFacultyService _iFacultyservice, IWebHostEnvironment _webHostEnvironment)
        {
            iFacultyservice = _iFacultyservice;
            webHostEnvironment = _webHostEnvironment;
        }

        //[Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            try
            {
                ViewBag.Faculties = iFacultyservice.listFaculty();
                return View();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}