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
    public class IdeaController : Controller
    {
        private IdeaContext _context;

        public IdeaController(IdeaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Idea> Ideas = _context.Ideas.Include(a => a.Likeds).ThenInclude(u => u.User).ToList();
            ViewBag.Ideas = Ideas;
            ViewBag.UserId = (int)HttpContext.Session.GetInt32("id");
            ViewBag.User = _context.Users.SingleOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("id"));
            return View("Dashboard");
        }

        [HttpPost]
        [Route("newidea")]
        public IActionResult NewIdea(string newidea)
        {
            Idea idea = new Idea
            {
                content = newidea,
                Creator = _context.Users.SingleOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("id")),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.Ideas.Add(idea);
            _context.SaveChanges();
            Liked liked = new Liked
            {
                UserId = (int)HttpContext.Session.GetInt32("id"),
                IdeaId = idea.IdeaId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Likeds.Add(liked);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("/like/{id}")]
        public IActionResult Like(int id){
            Idea TheIdea = _context.Ideas.Include(w => w.Likeds).ThenInclude(u => u.User).SingleOrDefault(w => w.IdeaId == id);
            User CurrUser = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id"));
            var CheckLiked = _context.Likeds.Where(l => l.User == CurrUser && l.Idea == TheIdea).ToList();
            if (TheIdea.Creator != CurrUser && CheckLiked.Count < 1)
            {
                Liked NewLike = new Liked
                {
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    IdeaId = TheIdea.IdeaId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Likeds.Add(NewLike);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("/idea/{id}")]
        public IActionResult ViewIdea(int id){
            Idea TheIdea = _context.Ideas.Include(w => w.Likeds).ThenInclude(u => u.User).SingleOrDefault(w => w.IdeaId == id);
            ViewBag.Idea = TheIdea;
            return View();
        }
        
        [HttpGet]
        [Route("/users/{id}")]
        public IActionResult ViewUser(int id){
            User TheUser = _context.Users.Include(w => w.Likeds).ThenInclude(u => u.Idea).SingleOrDefault(w => w.UserId == id);
            ViewBag.User = TheUser;
            List<Idea> Posts = _context.Ideas.Include(w => w.Likeds).ThenInclude(u => u.User).Where(w => w.Creator.UserId == id).ToList();
            List<Liked> Likes = _context.Likeds.Where(l => l.User == TheUser).ToList();
            ViewBag.Likes = Likes.Count;
            ViewBag.Posts = Posts.Count;
            return View();
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id){
            Idea deleted = _context.Ideas.SingleOrDefault(w => w.IdeaId == id);
            _context.Ideas.Remove(deleted);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}