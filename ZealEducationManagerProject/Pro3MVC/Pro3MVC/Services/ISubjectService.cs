using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface ISubjectService
    {

        public List<SubjectEntity> show();
        public List<SubjectEntity> Search(string keyword);
        public List<Subject> listSubject();
        public Task addSubject(Subject subM);

        //------student-----
        public List<Subject> FindSubjectsByCourseId(int courseId);
    }
}
