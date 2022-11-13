using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Pro3MVC.Models;
using Pro3MVC.Services;
using System.Globalization;
using Pro3MVC.Helper;

namespace Pro3MVC.Areas.Student.Controllers
{
    [Area("student")]
    [Route("student/profile")]
    public class ProfileController : Controller
    {
        private IAccountService iAccountService;
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext db;
        public ProfileController(IAccountService _iAccountService, IWebHostEnvironment _webHostEnvironment,
            DatabaseContext _db)
        {
            db = _db;
            iAccountService = _iAccountService;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            ViewBag.Account = iAccountService.FindById(id);
            ViewBag.Birthday = ViewBag.Account.Dob.ToString();
            var cut = ViewBag.Birthday.Replace(" 12:00:00 AM", "");
            if (cut.IndexOf("/") == 1)
            {
                cut = cut.Insert(0, "0");
            }
            if (cut.LastIndexOf("/") == 4)
            {
                cut = cut.Insert(3, "0");
            }
            var parse = DateTime.ParseExact(cut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            ViewBag.Birthday = parse.ToString("dd-MM-yyyy");
            ViewBag.Photo = ViewBag.Account.Photo;
            return View("index", ViewBag.Account);
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit()
        {
            try
            {
                var id = Convert.ToInt32(HttpContext.Session.GetString("id"));
                ViewBag.Account = iAccountService.FindById(id);
                ViewBag.Birthday = ViewBag.Account.Dob.ToString();
                var cut = ViewBag.Birthday.Replace(" 12:00:00 AM", "");
                if (cut.IndexOf("/") == 1)
                {
                    cut = cut.Insert(0, "0");
                }
                if (cut.LastIndexOf("/") == 4)
                {
                    cut = cut.Insert(3, "0");
                }
                var parse = DateTime.ParseExact(cut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                ViewBag.Birthday = parse.ToString("dd-MM-yyyy");
                ViewBag.Photo = ViewBag.Account.Photo;
                return View(ViewBag.Account);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(int id, Account account, string birthday, IFormFile file, string confirmpassword)
        {
            id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var currentAccount = iAccountService.FindById(id);
            if (!string.IsNullOrEmpty(account.Password) && !string.IsNullOrEmpty(confirmpassword))
            {
                if (account.Password == confirmpassword)
                {
                    currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                }
                else
                {
                    ViewBag.Error = "Wrong confirm password!";
                    ViewBag.Account = iAccountService.FindById(id);
                    ViewBag.Birthday = ViewBag.Account.Dob.ToString();
                    var cut = ViewBag.Birthday.Replace(" 12:00:00 AM", "");
                    if (cut.IndexOf("/") == 1)
                    {
                        cut = cut.Insert(0, "0");
                    }
                    if (cut.LastIndexOf("/") == 4)
                    {
                        cut = cut.Insert(3, "0");
                    }
                    var parse = DateTime.ParseExact(cut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    ViewBag.Birthday = parse.ToString("dd-MM-yyyy");
                    ViewBag.Photo = ViewBag.Account.Photo;
                    return View(ViewBag.Account);
                }
            }
            if (file != null && file.Length > 0)
            {
                //delete file
                var photoNameToDelete = iAccountService.FindPhoto(id);
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
            currentAccount.Email = account.Email;
            currentAccount.Fullname = account.Fullname;
            iAccountService.Edit(currentAccount);
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("changepass")]
        public IActionResult changePassword()
        {
            return View();
        }
    }
}