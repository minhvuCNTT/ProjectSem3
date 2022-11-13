using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/report")]
    public class ReportController : Controller
    {
        private IAccountService accountService;
        public ReportController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [Route("student")]
        public IActionResult Student()
        {
            try
            {
                ViewBag.Accounts = accountService.listStudent();
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("scoredetail/{id}")]
        public IActionResult ScoreDetail(int id)
        {
            try
            {
                var student = accountService.GetById(id);
                ViewBag.Student = student;
                ViewBag.Courses = accountService.GetAllCoursesOfStudent(id);
                return View();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Route("financial")]
        public IActionResult Financial()
        {
            return View();
        }
        [Route("batch")]
        public IActionResult Batch()
        {
            return View();
        }
    }
}
