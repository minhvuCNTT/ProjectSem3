using Microsoft.AspNetCore.Mvc;
using Pro3MVC.Entities;
using Pro3MVC.Models;
using Pro3MVC.Services;

namespace Pro3MVC.Controllers
{
    [Route("security")]
    public class SecurityController : Controller
    {
        private IAccountService _accountService;
        public SecurityController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.msg = "Username or Password is empty!";
                return View("login");
            }
            AccountEntity accountEntity = await _accountService.Login(username.Trim(), password.Trim());
            if (accountEntity != null)
            {
                HttpContext.Session.SetString("id", accountEntity.Id.ToString());
                HttpContext.Session.SetString("username", accountEntity.Username);
                HttpContext.Session.SetString("fullname", accountEntity.Fullname);
                HttpContext.Session.SetString("role", accountEntity.Role);
                if (accountEntity.Role == "admin")
                {
                    return RedirectToAction("index", "dashboard", new { area = "admin" });
                }
                else
                {
                    return RedirectToAction("index", "home", new { area = "student" });
                }

            }
            ViewBag.msg = "Invalid account!";
            return View("login");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("fullname");
            HttpContext.Session.Remove("role");
            return RedirectToAction("login", "security");
        }
    }
}