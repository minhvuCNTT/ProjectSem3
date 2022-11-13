using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface ICourseService
    {
        public Task<List<CourseEntity>> findAll();
        public Task Create(Course courseM);
        public Task Update(Course courseM);
        public Task<CourseEntity> UpdateFind(int id);
        public Task<Course> UpdateById(int id);
        public Task<bool> Delete(int id);
        public Task<int> addEnquiry(Enquiry enE);
        public List<EnquiryEntity> listEnquiry(int id);

        //-----PayPal--------
        public List<Course> listCourse();

        public List<CourseDetail> Detail(int id);

        public CourseEntity FindCourse(int idCourse);

        //---------Student---------
        public Course FindByName(string name);

        public List<ScoreEntity> Transcript(int id);
        public List<Course> ListCourse(int idStudent);
        public List<Course> GetAllCoursesOfStudent(int idStudent);
        public Account GetById(int idStudent);
        public List<Course> CourseNoRepeat(int idStudent);

    }
}
