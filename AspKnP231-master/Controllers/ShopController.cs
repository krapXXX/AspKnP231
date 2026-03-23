using Microsoft.AspNetCore.Mvc;

namespace AspKnP231.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
