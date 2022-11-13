using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class EnquiryServiceImplement : IEnquiryService
    {
        private DatabaseContext _db;
        public EnquiryServiceImplement (DatabaseContext db)
        {
            _db = db;
        }

        public async Task<EnquiryEntity> findE(int id)
        {
            var enq = await _db.Enquiries.FindAsync(id);
            return new EnquiryEntity
            {
                IdE = enq.Id,
                Question = enq.Question,
                Answer = enq.Answer,
                CourseId = enq.CourseId,
            };
        }

        public async Task<Enquiry> findbyidE(int id)
        {
            return await _db.Enquiries.FindAsync(id);
        }

        public async Task<Enquiry> updateEnquiry(Enquiry enM)
        {
            _db.Entry(enM).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return enM;
        }

        public async Task<bool> Delete(Enquiry en)
        {
            try
            {
                _db.Enquiries.Remove(en);
                await _db.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public List<Enquiry> listEnquiry(int courseId)
        {
            return _db.Enquiries.Where(e => e.CourseId == courseId).ToList();
        }
    }
}
