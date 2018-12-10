using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportStoreCore2.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        public ViewResult Error()
        {
            return View();
        }
    }
}
