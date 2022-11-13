using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pro3MVC.Entities;
using Pro3MVC.Models;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/subject")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subSer;
        private DatabaseContext _db;
        private IAccSubService _accsubSer;

        public SubjectController(ISubjectService subSer, DatabaseContext db, IAccSubService accsubSer)
        {
            _subSer = subSer;
            _db = db;
            _accsubSer = accsubSer;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var list = _subSer.show();

            return View(list);
        }


        [HttpGet]
        [Route("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var student = _accsubSer.detail(id);
            return View(student);
        }

        [HttpGet]
        [Route("updatedetail/{id}")]
        public async Task<IActionResult> UpdateD(int id)
        {
            var update = await _accsubSer.finById(id);
            ViewBag.update = update;
            return View(update);
        }

		[HttpPost]
		[Route("updatedetail/{id}")]
        public async Task<IActionResult> UpdateD(AccountSubject accsubE)
		{
            var currentAS = await _accsubSer.finById(accsubE.Id);
            currentAS.SubjectId = accsubE.SubjectId;
            currentAS.AccountId= accsubE.AccountId;
            currentAS.Score = accsubE.Score;
            await _accsubSer.updateD(currentAS);
            return RedirectToAction("detail", "subject", new { area = "admin", id = currentAS.SubjectId, idC = currentAS.CourseId});
		}

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            var result = _subSer.Search(keyword);
            return View("index", result);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult listSubject()
        {
            var list = _subSer.listSubject();
            return View(list);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddSubject(SubjectEntity subE)
        {
            var sub = new Subject
            {
                Id = subE.Id,
                Name = subE.Name,
                Sessions = subE.Sessions,
                Status = subE.Status
            };
            await _subSer.addSubject(sub);
            return RedirectToAction("listSubject");
        }

    }
}
