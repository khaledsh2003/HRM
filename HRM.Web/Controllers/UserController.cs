using HRM.Mapping;
using HRM.Models;
using HRM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HRM.Web.Controllers
{
    //baseController:controller
    //usercontroller:baseController
    //base shared function->manager or not,valid token or not,
    public class UserController : BaseController
    {
        private readonly MvcToDto _mvcToDto;
        public UserController(HttpClient _httpClient,MvcToDto mvcToDto):base(_httpClient)
        {
            _mvcToDto = mvcToDto;
        }
        public IActionResult Profile()
        {
            var user=GetUser();
            if (user != null)
            {
                return View(user);
            }
            return View();
        }
        public IActionResult Index()//fix when goes to login keep in index not profile -> return url, list user, dataTable->jquery->with api,
        {
          
            if (IsManager())
            {
                return RedirectToAction("Profile", "User");
            }
            if (IsTokenInvalidOrEmpty())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public IActionResult Create()
        {
            if (IsManager())
            {
                return RedirectToAction("Profile", "User");
            }
            if (IsTokenInvalidOrEmpty())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Profile");
            }
            var user=_mvcToDto.Map(userModel);
            var result = await _httpClient.PostAsJsonAsync<CreateUserDto>("api/User/Create", user);
            var response = await result.Content.ReadAsAsync<Response<UserDto>>();
        
            if (result.IsSuccessStatusCode)
            { 
                if (response.ErrorCode == ErrorCodes.Success && response.Data!=null)
                {
                    return View("Index");
                }
            }
            else
            {
                if (response.ErrorCode == ErrorCodes.UserAlreadyFound)
                {
                    ModelState.AddModelError(string.Empty, "User Already Found");
                    return View("Create");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unexpected Error");
                    return View("Create");
                }
            }
            ModelState.AddModelError(string.Empty, "Unexpected Error");
            return View("Create");

        }
        //4 methods->api
    }
}
