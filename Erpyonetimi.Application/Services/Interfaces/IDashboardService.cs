using Erpyonetimi.Domain.Entities;
using System.Collections.Generic;
using Erpyonetimi.Application.Services;
using System.Threading.Tasks;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task <int> GetToplamSiparisAsync();
        Task <int> GetToplamMusteriAsync ();
        Task <int> GetToplamKullaniciAsync ();
        Task <int> GetToplamParcaAsync ();
        Task <int> GetToplamKategoriAsync ();
        Task <int> GetToplamTedarikciAsync ();
        Task <int> GetKritikStokSayisiAsync ();
        Task <List<Users>> GetSonKullanicilarAsync (int adet);
        Task <List<Parca>> GetSonParcalarAsync (int adet);
        Task <List<Siparis>> GetSonSiparislerAsync (int adet);
    }
}
