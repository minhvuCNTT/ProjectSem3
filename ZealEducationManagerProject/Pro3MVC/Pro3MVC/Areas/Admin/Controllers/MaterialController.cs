using Microsoft.AspNetCore.Mvc;

namespace Pro3MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/material")]
    public class MaterialController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }
    }
}
