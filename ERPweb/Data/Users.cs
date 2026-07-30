using System.ComponentModel.DataAnnotations;
namespace ERPweb.Data
   
{
    public class Users
    {
        public int Id { get; set; }
        public string? AdSoyad { get; set; }
        [Required]
        public string KulAd { get; set; } = string.Empty;
        [Required]
        public string Sifre { get; set; } = string.Empty;
        public int? RolId { get; set; }
        public string? Email { get; set; }
        public bool? Aktifmi { get; set; }
        public DateTime? OlusturmaTarih { get; set; }

    }
}