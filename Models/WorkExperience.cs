using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentApp.Models
{
    [Table("WorkExperiences")]
    public class WorkExperience
    {
        [Key]
        public int Id { get; set; }

        public int BiodataId { get; set; }

        [MaxLength(100)]
        public string? NamaPerusahaan { get; set; }

        [MaxLength(100)]
        public string? PosisiTerakhir { get; set; }

        
        public required string PendapatanTerakhir { get; set; }

        public int Tahun { get; set; }

        public Biodata? Biodata { get; set; }
    }
}