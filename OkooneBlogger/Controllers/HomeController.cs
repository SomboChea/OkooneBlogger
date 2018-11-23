using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Helpers;

namespace OkooneBlogger.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin")]
        public IActionResult Admin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            return View();
        }

        [HttpGet("WebApi")]
        public IActionResult WebApi()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            return View();
        }
    }
}