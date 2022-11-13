using Pro3MVC.Models;
using Microsoft.EntityFrameworkCore;
using Pro3MVC.Services;

namespace Pro3MVC.Services
{
    public class StudentServiceImplement : IStudentService
    {
        private DatabaseContext db;
        public StudentServiceImplement(DatabaseContext _db)
        {
            db = _db;
        }
        public List<Account> listStudent()
        {
            return db.Accounts.Where(b => b.Role == "student").ToList();
        }
        public bool Create(Account accStu)
        {
            try
            {
                var account = new Account();
                account.Username = accStu.Username;
                account.Fullname = accStu.Fullname;
                account.Password = accStu.Password;
                account.Status = true;
                account.Email = accStu.Email;
                account.Dob = accStu.Dob;
                account.Photo = accStu.Photo;
                account.Role = "student";
                db.Accounts.Add(account);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public Account FindById(int id)
        {
            return db.Accounts.Find(id);
        }
        public void Edit(Account accStu)
        {
            db.Entry(accStu).State = EntityState.Modified;
            db.SaveChanges();

        }
        public string FindPhoto(int idAcc)
        {
            return db.Accounts.Find(idAcc).Photo;
        }
        public void Delete(int id)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Id == id);
            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
        }

        public List<Account> Search(string keyword)
        {
            return string.IsNullOrEmpty(keyword) ? listStudent() : db.Accounts.Where(a => a.Fullname.ToLower().Contains(keyword.ToLower()) && a.Role == "student").ToList();
        }
    }
}