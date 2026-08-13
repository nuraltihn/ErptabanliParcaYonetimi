using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        int GetToplamSiparis();
        int GetToplamMusteri();
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
