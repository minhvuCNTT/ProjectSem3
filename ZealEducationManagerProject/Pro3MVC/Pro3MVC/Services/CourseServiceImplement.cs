using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class CourseServiceImplement : ICourseService
    {
        private DatabaseContext _db;
        public CourseServiceImplement(DatabaseContext db)
        {
            _db = db;
        }


        public async Task Create(Course courseM)
        {
            await _db.Courses.AddAsync(courseM);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _db.Courses.Remove(await UpdateById(id));
                await _db.SaveChangesAsync();
                return true;
            } 
            catch
            {
                return false;
            }
            
        }

        public async Task<List<CourseEntity>> findAll()
        {

            var courses = await _db.Courses.Select(c => new CourseEntity
            {
                Id = c.Id,
                Name = c.Name,
                Fee = c.Fee,
                startDate = c.StartDate,
                endDate = c.EndDate
            }).ToListAsync();
            return courses;
        }

        public async Task Update(Course courseM)
        {
            _db.Entry(courseM).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<CourseEntity> UpdateFind(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            return new CourseEntity
            {
                Id = course.Id,
                Name = course.Name,
                Fee = course.Fee,
                Status = course.Status,
                startDate = course.StartDate,
                endDate = course.EndDate
            };
        }

        public async Task<Course> UpdateById(int id)
        {
            return await _db.Courses.FindAsync(id);
        }

        public async Task<int> addEnquiry(Enquiry enE)
        {
            await _db.Enquiries.AddAsync(enE);
            await _db.SaveChangesAsync();
            return (int) enE.CourseId;
        }

        public List<EnquiryEntity> listEnquiry(int id)
        {

            var enq = from e in _db.Enquiries
                       where
                       e.CourseId == id
                       select new EnquiryEntity
                       {
                           IdE = e.Id,
                           CourseId = e.CourseId,
                           Answer = e.Answer,
                           Question = e.Question
                       };
            var list = enq.ToList();
            return list;
        }



        /*public async Task<List<Enquiry>> listEnquiry()
        {
            var course = await _db.Courses.FindAsync(id);
            return new CourseEntity
            {
                Id = course.Id,
                Name = course.Name,
                Fee = course.Fee,
                Status = course.Status
            };
        }*/

        //-------PayPal------
        public List<Course> listCourse()
        {
            return _db.Courses.Where(c=>DateTime.Now < c.StartDate).ToList();
        }

        public List<CourseDetail> Detail(int id)
        {
            var result = (from Batch_Subject in _db.BatchSubjects
                          where
                            Batch_Subject.Batch.Course.Id == id
                          select new CourseDetail
                          {
                              BatchName = Batch_Subject.Batch.Name,
                              CourseId = (int?)Batch_Subject.Batch.CourseId,
                              BatchId = Batch_Subject.BatchId,
                              SubjectId = Batch_Subject.SubjectId,
                              SubjectName = Batch_Subject.Subject.Name,
                              SubjectSessions = (int?)Batch_Subject.Subject.Sessions,
                              CourseName = Batch_Subject.Batch.Course.Name,
                              StartDate = Batch_Subject.Batch.Course.StartDate,
                              EndDate = Batch_Subject.Batch.Course.EndDate


                          }).ToList();
            return result;
        }

        public CourseEntity FindCourse(int idCourse)
        {
            return _db.Courses.Where(c => c.Id == idCourse).Select(c => new CourseEntity
            {
                Id = c.Id,
                Name = c.Name,
                Fee = c.Fee,
                Status = c.Status
            }).FirstOrDefault();
        }

        public List<ScoreEntity> Transcript(int id)
        {
            var result = (from Account_Subject in _db.AccountSubjects
                          join Batch in _db.Batches on Account_Subject.Account.Id equals Batch.Id
                          where Account_Subject.Account.Id == id
                          select new ScoreEntity
                          {
                              Account_Id = id,
                              Username = Account_Subject.Account.Username,
                              Course_Name = Batch.Course.Name,
                              Batch_Name = Batch.Name,
                              Course_Id = Batch.Course.Id,
                              Subject_Name = Account_Subject.Subject.Name,
                              Score = (decimal)Account_Subject.Score
                          }).ToList();
            return result;
        }
        public List<Course> ListCourse(int idStudent)
        {
            return (from Account_Course in _db.AccountCourses
                    where
                      Account_Course.Account.Id == idStudent
                    select new Course
                    {
                        Id = Account_Course.Course.Id,
                        Name = Account_Course.Course.Name,
                        Fee = Account_Course.Course.Fee,
                        Status = Account_Course.Course.Status
                    }).ToList();
        }
        public Course FindByName(string name)
        {
            return _db.Courses.SingleOrDefault(a => a.Name == name);
        }
        public List<Course> GetAllCoursesOfStudent(int idStudent)
        {
            var CourseIds = _db.AccountCourses.Where(ac => ac.AccountId == idStudent).Select(r => r.CourseId).ToList();
            List<Course> coursesOfStudent = new List<Course>();
            foreach (var id in CourseIds)
            {
                var course = _db.Courses.SingleOrDefault(c => c.Id == id);
                coursesOfStudent.Add(course);
            }
            return coursesOfStudent;
        }
        public Account GetById(int idStudent)
        {
            return _db.Accounts.SingleOrDefault(a => a.Id == idStudent);
        }

        public List<Course> CourseNoRepeat(int idStudent)
        {
            var allCourse = listCourse();
            List<Course> CoursesRepeated = new List<Course>();
            List<AccountCourse> accountCoursesRepeated = new List<AccountCourse>();

            foreach (var course in allCourse)
            {
                foreach (var accountCourse in course.AccountCourses)
                {
                    accountCoursesRepeated.AddRange(_db.AccountCourses.Where(ac => ac.AccountId == idStudent));
                }
            }

            foreach (var item in accountCoursesRepeated)
            {
                allCourse.Remove(_db.Courses.SingleOrDefault(c => c.Id == item.CourseId));
            }
            return allCourse;
        }
    }
}
