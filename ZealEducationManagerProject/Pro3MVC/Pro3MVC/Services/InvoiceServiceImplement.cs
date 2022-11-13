using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class InvoiceServiceImplement : IInvoiceService
    {
        private readonly DatabaseContext db;
        public InvoiceServiceImplement(DatabaseContext db)
        {
            this.db = db;
        }

        public bool Create(Invoice invoice)
        {
            try
            {
                db.Invoices.Add(invoice);
                return db.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CreateRelationshipForStudentAndCourse(AccountCourse accountCourse, List<AccountSubject> accountSubjects)
        {
            try
            {
                db.AccountCourses.Add(accountCourse);
                db.AccountSubjects.AddRange(accountSubjects);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Invoice> FindAll()
        {
            return db.Invoices.ToList();
        }

        public List<Invoice> SearchInvoiceByCourseName(string courseName)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                return this.FindAll();
            }
            else
            {
                return db.Invoices.Where(i => i.Course.Name.ToLower().Contains(courseName.ToLower())).ToList();
            }
        }
    }
}