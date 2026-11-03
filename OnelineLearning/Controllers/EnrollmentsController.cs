using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Models;

namespace OnelineLearning.Controllers
{
    public class EnrollmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
