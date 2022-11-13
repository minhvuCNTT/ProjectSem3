using Pro3MVC.Helper;
using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Models;
using Pro3MVC.Services;
using System.Diagnostics;
using System.Globalization;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/account")]
    public class AccountController : Controller
    {
        private IStudentService iStudentservice;
        private IWebHostEnvironment webHostEnvironment;
        private IAccountService accountService;
        public AccountController(IStudentService _iStudentservice, IWebHostEnvironment _webHostEnvironment, IAccountService accountService)
        {
            iStudentservice = _iStudentservice;
            webHostEnvironment = _webHostEnvironment;
            this.accountService = accountService;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Accounts = iStudentservice.listStudent();
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
            return View(new Account());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Account accStus, string birthday, IFormFile file)
        {
            if (accountService.IsExist(accStus.Username.Trim()))
            {
                ViewBag.error = "Username exist!";
                return View("add");
            }
            if (birthday != null)
            {
                var dateTimeBirthday = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                accStus.Dob = dateTimeBirthday.Date;
            }
            if (file != null && file.Length > 0)
            {
                var newFileName = new UploadHelper().GenerateFileName(file.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "img", newFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                accStus.Photo = newFileName;
            }
            else
            {
                accStus.Photo = "personavatar.png";
            }
            if (accStus != null)
            {
                accStus.Password = BCrypt.Net.BCrypt.HashPassword(accStus.Password);
                iStudentservice.Create(accStus);
            }

            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var account = iStudentservice.FindById(id);
            var x = account.Dob.ToString(); //10/21/2000 12:00:00 AM
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
            ViewBag.Birthday = parse.ToString("dd-MM-yyyy");
            return View(account);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Account account, string birthday, IFormFile file)
        {
            var currentAccount = iStudentservice.FindById(id);
            if (file != null && file.Length > 0)
            {
                //delete file
                var photoNameToDelete = iStudentservice.FindPhoto(id);
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
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentAccount.Dob = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            currentAccount.Username = account.Username;
            currentAccount.Status = account.Status;
            currentAccount.Fullname = account.Fullname;
            currentAccount.Email = account.Email;
            iStudentservice.Edit(currentAccount);
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            ViewBag.Accounts = iStudentservice.Search(keyword);
            return View("Index");
        }
    }
}