using System.ComponentModel.DataAnnotations;

namespace RecruitmentApp.ViewModels
{
    public class EducationViewModel
    {
        public string Jenjang { get; set; } = string.Empty;
        public string NamaInstitusi { get; set; } = string.Empty;
        public string Jurusan { get; set; } = string.Empty;
        public int TahunLulus { get; set; }
        public string IPK { get; set; } = string.Empty;
    }
}