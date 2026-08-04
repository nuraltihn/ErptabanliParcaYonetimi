using Erpyonetimi.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Helpers;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Application.Services.Interfaces;
using System.Collections.ObjectModel;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Context;

namespace Erpyonetimi.ViewModels
{
    public class DashboardViewModel:BaseViewModel
    {
        private readonly IDashboardService _dashboardService;

        private int _toplamKullanici;
        private int _toplamParca;
        private int _toplamKategori;
        private int _toplamTedarikci;
        private int _kritikStokSayisi;
        public int ToplamKullanici { get => _toplamKullanici; set { _toplamKullanici = value; OnPropertyChanged(); } }
        public int ToplamParca { get => _toplamParca; set { _toplamParca = value; OnPropertyChanged(); } }
        public int ToplamKategori{ get => _toplamKategori; set { _toplamKategori = value; OnPropertyChanged(); } }
        public int ToplamTedarikci { get => _toplamTedarikci; set { _toplamTedarikci = value; OnPropertyChanged(); } }
        public int KrittikStokSayisi { get => _kritikStokSayisi; set { _kritikStokSayisi = value; OnPropertyChanged(); } }
        public ObservableCollection<Users> SonKullanicilar { get; set; }
        public ObservableCollection<Parca> SonParcalar { get; set; }
        public ObservableCollection<Siparis> SonSiparisler { get; set; }
        
        public DashboardViewModel( IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;

            SonKullanicilar = new ObservableCollection<Users>();
            SonParcalar = new ObservableCollection<Parca>();
            SonSiparisler = new ObservableCollection<Siparis>();

            //VeriYukle();
        }

        private async Task VeriYukle()
        {
            try
            {
                ToplamKullanici = await Task.Run(() => _dashboardService.GetToplamKullanici());
                ToplamParca = await Task.Run(() => _dashboardService.GetToplamParca());
                ToplamKategori = await Task.Run(() => _dashboardService.GetToplamKategori());
                ToplamTedarikci = await Task.Run(() => _dashboardService.GetToplamTedarikci());
                KrittikStokSayisi = await Task.Run(() => _dashboardService.GetKritikStokSayisi());

                var kullanicilar = await Task.Run(() => _dashboardService.GetSonKullanicilar(10));
                SonKullanicilar.Clear();
                foreach (var item in kullanicilar)
                {
                    SonKullanicilar.Add(item);
                }

                var parcalar = await Task.Run(() => _dashboardService.GetSonParcalar(10));
                SonParcalar.Clear();
                foreach (var item in parcalar)
                {
                    SonParcalar.Add(item);
                }

                var siparisler = await Task.Run(() => _dashboardService.GetSonSiparisler(10));
                SonSiparisler.Clear();
                foreach (var item in siparisler)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
       
        }
    }
}
