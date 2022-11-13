using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IAccountService
    {
        public Task<dynamic> FindAll();
        public Task<dynamic> Find(int id);
        public Task<dynamic> Find(string username);
        public Task<dynamic> Find(bool status);
        public bool IsActive(string username);
        public Task<AccountEntity> Login(string username, string password);
        //------profile----------
        public Account FindById(int id);
        public string FindPhoto(int idAcc);
        public void Edit(Account account);

        //-------student-------
        public List<Course> GetAllCoursesOfStudent(int idStudent);
        public Account GetById(int idStudent);
        public List<Account> listStudent();
        public bool IsExist(string username); 
    }
}
