using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IInvoiceService
    {
        public bool Create(Invoice invoice);
        public bool CreateRelationshipForStudentAndCourse(AccountCourse accountCourse, List<AccountSubject> accountSubject);
        public List<Invoice> FindAll();
        public List<Invoice> SearchInvoiceByCourseName(string courseName);
    }
}
