using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Entities;
using Pro3MVC.Models;
using Pro3MVC.Services;
using System.Globalization;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/course")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseSer;
        private readonly IEnquiryService _enquirySer;
        private DatabaseContext _db;

        public CourseController(ICourseService courseSer, DatabaseContext db, IEnquiryService enquirySer)
        {
            _courseSer = courseSer;
            _db = db;
            _enquirySer = enquirySer;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.listCourse = await _courseSer.findAll();
            
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View(new CourseEntity());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(CourseEntity courseEntity, string startDate, string endDate)
        {

            var date = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var date2 = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (courseEntity.Fee < 0)
            {
                TempData["exists"] = "Money can't be negative!!!";
                return View("add");
            }
            else if (date < DateTime.Now)
            {
                TempData["valid"] = "Cannot add past date !!";
                return View("add");
            }
            else if (date2 < DateTime.Now)
            {
                TempData["valid"] = "Cannot add past date !!";
                return View("add");
            }
            else if (date >= date2)
            {
                TempData["valid"] = "Start day can not be greater than or equal end day !!!";
                return View("add");
            }
            else
            {
                //courseEntity.endDate = date2.Date;
                var course = new Course
                {
                    Id = courseEntity.Id,
                    Name = courseEntity.Name,
                    Fee = courseEntity.Fee,
                    Status = Convert.ToByte(courseEntity.Status),
                    StartDate = date.Date,
                    EndDate = date2.Date
                };
                await _courseSer.Create(course);
                return RedirectToAction("index");
            }

        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var course = await _courseSer.UpdateFind(id);


            if (DateTime.Now > course.startDate && DateTime.Now < course.endDate)
            {
                TempData["checkDate"] = "The course is happening cannot be updated";
                return RedirectToAction("index");
            }
            else if(course.endDate < DateTime.Now && course.startDate < DateTime.Now)
            {
                TempData["checkDate"] = "The course has ended !!!";
                return RedirectToAction("index");
            }
            else
            {
                var start = course.startDate.ToString();
                start = start.Replace(" 12:00:00 AM", "");
                var end = course.endDate.ToString();
                end = end.Replace(" 12:00:00 AM", "");

                if (start.IndexOf("/") == 1)
                {
                    start = start.Insert(0, "0");
                }
                if (start.LastIndexOf("/") == 4)
                {
                    start = start.Insert(3, "0");
                }

                if (end.IndexOf("/") == 1)
                {
                    end = end.Insert(0, "0");
                }
                if (end.LastIndexOf("/") == 4)
                {
                    end = end.Insert(3, "0");
                }
                var parse = DateTime.ParseExact(start, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var parse2 = DateTime.ParseExact(end, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                ViewBag.startDate = parse.ToString("dd/MM/yyyy");
                ViewBag.endDate = parse2.ToString("dd/MM/yyyy");
                return View(course);
            }
            
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, CourseEntity courseEntity, string startDate, string endDate)
        {
            var date = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var date2 = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (date < DateTime.Now)
            {
                TempData["valid"] = "Cannot add past date !!!";
                return RedirectToAction("update", "course", new { area = "admin", id = id });
            }
            else if (date2 < DateTime.Now)
            {
                TempData["valid"] = "Cannot add past date !!!";
                return RedirectToAction("update", "course", new { area = "admin", id = id });
            }
            else if (date >= date2)
            {
                TempData["valid"] = "Start day can not be greater than or equal end day !!!";
                return RedirectToAction("update", "course", new { area = "admin", id = id });
            }
            else
            {
                var mapCourse = await _courseSer.UpdateById(id);
                mapCourse.Name = courseEntity.Name;
                mapCourse.Fee = courseEntity.Fee;
                mapCourse.Status = courseEntity.Status;
                mapCourse.StartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                mapCourse.EndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                await _courseSer.Update(mapCourse);
                return RedirectToAction("index");
            }
        }

        /*[HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseSer.Delete(id);
            if (result)
            {
                TempData["success"] = "Delete successfully.";
                return RedirectToAction("index");
            }
            else
            {
                TempData["msg"] = "Can not delete!!!!";
                return RedirectToAction("index");

            }
        }*/

        [HttpGet]
        [Route("enquiry/add")]
        public async Task<IActionResult> AddEnquiry()
        {
            if (TempData["idcourse"] != null)
            {
                ViewBag.IdCourse = Convert.ToInt32(TempData["idcourse"].ToString());
            }
            return View(new EnquiryEntity());
        }

        [HttpPost]
        [Route("enquiry/add")]
        public async Task<IActionResult> AddEnquiry(EnquiryEntity enquiryEntity, string cid)
        {
            var courseId = Convert.ToInt32(cid);
            var enquiry = new Enquiry
            {
                Id = enquiryEntity.IdE,
                Answer = enquiryEntity.Answer,
                Question = enquiryEntity.Question,
                CourseId = courseId
            };
            var enquiryReturned = await _courseSer.addEnquiry(enquiry);
            var courseId2 = enquiryReturned;
            return RedirectToAction("listEnquiry", "course", new { area = "admin", id = courseId2 });
        }

        [HttpGet]
        [Route("enquiry/list/{id}")]
        public IActionResult listEnquiry(int id)
        {
            var enq = _courseSer.listEnquiry(id);
                TempData["idcourse"] = id;
                
            return View(enq);
        }

        [HttpGet]
        [Route("enquiry/update/{id}")]
        public async Task<IActionResult> updateEnquiry(int id)
        {
            var enq = await _enquirySer.findE(id);
            return View(enq);
        }

        [HttpPost]
        [Route("enquiry/update/{id}")]
        public async Task<IActionResult> updateEnquiry(int id, EnquiryEntity enquiryEntity)
        {
            var mapEnquiry = await _enquirySer.findbyidE(id);
            mapEnquiry.Id = enquiryEntity.IdE;
            mapEnquiry.CourseId = enquiryEntity.CourseId;
            mapEnquiry.Question = enquiryEntity.Question;
            mapEnquiry.Answer = enquiryEntity.Answer;

            var update = await _enquirySer.updateEnquiry(mapEnquiry);
            return RedirectToAction("listEnquiry", "course", new { area = "admin", id = update.CourseId });
        }

        [HttpGet]
        [Route("enquiry/delete/{id}")]
        public async Task<IActionResult> DeleteE(int id)
        {
            var idc = await _enquirySer.findbyidE(id);
            var result = await _enquirySer.Delete(idc);

            if (result)
            {
                TempData["msg"] = "Delete successfully.";
                return RedirectToAction("listEnquiry", "course", new { area = "admin", id = idc.CourseId });
            }
            else
            {
                return RedirectToAction("listEnquiry", "course", new { area = "admin", id = idc.CourseId });
            }

        }
    }
}
