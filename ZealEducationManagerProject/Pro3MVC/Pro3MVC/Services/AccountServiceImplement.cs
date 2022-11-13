using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class AccountServiceImplement : IAccountService
    {
        private DatabaseContext db;
        public AccountServiceImplement(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<dynamic> FindAll()
        {
            return await db.Accounts.Select(a => new
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.Password,
                Dob = a.Dob,
                Email = a.Email,
                Photo = a.Photo,
                Status = a.Status,
                Role = a.Role,
                FullName = a.Fullname
            }).ToListAsync();
        }

        public async Task<dynamic> Find(int id)
        {
            return await db.Accounts.Select(a => new
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.Password,
                Dob = a.Dob,
                Email = a.Email,
                Photo = a.Photo,
                Status = a.Status,
                Role = a.Role,
                FullName = a.Fullname
            }).SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<dynamic> Find(string username)
        {
            return await db.Accounts.Select(a => new
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.Password,
                Dob = a.Dob,
                Email = a.Email,
                Photo = a.Photo,
                Status = a.Status,
                Role = a.Role,
                FullName = a.Fullname
            }).SingleOrDefaultAsync(a => a.Username == username.Trim());
        }

        public async Task<dynamic> Find(bool status)
        {
            return await db.Accounts.Where(a => a.Status == status).Select(a => new
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.Password,
                Dob = a.Dob,
                Email = a.Email,
                Photo = a.Photo,
                Status = a.Status,
                Role = a.Role,
                FullName = a.Fullname
            }).ToListAsync();
        }

        public bool IsActive(string username)
        {
            return db.Accounts.SingleOrDefault(a => a.Username == username.Trim()).Status;
        }

        public async Task<AccountEntity> Login(string username, string password)
        {
            var user = username.Trim();
            var account = await db.Accounts.SingleOrDefaultAsync(a => a.Username == user);
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password.Trim(), account.Password) && account.Status == true)
                {
                    var accountEntity = new AccountEntity
                    {
                        Id = account.Id,
                        Username = account.Username,
                        Password = account.Password,
                        Dob = account.Dob,
                        Email = account.Email,
                        Photo = account.Photo,
                        Status = account.Status,
                        Role = account.Role,
                        Fullname = account.Fullname,
                    };
                    return accountEntity;
                }
            }
            return null;
        }

        public List<Course> GetAllCoursesOfStudent(int idStudent)
        {
            var CourseIds = db.AccountCourses.Where(ac => ac.AccountId == idStudent).Select(r => r.CourseId).ToList();
            List<Course> coursesOfStudent = new List<Course>();
            foreach (var id in CourseIds)
            {
                var course = db.Courses.SingleOrDefault(c => c.Id == id);
                coursesOfStudent.Add(course);
            }
            return coursesOfStudent;
        }

        public Account GetById(int idStudent)
        {
            return db.Accounts.SingleOrDefault(a => a.Id == idStudent);
        }

        public List<Account> listStudent()
        {
            return db.Accounts.Where(b => b.Role == "student").ToList();
        }
        public Account FindById(int id)
        {
            return db.Accounts.Find(id);
        }
        public string FindPhoto(int idAcc)
        {
            return db.Accounts.Find(idAcc).Photo;
        }
        public void Edit(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();

        }
        public bool IsExist(string username)
        {
           return db.Accounts.Any(a => a.Username == username);
        }

    }
}
