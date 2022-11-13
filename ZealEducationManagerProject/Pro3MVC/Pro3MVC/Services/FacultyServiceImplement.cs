using Pro3MVC.Entities;
using Pro3MVC.Models;
using Pro3MVC.Services;

namespace Pro3MVC.Services
{
    public class FacultyServiceImplement : IFacultyService
    {
        private DatabaseContext db;
        public FacultyServiceImplement(DatabaseContext _db)
        {
            db = _db;
        }
        public List<Faculty> listFaculty()
        {
            return db.Faculties.ToList();
        }
        public bool Create(Faculty faculty)
        {
            try
            {
                var fac = new Faculty();
                fac.Name = faculty.Name;
                fac.Dob = faculty.Dob;
                fac.Description = faculty.Description;
                fac.Photo = faculty.Photo;
                fac.Status = true;
                db.Faculties.Add(fac);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public Faculty FindById(int id)
        {
            return db.Faculties.Find(id);
        }
        public void Edit(Faculty faculty)
        {
            db.Entry(faculty).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

        }
        public string FindPhoto(int idFac)
        {
            return db.Faculties.Find(idFac).Photo;
        }
        public List<Faculty> Search(string keyword)
        {
            return string.IsNullOrEmpty(keyword) ? listFaculty() : db.Faculties.Where(a => a.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }

        public List<FacultySubjectEntity> FacSub(int id)
        {
            var facsub = from fs in db.FacultySubjects
                         where fs.FacultyId == id
                         select new FacultySubjectEntity
                         {
                             Id = fs.Id,
                             FacultyId = fs.FacultyId,
                             SubjectName = fs.Subject.Name
                         };
            var list = facsub.ToList();
            return list;
        }

        public async Task<int> addFacsub(FacultySubject facsubM)
        {
            await db.FacultySubjects.AddAsync(facsubM);
            await db.SaveChangesAsync();
            return (int)facsubM.FacultyId;
        }
    }
}