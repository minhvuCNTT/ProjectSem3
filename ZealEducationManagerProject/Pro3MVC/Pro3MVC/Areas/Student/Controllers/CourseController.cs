using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Entities;
using Pro3MVC.PayPal;
using Pro3MVC.Services;
using System.Diagnostics;
using Newtonsoft.Json;
using Pro3MVC.Models;

namespace Pro3MVC.Areas.Student.Controllers
{
    [Area("student")]
    [Route("student/course")]
    public class CourseController : Controller
    {
        private ICourseService iCourseService;
        private IWebHostEnvironment webHostEnvironment;
        private IConfiguration configuration;
        private IInvoiceService invoiceService;
        private ISubjectService subjectService;
        public CourseController(ICourseService _iCourseService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration, IInvoiceService invoiceService, ISubjectService subjectService)
        {
            iCourseService = _iCourseService;
            webHostEnvironment = _webHostEnvironment;
            configuration = _configuration;
            this.invoiceService = invoiceService;
            this.subjectService = subjectService;
        }
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (HttpContext.Session.GetString("cart") != null)
                {
                    HttpContext.Session.Remove("cart");
                }
                if (HttpContext.Session.GetString("username") == null)
                {
                    ViewBag.Courses = iCourseService.listCourse();
                    return View();
                }
                else
                {
                    ViewBag.Courses = iCourseService.CourseNoRepeat(Convert.ToInt32(HttpContext.Session.GetString("id")));

                    return View();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                ViewBag.CourseDetail = iCourseService.Detail(id);
                //---paypal-----
                var course = iCourseService.FindCourse(id);
                ViewBag.course = course;
                PayPalConfig payPalConfig = PayPalService.getPayPalConfig(configuration);
                ViewBag.payPalConfig = payPalConfig;
                //------enquiry----
                var enquiry = iCourseService.listEnquiry(id);
                ViewBag.enquiries = enquiry;

                //--------------cart----------------
                var cart = course;
                if (HttpContext.Session.GetString("cart") == null)
                {
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                }

                return View();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Route("success")]
        public IActionResult Success()
        {
            //var result =  PDTHolder.Success(Request.Query["tx"].ToString(), configuration, Request);
            //Debug.WriteLine($"Lastname: {result.PayerLastName}");
            var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(HttpContext
                .Session.GetString("cart"));
            //add new invoice
            var invoice = new Invoice
            {
                Name = $"{HttpContext.Session.GetString("username")}_buy_{courseEntity.Name}",
                AccountId = Convert.ToInt32(HttpContext.Session.GetString("id")),
                CourseId = courseEntity.Id,
                Payment = "Paypal",
                Created = DateTime.Now,
                Total = courseEntity.Fee
            };
            //add invoice
            invoiceService.Create(invoice);
            //add relationship
            //add account_subject
            List<Subject> subjects = subjectService.FindSubjectsByCourseId(courseEntity.Id);
            List<AccountSubject> accountSubjects = new List<AccountSubject>();
            foreach (var subject in subjects)
            {
                var accountSubject = new AccountSubject
                {
                    AccountId = Convert.ToInt32(HttpContext.Session.GetString("id")),
                    SubjectId = subject.Id,
                    Score = null,
                    CourseId = courseEntity.Id
                };
                accountSubjects.Add(accountSubject);
            }
            //add account_course
            var accountCourse = new AccountCourse
            {
                AccountId = Convert.ToInt32(HttpContext.Session.GetString("id")),
                CourseId = courseEntity.Id,
                StartDate = DateTime.Now,
                Fee = courseEntity.Fee,
                Status = true,
                Description = null
            };

            invoiceService.CreateRelationshipForStudentAndCourse(accountCourse, accountSubjects);

            HttpContext.Session.Remove("cart");

            return View();
        }

    }
}