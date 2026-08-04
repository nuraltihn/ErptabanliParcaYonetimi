using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Services.Interfaces
{
    internal interface IDashboardService
    {
        int GetToplamKullanici();
        int GetToplamParca();
        int GetToplamKategori();
        int GetToplamTedarikci();
        int GetKritikStokSayisi();
        List<Users> GetSonKullanicilar(int adet);
        List<Parca> GetSonParcalar(int adet);
        List<Siparis> GetSonSiparisler(int adet);
    }
}
