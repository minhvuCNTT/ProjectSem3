using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;
using System.Linq;

namespace Pro3MVC.Services
{
    public class SubjectServiceImplement : ISubjectService
    {
        private DatabaseContext _db;

        public SubjectServiceImplement(DatabaseContext db)
        {
            _db = db;
        }

        public List<SubjectEntity> show()
        {
            var subject = from Subject in _db.Subjects
                          from Batch_Subject in _db.BatchSubjects
                          from Course in _db.Courses
                          from Batch in _db.Batches
                          where
                            Subject.Id == Batch_Subject.SubjectId &&
                            Batch.Id == Batch_Subject.BatchId &&
                            Course.Id == Batch.CourseId
                          select new SubjectEntity
                          {
                              Id = Subject.Id,
                              Name = Batch_Subject.Subject.Name,
                              Sessions = Subject.Sessions,
                              courseName = Batch.Course.Name,
                              batchName = Batch_Subject.Batch.Name
                          };
            var list = subject.ToList();
            return list;
        }

        public List<SubjectEntity> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {

                var subject = from Subject in _db.Subjects
                              from Batch_Subject in _db.BatchSubjects
                              from Course in _db.Courses
                              from Batch in _db.Batches
                              where
                                Subject.Id == Batch_Subject.SubjectId &&
                                Batch.Id == Batch_Subject.BatchId &&
                                Course.Id == Batch.CourseId
                              select new SubjectEntity
                              {
                                  Id = Subject.Id,
                                  Name = Batch_Subject.Subject.Name,
                                  Sessions = Subject.Sessions,
                                  courseName = Batch.Course.Name,
                                  batchName = Batch_Subject.Batch.Name
                              };
                var list = subject.ToList();
                return list;
            }
            else
            {
                var subject = from Subject in _db.Subjects
                              from Batch_Subject in _db.BatchSubjects
                              from Course in _db.Courses
                              from Batch in _db.Batches
                              where
                                Subject.Id == Batch_Subject.SubjectId &&
                                Batch.Id == Batch_Subject.BatchId &&
                                Course.Id == Batch.CourseId &&
                                Subject.Name.ToLower().Contains(keyword)
                              select new SubjectEntity
                              {
                                  Id = Subject.Id,
                                  Name = Batch_Subject.Subject.Name,
                                  Sessions = Subject.Sessions,
                                  courseName = Batch.Course.Name,
                                  batchName = Batch_Subject.Batch.Name
                              };
                var list = subject.ToList();
                return list;
            }
        }

        //-----student-----

        public List<Subject> FindSubjectsByCourseId(int courseId)
        {
            return _db.BatchSubjects
                .Where(Batch_Subject => (Batch_Subject.Batch.Course.Id == courseId))
                .Select(
                  Batch_Subject =>
                     new Subject
                     {
                         Id = Batch_Subject.Subject.Id,
                         Name = Batch_Subject.Subject.Name,
                         Sessions = Batch_Subject.Subject.Sessions,
                         Status = Batch_Subject.Subject.Status
                     }).ToList();
        }

        public List<Subject> listSubject()
        {
            return _db.Subjects.ToList();
        }

        public async Task addSubject(Subject subM)
        {
            await _db.Subjects.AddAsync(subM);
            await _db.SaveChangesAsync();
        }
    }
}
