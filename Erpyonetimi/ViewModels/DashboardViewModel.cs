using Erpyonetimi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Helpers;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Services.Interfaces;
using System.Collections.ObjectModel;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Context;

namespace Erpyonetimi.ViewModels
{
    public class DashboardViewModel:BaseViewModel
    {
        private readonly IDashboardService _dashboardService;

        public int ToplamKullanici { get; set; }
        public int ToplamParca { get; set; }
        public int ToplamKategori{ get; set; }
        public int ToplamTedarikci { get; set; }
        public int KrittikStokSayisi { get; set; }
        public ObservableCollection<Users> SonKullanicilar { get; set; }
        public ObservableCollection<Parca> SonParcalar { get; set; }
        public ObservableCollection<Siparis> SonSiparisler { get; set; }
        
        public DashboardViewModel()
        {
            var context = new ErpDbContextFactory().CreateDbContext(Array.Empty<string>());
            _dashboardService = new DashboardService(context);

            SonKullanicilar = new ObservableCollection<Users>();
            SonParcalar = new ObservableCollection<Parca>();
            SonSiparisler = new ObservableCollection<Siparis>();

            VeriYukle();
        }

        private void VeriYukle()
        {
            ToplamKullanici = _dashboardService.GetToplamKullanici();
            ToplamParca = _dashboardService.GetToplamParca();
            ToplamKategori = _dashboardService.GetToplamKategori();
            ToplamTedarikci = _dashboardService.GetToplamTedarikci();
            KrittikStokSayisi = _dashboardService.GetKritikStokSayisi();

            SonKullanicilar.Clear();
            foreach(var item in _dashboardService.GetSonKullanicilar(10))
            {
                SonKullanicilar.Add(item);
            }


            SonParcalar.Clear();
            foreach(var item in _dashboardService.GetSonParcalar(10))
            {
                SonParcalar.Add(item);
            }

            SonSiparisler.Clear();
            foreach(var item in _dashboardService.GetSonSiparisler(10))
            {
                SonSiparisler.Add(item);
            }


            OnPropertyChanged(nameof(ToplamKullanici));
            OnPropertyChanged(nameof(ToplamParca));
            OnPropertyChanged(nameof(ToplamKategori));
            OnPropertyChanged(nameof(ToplamTedarikci));
            OnPropertyChanged(nameof(KrittikStokSayisi));
            OnPropertyChanged(nameof(SonKullanicilar));
            OnPropertyChanged(nameof(SonParcalar));
            OnPropertyChanged(nameof(SonSiparisler));
        }
    }
}
