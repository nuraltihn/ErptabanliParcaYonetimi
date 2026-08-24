using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Erpyonetimi.ViewModels
{
    public class DashboardViewModel:BaseViewModel
    {
        private readonly IDashboardService _dashboardService;
       public string Hosgeldinmsj
        {
            get
            {
                return $"Hoşgeldin {UserSession.CurrentUser?.AdSoyad}!";
            }
        }
        public string Rutbemsj
        {
            get
            {
                return $"Rolü: {UserSession.CurrentUser.Rol?.RolAdi??""}";
            }

        }

      
        private int _toplamMusteri;
        public int ToplamMusteri { get => _toplamMusteri; set { _toplamMusteri = value; OnPropertyChanged(); } }
        private int _toplamSiparis;
        public int ToplamSiparis { get => _toplamSiparis; set { _toplamSiparis = value; OnPropertyChanged(); } }
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
        private string _adSoyad;
        public string AdSoyad { get => _adSoyad; set { _adSoyad = value; OnPropertyChanged(); } }
        private string _kulAd;
        public string KulAd { get => _kulAd; set { _kulAd = value; OnPropertyChanged(); } }
        private string _siparisNo;
        public string SiparisNo { get => _siparisNo; set { _siparisNo = value; OnPropertyChanged(); } }
        private string _parcAdi;
        public string ParcAdi { get => _parcAdi; set { _parcAdi = value; OnPropertyChanged(); } }
        private string _parcaKodu;
        public string ParcaKodu { get => _parcaKodu; set { _parcaKodu = value; OnPropertyChanged(); } }
        public DashboardViewModel( IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;

            SonKullanicilar = new ObservableCollection<Users>();
            SonParcalar = new ObservableCollection<Parca>();
            SonSiparisler = new ObservableCollection<Siparis>();

          _=  VeriYukle();
        }

        private async Task VeriYukle()
        {
             DatabaseHelper.CheckConnection();
                if (!DatabaseHelper.IsConnected)
                {
                return;
        }
       
            try
            {
               

                ToplamMusteri = await _dashboardService.GetToplamMusteriAsync();
                ToplamSiparis = await _dashboardService.GetToplamSiparisAsync();
                ToplamKullanici = await _dashboardService.GetToplamKullaniciAsync();
                ToplamParca = await _dashboardService.GetToplamParcaAsync();
                ToplamKategori = await _dashboardService.GetToplamKategoriAsync();
                ToplamTedarikci = await _dashboardService.GetToplamTedarikciAsync();
                KrittikStokSayisi = await _dashboardService.GetKritikStokSayisiAsync();

                SonKullanicilar = new ObservableCollection<Users>( await
                    _dashboardService.GetSonKullanicilarAsync(10));

                SonParcalar = new ObservableCollection<Parca>( await
                    _dashboardService.GetSonParcalarAsync(10));

                SonSiparisler = new ObservableCollection<Siparis>( await
                    _dashboardService.GetSonSiparislerAsync(10));

          
                OnPropertyChanged(nameof(SonKullanicilar));
                OnPropertyChanged(nameof(SonParcalar));
                OnPropertyChanged(nameof(SonSiparisler));
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
