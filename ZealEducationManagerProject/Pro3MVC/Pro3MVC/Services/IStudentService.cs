using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IStudentService
    {
        public List<Account> listStudent();
        public bool Create(Account accStu);
        public void Edit(Account accStu);
        public Account FindById(int id);
        public string FindPhoto(int idAcc);
        public void Delete(int id);
        public List<Account> Search(string keyword);
    }
}