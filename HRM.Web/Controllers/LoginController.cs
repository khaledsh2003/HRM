using HRM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace HRM.Web.Controllers
{
    public class LoginController : Controller 
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
          
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://localhost:7266/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HTTP POST

                var postTask = client.PostAsJsonAsync<LoginModel>("api/User/Login", login);
                postTask.Wait();

                var result = postTask.Result;
                var token = postTask.Result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode &&  token!=null)
                {
                    return RedirectToAction("UserView", "User");
                }
            }

            ModelState.AddModelError(string.Empty, "UserNotFound");
            return View();
        }



    }
}