using Business.IServices;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LInkedIn.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService _userService)
        {
            userService = _userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostLogin(string email, string password)
        {
            var exist=userService.GetAllUser().Where(x=>x.Email==email &&  x.Password==password).FirstOrDefault();
            if (exist!=null)
            {
                string strJson = JsonSerializer.Serialize<User>(exist);
                HttpContext.Session.SetString("CurrentUser", strJson);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.err = "Invalid email or password.";
            return View("Login");
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostSignUp(User user)
        {
            if (ModelState.IsValid)
            {
                var exist = userService.GetAllUser().Where(x => x.Email == user.Email).FirstOrDefault();
                if (exist == null)
                {
                    userService.AddUser(user);
                    if (user.Id != Guid.Empty)
                    {
                        string strJson = JsonSerializer.Serialize<User>(user);
                        HttpContext.Session.SetString("CurrentUser", strJson);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewBag.err = "Email already exists!";
            }
            return View("Signup");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
