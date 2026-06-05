using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentApp.Models
{
    [Table("Trainings")]
    public class Training
    {
        [Key]
        public int Id { get; set; }

        public int BiodataId { get; set; }

        [MaxLength(100)]
        public string? NamaKursus { get; set; }

        public string? Sertifikat { get; set; }

        public int Tahun { get; set; }
       
        public Biodata? Biodata { get; set; }
    }
}