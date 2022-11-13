using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pro3MVC.Models;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Student.Controllers
{
    [Area("student")]
    [Route("student/score")]

    public class ScoreController : Controller
    {
        private ICourseService iCourseService;
        private IWebHostEnvironment webHostEnvironment;
        private readonly HttpContext context;
        private DatabaseContext db;
        //var stringId = context.Session.GetString("cart");
        public ScoreController(ICourseService _iCourseService, IWebHostEnvironment _webHostEnvironment,
            DatabaseContext _db)
        {
            db = _db;
            iCourseService = _iCourseService;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            int idStudent = (int)HttpContext.Session.GetInt32("id");
            var listCourse = iCourseService.ListCourse(idStudent);
            ViewBag.lC = listCourse;

            return View();

        }

        [Route("transcript")]
        public IActionResult Transcript()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
                var student = iCourseService.GetById(id);
                ViewBag.Student = student;
                ViewBag.Courses = iCourseService.GetAllCoursesOfStudent(id);
                return View();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

    }
}