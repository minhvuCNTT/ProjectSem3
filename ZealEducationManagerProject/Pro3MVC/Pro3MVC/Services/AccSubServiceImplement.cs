using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class AccSubServiceImplement : IAccSubService
    {
        private DatabaseContext _db;
        public AccSubServiceImplement(DatabaseContext db)
        {
            _db = db;
        }

        public List<AccountSubjectEntity> detail(int id)
        {
            var stu = from accsub in _db.AccountSubjects
                      where accsub.SubjectId == id 
                      select new AccountSubjectEntity
                      {
                          Id = accsub.Id,
                          subId = accsub.SubjectId,
                          fullname = accsub.Account.Fullname,
                          Score = accsub.Score,

                      };

            var list = stu.ToList();
            return list;
        }

        public async Task<AccountSubject> finById(int id)
        {
            return await _db.AccountSubjects.FindAsync(id);
        }

        public async Task<AccountSubjectEntity> find(int id)
        {
            var acc = await _db.AccountSubjects.FindAsync(id);
            return new AccountSubjectEntity
            {
                Id = acc.Id,
                fullname = acc.Account.Fullname,
                Score = acc.Score,
            };

        }

        public async Task updateD(AccountSubject accsubM)
        {
            _db.Entry(accsubM).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
