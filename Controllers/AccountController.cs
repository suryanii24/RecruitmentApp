using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
using RecruitmentApp.ViewModels;

namespace RecruitmentApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "Email sudah digunakan");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}