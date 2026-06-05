using System.ComponentModel.DataAnnotations;

namespace RecruitmentApp.ViewModels
{
    public class WorkExperienceViewModel
    {
        public string? NamaPerusahaan { get; set; }
        public string? PosisiTerakhir { get; set; }
        public string? PendapatanTerakhir { get; set; }
        public int? Tahun { get; set; }
    }
}