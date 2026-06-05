using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
using RecruitmentApp.ViewModels;

namespace RecruitmentApp.Controllers
{
    public class BiodataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BiodataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Biodata()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Login");

            var user = _context.Users
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.Educations)
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.Trainings)
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.WorkExperiences)
                .FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return RedirectToAction("Login", "Login");

            if (user.Biodata != null)
            {
                var model = new BiodataViewModel
                {
                    PosisiDilamar = user.Biodata.PosisiDilamar,
                    Nama = user.Biodata.Nama,
                    NoKTP = user.Biodata.NoKtp,
                    TempatLahir = user.Biodata.TempatLahir,
                    TanggalLahir = user.Biodata.TanggalLahir,
                    JenisKelamin = user.Biodata.JenisKelamin,
                    Agama = user.Biodata.Agama,
                    GolonganDarah = user.Biodata.GolonganDarah,
                    Status = user.Biodata.Status,
                    AlamatKTP = user.Biodata.AlamatKtp,
                    AlamatTinggal = user.Biodata.AlamatTinggal,
                    Email = user.Biodata.Email,
                    NoTelp = user.Biodata.NoTelp,
                    OrangTerdekat = user.Biodata.KontakDarurat,
                    Skill = user.Biodata.Skill,
                    BersediaDitempatkan = user.Biodata.BersediaDitempatkan,
                    PenghasilanDiharapkan = user.Biodata.GajiDiharapkan,

                    Educations = user.Biodata.Educations.Select(e => new EducationViewModel
                    {
                        Jenjang = e.Jenjang,
                        NamaInstitusi = e.Institusi,
                        Jurusan = e.Jurusan,
                        TahunLulus = e.TahunLulus,
                        IPK = e.IPK.ToString("0.00", CultureInfo.InvariantCulture)
                    }).ToList(),

                    Trainings = user.Biodata.Trainings.Select(t => new TrainingViewModel
                    {
                        NamaKursus = t.NamaKursus,
                        Sertifikat = t.Sertifikat,
                        Tahun = t.Tahun
                    }).ToList(),

                    WorkExperiences = user.Biodata.WorkExperiences.Select(w => new WorkExperienceViewModel
                    {
                        NamaPerusahaan = w.NamaPerusahaan,
                        PosisiTerakhir = w.PosisiTerakhir,
                        PendapatanTerakhir = w.PendapatanTerakhir,
                        Tahun = w.Tahun
                    }).ToList()
                };

                return View(model);
            }

            var emptyModel = new BiodataViewModel
            {
                Educations = new List<EducationViewModel> { new EducationViewModel() },
                Trainings = new List<TrainingViewModel> { new TrainingViewModel() },
                WorkExperiences = new List<WorkExperienceViewModel> { new WorkExperienceViewModel() }
            };

            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult Biodata(BiodataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Educations ??= new List<EducationViewModel>();
                model.Trainings ??= new List<TrainingViewModel>();
                model.WorkExperiences ??= new List<WorkExperienceViewModel>();

                return View(model);
            }
                

            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Login");

            var user = _context.Users
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.Educations)
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.Trainings)
                .Include(u => u.Biodata)
                    .ThenInclude(b => b!.WorkExperiences)
                .FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return RedirectToAction("Login", "Login");

            if (user.Biodata == null)
            {
                var biodata = new Biodata
                {
                    UserId = user.Id,
                    PosisiDilamar = model.PosisiDilamar,
                    Nama = model.Nama,
                    NoKtp = model.NoKTP,
                    TempatLahir = model.TempatLahir,
                    TanggalLahir = model.TanggalLahir,
                    JenisKelamin = model.JenisKelamin,
                    Agama = model.Agama,
                    GolonganDarah = model.GolonganDarah,
                    Status = model.Status,
                    AlamatKtp = model.AlamatKTP,
                    AlamatTinggal = model.AlamatTinggal,
                    Email = model.Email,
                    NoTelp = model.NoTelp,
                    KontakDarurat = model.OrangTerdekat,
                    Skill = model.Skill,
                    BersediaDitempatkan = model.BersediaDitempatkan,
                    GajiDiharapkan = model.PenghasilanDiharapkan ?? string.Empty,
                    Educations = model.Educations.Select(e => new Education
                    {
                        Jenjang = e.Jenjang,
                        Institusi = e.NamaInstitusi,
                        Jurusan = e.Jurusan,
                        TahunLulus = e.TahunLulus,
                        IPK = decimal.Parse(e.IPK.Replace(",", "."), CultureInfo.InvariantCulture)
                    }).ToList(),
                    Trainings = model.Trainings.Select(t => new Training
                    {
                        NamaKursus = t.NamaKursus,
                        Sertifikat = t.Sertifikat,
                        Tahun = t.Tahun
                    }).ToList(),
                    WorkExperiences = model.WorkExperiences.Select(w => new WorkExperience
                    {
                        NamaPerusahaan = w.NamaPerusahaan,
                        PosisiTerakhir = w.PosisiTerakhir,
                        PendapatanTerakhir = w.PendapatanTerakhir ?? string.Empty,
                        Tahun = w.Tahun ??0
                    }).ToList()
                };

                _context.Biodatas.Add(biodata);
            }

            else
            {
               var biodata = user.Biodata;

                biodata.PosisiDilamar = model.PosisiDilamar;
                biodata.Nama = model.Nama;
                biodata.NoKtp = model.NoKTP;
                biodata.TempatLahir = model.TempatLahir;
                biodata.TanggalLahir = model.TanggalLahir;
                biodata.JenisKelamin = model.JenisKelamin;
                biodata.Agama = model.Agama;
                biodata.GolonganDarah = model.GolonganDarah;
                biodata.Status = model.Status;
                biodata.AlamatKtp = model.AlamatKTP;
                biodata.AlamatTinggal = model.AlamatTinggal;
                biodata.Email = model.Email;
                biodata.NoTelp = model.NoTelp;
                biodata.KontakDarurat = model.OrangTerdekat;
                biodata.Skill = model.Skill;
                biodata.BersediaDitempatkan = model.BersediaDitempatkan;
                biodata.GajiDiharapkan = model.PenghasilanDiharapkan ?? string.Empty;

                _context.Educations.RemoveRange(biodata.Educations);
                _context.Trainings.RemoveRange(biodata.Trainings);
                _context.WorkExperiences.RemoveRange(biodata.WorkExperiences);

                biodata.Educations = model.Educations.Select(e => new Education
                {
                    BiodataId = biodata.Id,
                    Jenjang = e.Jenjang,
                    Institusi = e.NamaInstitusi,
                    Jurusan = e.Jurusan,
                    TahunLulus = e.TahunLulus,
                    IPK = decimal.Parse(e.IPK.Replace(",", "."), CultureInfo.InvariantCulture)
                }).ToList();

                biodata.Trainings = model.Trainings.Select(t => new Training
                {
                    BiodataId = biodata.Id,
                    NamaKursus = t.NamaKursus,
                    Sertifikat = t.Sertifikat,
                    Tahun = t.Tahun
                }).ToList();

                biodata.WorkExperiences = model.WorkExperiences.Select(w => new WorkExperience
                {
                    BiodataId = biodata.Id,
                    NamaPerusahaan = w.NamaPerusahaan,
                    PosisiTerakhir = w.PosisiTerakhir,
                    PendapatanTerakhir = w.PendapatanTerakhir ?? string.Empty,
                    Tahun = w.Tahun ?? 0
                }).ToList(); 
            }
           
            _context.SaveChanges();
            
            return RedirectToAction("Biodata", "Biodata");
        }

        [HttpPost]
        public IActionResult DeleteBiodata()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
        
            return RedirectToAction("Login", "Login");
        
            var user = _context.Users
            .Include(u => u.Biodata)
                .ThenInclude(b => b!.Educations)
            .Include(u => u.Biodata)
                .ThenInclude(b => b!.Trainings)
            .Include(u => u.Biodata)
                .ThenInclude(b => b!.WorkExperiences)
            .FirstOrDefault(u => u.Email == userEmail);
            
            if (user == null)
            return RedirectToAction("Login", "Login");

            if (user.Biodata == null)
            return RedirectToAction("Biodata", "Biodata");
            
            _context.Educations.RemoveRange(user.Biodata.Educations);
            _context.Trainings.RemoveRange(user.Biodata.Trainings);
            _context.WorkExperiences.RemoveRange(user.Biodata.WorkExperiences);
            _context.Biodatas.Remove(user.Biodata);

            _context.SaveChanges();

            return RedirectToAction("Biodata", "Biodata");
        }

    }
}