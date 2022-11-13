using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Entities;
using Pro3MVC.Helper;
using Pro3MVC.Models;
using Pro3MVC.Services;
using System.Diagnostics;
using System.Globalization;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/faculty")]
    public class FacultyController : Controller
    {
        private IFacultyService iFacultyservice;
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext _db;
        public FacultyController(IFacultyService _iFacultyservice, IWebHostEnvironment _webHostEnvironment, DatabaseContext db)
        {
            iFacultyservice = _iFacultyservice;
            webHostEnvironment = _webHostEnvironment;
            _db = db;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
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

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View(new Faculty());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Faculty faculty, string birthday, IFormFile file)
        {
            if (birthday != null)
            {
                var dateTimeBirthday = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                faculty.Dob = dateTimeBirthday.Date;
            }
            if (file != null && file.Length > 0)
            {
                var newFileName = new UploadHelper().GenerateFileName(file.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img", newFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                faculty.Photo = newFileName;
            }
            else
            {
                faculty.Photo = "personavatar.png";
            }
            if (faculty != null)
            {
                iFacultyservice.Create(faculty);
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var account = iFacultyservice.FindById(id);
            var x = account.Dob.ToString(); //8/16/1996 12:00:00 AM  -- 08/6/1996
            var cut = x.Replace(" 12:00:00 AM", "");
            if (cut.IndexOf("/") == 1)
            {
                cut = cut.Insert(0, "0");
            }
            if (cut.LastIndexOf("/") == 4)
            {
                cut = cut.Insert(3, "0");
            }
            var parse = DateTime.ParseExact(cut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Debug.WriteLine(x);
            ViewBag.Birthday = parse.ToString("dd-MM-yyyy");
            return View("edit", account);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Faculty faculty, string birthday, IFormFile file)
        {
            var currentAccount = iFacultyservice.FindById(id);
            if (file != null && file.Length > 0)
            {//delete file
                var photoNameToDelete = iFacultyservice.FindPhoto(id);
                var pathForDelete = Path.Combine(webHostEnvironment.WebRootPath, "img", photoNameToDelete);
                System.IO.File.Delete(pathForDelete);
                //--------------------------------------------
                var newFileName = new UploadHelper().GenerateFileName(file.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img", newFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                currentAccount.Photo = newFileName;
            }
            currentAccount.Dob = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            currentAccount.Status = faculty.Status;
            currentAccount.Description = faculty.Description;
            currentAccount.Name = faculty.Name;
            iFacultyservice.Edit(currentAccount);
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            ViewBag.Faculties = iFacultyservice.Search(keyword);
            return View("Index");
        }

        [HttpGet]
        [Route("listfacsub/{id}")]
        public IActionResult listFacsub(int id)
        {
            var facsub = iFacultyservice.FacSub(id);
            TempData["idfaculty"] = id;
            return View(facsub);

        }

        [HttpGet]
        [Route("addfacsub")]
        public IActionResult addFacsub()
        {
            if (TempData["idfaculty"] != null)
            {
                ViewBag.IdFaculty = Convert.ToInt32(TempData["idfaculty"].ToString());
            }

            var subject = (from sub in _db.Subjects
                           select new Course()
                           {
                               Name = sub.Name,
                               Id = sub.Id
                           }).ToList();
            ViewBag.listofSubject = subject;
            return View(new FacultySubjectEntity());
        }

        [HttpPost]
        [Route("addfacsub")]
        public async Task<IActionResult> accFacsub(FacultySubjectEntity facsubE, string fid)
        {
            var facId = Convert.ToInt32(fid);
            var facsub = new FacultySubject
            {
                FacultyId = facId,
                SubjectId = facsubE.SubjectId
            };
            var facultyReturn = await iFacultyservice.addFacsub(facsub);
            var facId2 = facultyReturn;
            return RedirectToAction("listFacsub", "faculty", new { area = "admin", id = facId2 });
        }
    }
}
