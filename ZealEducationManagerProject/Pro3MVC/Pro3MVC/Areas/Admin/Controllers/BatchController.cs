using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/batch")]
    public class BatchController : Controller
    {
        private DatabaseContext _db;
        private readonly IBatchService _batchSer;

        public BatchController(IBatchService batchSer, DatabaseContext db)
        {
            _batchSer = batchSer;
            _db = db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var list = _batchSer.findAll();
            return View(list);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var combobox = (from course in _db.Courses
                            where DateTime.Now < course.StartDate
                            select new Course()
                            {
                                Name = course.Name,
                                Id = course.Id
                            }).ToList();
            /*combobox.Insert(0, new Course()
            {
                Name = "------Select course------",
                Id = 0
            });*/

            ViewBag.ListofCourse = combobox;
            return View(new BatchEntity());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(BatchEntity batchE)
        {
            Batch batch = _db.Batches.Where(x => x.Name == batchE.Name && x.CourseId == batchE.CourseId).FirstOrDefault();

            if(batch != null)
            {
                TempData["check"] = "The batch name in the course already exists!!!";
                return RedirectToAction("add");
            }
            else
            {
                var batches = new Batch
                {
                    Id = batchE.Id,
                    Name = batchE.Name,
                    CourseId = batchE.CourseId,
                    Status = Convert.ToBoolean(batchE.Status)
                };
                await _batchSer.Add(batches);
                return RedirectToAction("index");
            }
            
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var combobox = (from course in _db.Courses
                            where course.Status == 1
                            select new Course()
                            {
                                Name = course.Name,
                                Id = course.Id
                            }).ToList();
            /*combobox.Insert(0, new Course()
            {
                Name = "------Select course------",
                Id = 0
            });*/

            ViewBag.ListofCourse = combobox;

            var batch = await _batchSer.Find(id);
            return View(batch);
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, BatchEntity batchE)
        {
            var mapBatch = await _batchSer.FindById(id);
            mapBatch.Id = batchE.Id;
            mapBatch.Name = batchE.Name;
            mapBatch.Course.Name = batchE.courseName;
            mapBatch.Status = mapBatch.Status;
            await _batchSer.Update(mapBatch);

            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _batchSer.Delete(id);

            if(result)
            {

                TempData["msg"] = "Delete successfully.";
                return RedirectToAction("index");
            }
            else
            {
                TempData["fail"] = "Can not delete!!!";
                return RedirectToAction("index");
            }
        }

        [HttpGet]
        [Route("addSubject")]
        public async Task<IActionResult> AddSubject()
        {
            if (TempData["idbatch"] != null)
            {
                ViewBag.IdBatch = Convert.ToInt32(TempData["idbatch"].ToString());
            }

            //var subject = (from sub in _db.Subjects
            //                select new Course()
            //                {
            //                    Name = sub.Name,
            //                    Id = sub.Id
            //                }).ToList();
            //ViewBag.listofSubject = subject;

            //-------------------
            var idBatch = Convert.ToInt32(TempData["idbatch"].ToString());

            List<Subject> repeatedSubjects = new List<Subject>();
            List<BatchSubject> batchSubjectsReapeated = _db.BatchSubjects.Where(bs => bs.BatchId == idBatch).ToList();
            List<Subject> allSubjects = _db.Subjects.ToList();

            foreach (var item in batchSubjectsReapeated)
            {
                allSubjects.Remove(_db.Subjects.SingleOrDefault(s => s.Id == item.SubjectId));
            }
            ViewBag.listofSubject = allSubjects;

            return View(new BatchSubjectEntity());
        }

        [HttpPost]
        [Route("addSubject")]
        public async Task<IActionResult> AddSubject(BatchSubjectEntity bsEntity, string bid)
        {
            var batchId = Convert.ToInt32(bid);
            var batch = new BatchSubject
            {
                BatchId = batchId,
                SubjectId = bsEntity.SubjectId
            };
            var batchReturn = await _batchSer.AddSubject(batch);
            var batchId2 = batchReturn;
            return RedirectToAction("listSubject", "batch", new { area = "admin", id = batchId2 });
        }

        [HttpGet]
        [Route("subject/list/{id}")]
        public IActionResult listSubject(int id)
        {
            var sub = _batchSer.listSubject(id);
            TempData["idbatch"] = id;
            return View(sub);
        }
    }
}
