using System.ComponentModel.DataAnnotations;

namespace RecruitmentApp.ViewModels
{
    public class TrainingViewModel
    {
        public string? NamaKursus { get; set; }
        public string? Sertifikat { get; set; }
        public int Tahun { get; set; }
    }
}