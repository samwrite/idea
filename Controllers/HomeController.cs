using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using idea.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace idea.Controllers
{
    public class HomeController : Controller
    {
        private IdeaContext _context;

        public HomeController(IdeaContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterProcess(RegUser NewUser)
        {
            if (ModelState.IsValid)
            {
                List<User> UserExists = _context.Users.Where(theuser => theuser.Email == NewUser.Email).ToList();
                if (UserExists.Count > 0)
                {
                    ViewBag.ErrorRegister = "Email already exists...";
                    return View("Index");
                }
                PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                User user = new User
                {
                    Name = NewUser.Name,
                    Alias = NewUser.Alias,
                    Email = NewUser.Email,
                    Password = NewUser.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                User created = _context.Users.Single(u => u.Email == NewUser.Email);
                HttpContext.Session.SetInt32("id", (int)created.UserId);
                HttpContext.Session.SetString("User", (string)created.Name);
                return RedirectToAction("Dashboard", "Idea");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("loginprocess")]
        public IActionResult LoginProcess(string LEmail = null, string Password = null)
        {
            if(Password != null && LEmail != null)
            {
                List<User> UserExists = _context.Users.Where(u => u.Email == LEmail).ToList();
                if (UserExists.Count > 0)
                {
                    var Hasher  = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(UserExists[0], UserExists[0].Password, Password))
                    {
                        HttpContext.Session.SetInt32("id", (int)UserExists[0].UserId);
                        HttpContext.Session.SetString("User", (string)UserExists[0].Name);
                        return RedirectToAction("Dashboard", "Idea");
                    }
                }
            }
            ViewBag.LoginError = "Invalid Login...";
            return View("Index");
        }        
    }
}