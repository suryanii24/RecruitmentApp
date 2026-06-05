using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentApp.Models
{
    [Table("Biodatas")]
    public class Biodata
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [MaxLength(100)]
        public required string PosisiDilamar { get; set; }

        [MaxLength(100)]
        public required string Nama { get; set; }

        [MaxLength(30)]
        public required string NoKtp { get; set; }

        [MaxLength(100)]
        public required string TempatLahir { get; set; }

        public DateTime TanggalLahir { get; set; }

        [MaxLength(20)]
        public required string JenisKelamin { get; set; }

        [MaxLength(50)]
        public required string Agama { get; set; }

        [MaxLength(5)]
        public string? GolonganDarah { get; set; }

        [MaxLength(30)]
        public required string Status { get; set; }

        public required string AlamatKtp { get; set; }

        public required string AlamatTinggal { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(30)]
        public required string NoTelp { get; set; }

        [MaxLength(100)]
        public required string KontakDarurat { get; set; }

        public string? Skill { get; set; }

        public bool BersediaDitempatkan { get; set; }
 
        public required string GajiDiharapkan { get; set; }

        // Navigation Property
        public User? User { get; set; }

        public ICollection<Education> Educations { get; set; }
            = new List<Education>();

        public ICollection<Training> Trainings { get; set; }
            = new List<Training>();

        public ICollection<WorkExperience> WorkExperiences { get; set; }
            = new List<WorkExperience>();
    }
}