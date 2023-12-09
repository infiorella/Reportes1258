using Microsoft.AspNetCore.Mvc;

namespace Colegio1258.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }
    }
}
