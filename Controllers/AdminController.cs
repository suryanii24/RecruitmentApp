using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Data;

namespace RecruitmentApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        [HttpGet]
        public IActionResult Index(string? search)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Login");

            var query = _context.Biodatas
                .Include(b => b.Educations)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.Nama.Contains(search) ||
                    b.PosisiDilamar.Contains(search) ||
                    b.Educations.Any(e => e.Jenjang.Contains(search))
                );
            }

            var data = query.ToList();

            ViewBag.Search = search;

            return View(data);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Login");

            var biodata = _context.Biodatas
                .Include(b => b.Educations)
                .Include(b => b.Trainings)
                .Include(b => b.WorkExperiences)
                .FirstOrDefault(b => b.Id == id);

            if (biodata == null)
                return NotFound();

            return View(biodata);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Login");

            var biodata = _context.Biodatas
                .Include(b => b.Educations)
                .Include(b => b.Trainings)
                .Include(b => b.WorkExperiences)
                .FirstOrDefault(b => b.Id == id);

            if (biodata == null)
                return NotFound();

            _context.Educations.RemoveRange(biodata.Educations);
            _context.Trainings.RemoveRange(biodata.Trainings);
            _context.WorkExperiences.RemoveRange(biodata.WorkExperiences);
            _context.Biodatas.Remove(biodata);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}