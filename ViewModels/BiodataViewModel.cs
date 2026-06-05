using System.ComponentModel.DataAnnotations;

namespace RecruitmentApp.ViewModels
{
    public class BiodataViewModel
    {
        [Required]
        public string PosisiDilamar { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string NoKTP { get; set; } = string.Empty;

        [Required]
        public string TempatLahir { get; set; } = string.Empty;

        [Required]
        public DateTime TanggalLahir { get; set; } 

        [Required]
        public string JenisKelamin { get; set; } = string.Empty;

        [Required]
        public string Agama { get; set; } = string.Empty;

        [Required]
        public string? GolonganDarah { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public string AlamatKTP { get; set; } = string.Empty;

        [Required]
        public string AlamatTinggal { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string NoTelp { get; set; } = string.Empty;

        [Required]
        public string OrangTerdekat { get; set; } = string.Empty;

        public string? Skill { get; set; }

        public bool BersediaDitempatkan { get; set; }

        public string? PenghasilanDiharapkan { get; set; }

        public List<EducationViewModel> Educations { get; set; } = new();

        public List<TrainingViewModel> Trainings { get; set; } = new();

        public List<WorkExperienceViewModel> WorkExperiences { get; set; } = new();
    }
}