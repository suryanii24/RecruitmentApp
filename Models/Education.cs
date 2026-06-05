using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentApp.Models
{
    [Table("Educations")]
    public class Education
    {
        [Key]
        public int Id { get; set; }

        public int BiodataId { get; set; }

        [MaxLength(50)]
        public required string Jenjang { get; set; }

        [MaxLength(100)]
        public required string Institusi { get; set; }

        [MaxLength(100)]
        public required string Jurusan { get; set; }

        public int TahunLulus { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal IPK { get; set; }

        public Biodata? Biodata { get; set; }
    }
}