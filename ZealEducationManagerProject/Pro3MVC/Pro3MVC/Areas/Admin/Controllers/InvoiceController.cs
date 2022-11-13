using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Services;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/invoice")]
    public class InvoiceController : Controller
    {
        private IInvoiceService invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.Invoices = invoiceService.FindAll();
            return View();
        }

        [Route("searchInvoice")]
        public IActionResult SearchInvoice(string keyword)
        {
            ViewBag.Invoices = invoiceService.SearchInvoiceByCourseName(keyword);
            ViewBag.Keyword = keyword;
            return View("Index");
        }
    }
}
