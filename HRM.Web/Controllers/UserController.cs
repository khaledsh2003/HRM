using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserView()
        {
            return View();
        }
    }
}
