using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IAccSubService
    {
        public List<AccountSubjectEntity> detail(int id);

        public Task updateD(AccountSubject accsubM);

        public Task<AccountSubjectEntity> find(int id);

        public Task<AccountSubject> finById(int id);
    }
}
