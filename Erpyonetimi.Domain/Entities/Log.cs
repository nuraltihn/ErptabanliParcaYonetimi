namespace Erpyonetimi.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Islem { get; set; } = "";
        public string Aciklama { get; set; } = "";
        public DateTime Tarih { get; set; }
        public Users? Kullanici { get; set; }
    }
}