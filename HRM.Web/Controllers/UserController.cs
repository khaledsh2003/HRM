using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HRM.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            //return view user info
            var user = HttpContext.Session.GetString("user");
            var result = JsonConvert.DeserializeObject<UserDto>(user);
            
            return View(result);
        }
        public IActionResult Index()
        {
            //validate is manager 
            //if not return to homepage
            //valid token, if not redirect to loginpage

            return View();
        }
        //4 methods->api
    }
}
