using Microsoft.AspNetCore.Mvc;

namespace Pro3MVC.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public void SetAlert(string msg, string type)
        {
            TempData["AlertMessage"] = msg;
            switch (type)
            {
                case "success":
                    TempData["AlertType"] = "success"; break;
                case "warning":
                    TempData["AlertType"] = "warning";break;
                case "error":
                    TempData["AlertType"] = "error";break;
                default:
                    TempData["AlertType"] = "";break;
            }
        }
    }
}
