using CollegeManagement.Models;
using CollegeManagement.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;




namespace CollegeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IUserService userService, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("Login")]
        public async Task<ActionResult> SignIn(User user)
        {
            var userData = _httpContextAccessor.HttpContext.Session.GetString("userData");
            if (userData != null)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
            }
            if(_userService.IsValidUser(user.EmailId, user.UserPassword))
            {
                var us = _userService.Get(user.EmailId);
                _httpContextAccessor.HttpContext.Session.SetString("userData", us.EmailId);
                var claims = new List<Claim>();
                claims.Add(new Claim("EmailId", user.EmailId));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.EmailId));
                claims.Add(new Claim(ClaimTypes.Role, us.UserType));
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                
                TempData["user"] = us.Id;
                switch(us.UserType)
                {
                    case "admin":
                            return RedirectToAction("Index","Home"); 
                    case "Student":
                        return RedirectToAction("Details", "Student");
                    case "Teacher":
                        return RedirectToAction("Details", "Teacher");
                }
            }
            else
            {
                ViewBag.Message = "Invalid Username Or Password";
            }
            return View("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
