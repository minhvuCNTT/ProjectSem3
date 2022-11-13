using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IEnquiryService
    {
        public Task<EnquiryEntity> findE(int id);
        public Task<Enquiry> findbyidE(int id);
        public Task<Enquiry> updateEnquiry(Enquiry enM);
        public Task<bool> Delete(Enquiry en);
        public List<Enquiry> listEnquiry(int courseId);
    }
}
