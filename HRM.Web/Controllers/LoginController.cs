﻿using HRM.Models;
using HRM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace HRM.Web.Controllers
{
    public class LoginController : Controller 
    {
        private readonly ILogger<LoginController> _logger;
        private readonly HttpClient _httpClient;
        public LoginController(ILogger<LoginController> logger,HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(" https://localhost:7266/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public bool IsTokenInvalidOrEmpty(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }

        public IActionResult Login()
        {
            var token=HttpContext.Session.GetString("token");
            
            if (IsTokenInvalidOrEmpty(token))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Profile","User");
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel login,string returnUrl)
        {   
            var result = await _httpClient.PostAsJsonAsync<LoginModel>("api/User/Login", login);
            var response = await result.Content.ReadAsAsync<LoginResponse>();
            if (response.Data != null && result.IsSuccessStatusCode)
            {
                var jwt = response.Token;
                var handler = new JwtSecurityTokenHandler();//remove
                var Token = handler.ReadJwtToken(jwt);//remove
                if (IsTokenInvalidOrEmpty(jwt))
                {
                    ModelState.AddModelError(string.Empty, "Not authorized");
                    return View(login);
                }
                var type = Token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;//remove it
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(response.Data));
                HttpContext.Session.SetString("token", response.Token);
                if (type==UserType.manager.ToString())
                {

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index","Vacation");
                }
            }
            else if(response.Data==null && response.Token==null && response.ErrorCode!=0)
            {
               // ViewBag.ReturnUrl = returnUrl;
                ModelState.AddModelError(string.Empty, "User not Found ");
                return View();
            }
            else
            {
               // ViewBag.ReturnUrl = returnUrl;
                ModelState.AddModelError(string.Empty, "Login Falied");
                return View();
            }

        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(UpdatePasswordDto updatePasswordDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://localhost:7266/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var putTask = client.PutAsJsonAsync<UpdatePasswordDto>("api/User/ResetPassword", updatePasswordDto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Reset Successfully");
                    return RedirectToAction("Login", "Login");
                }
                ModelState.AddModelError(string.Empty, "UserNotFound");
                return View();
            }
        }



    }
}