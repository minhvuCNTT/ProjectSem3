using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IFacultyService
    {
        public List<Faculty> listFaculty();
        public bool Create(Faculty faculty);
        public void Edit(Faculty faculty);
        public Faculty FindById(int id);
        public string FindPhoto(int idFac);
        public List<Faculty> Search(string keyword);

        //------admin-------
        public List<FacultySubjectEntity> FacSub(int id);
        public Task<int> addFacsub(FacultySubject facsubM);
    }
}