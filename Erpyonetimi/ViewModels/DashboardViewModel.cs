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
                return $"Rolü: {UserSession.CurrentUser?.Rol?.RolAdi??""}";
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
        public ObservableCollection<Users> SonKullanicilar { get; set; } = new();
        public ObservableCollection<Parca> SonParcalar { get; set; } = new();
        public ObservableCollection<Siparis> SonSiparisler { get; set; } = new();
        private string _adSoyad;
        public string AdSoyad { get => _adSoyad; set { _adSoyad = value; OnPropertyChanged(); } }
        private string _kulAd;
        public string KulAd { get => _kulAd; set { _kulAd = value; OnPropertyChanged(); } }
       
        private string _parcAdi;
        public string ParcAdi { get => _parcAdi; set { _parcAdi = value; OnPropertyChanged(); } }

        private readonly IServiceProvider _serviceProvider;
        public DashboardViewModel(IServiceProvider serviceProvider, IDashboardService dashboardService)
        {
            _serviceProvider = serviceProvider;
            _dashboardService = dashboardService;

        

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
            }
            catch
            {
                ToplamMusteri = 0;
            }
            try
            {
                ToplamSiparis = await _dashboardService.GetToplamSiparisAsync();
            }
            catch
            {
                ToplamSiparis= 0;
            }
            try
            {
                ToplamKullanici = await _dashboardService.GetToplamKullaniciAsync();
            }
            catch
            {
                ToplamKullanici= 0;
            }
            try
            {
                ToplamParca = await _dashboardService.GetToplamParcaAsync();
            }
            catch { ToplamParca= 0; }


            try
            {

                var kullanicilar = await _dashboardService.GetSonKullanicilarAsync(10);
                SonKullanicilar = new ObservableCollection<Users>(kullanicilar ?? new());
                OnPropertyChanged(nameof(SonKullanicilar));
            }
            catch {
                SonKullanicilar.Clear();
            
            }
            try
            {
                var parcalar = await _dashboardService.GetSonParcalarAsync(10);
                SonParcalar = new ObservableCollection<Parca>(parcalar ?? new());
                OnPropertyChanged(nameof(SonParcalar)); }
            catch {
                SonParcalar.Clear();
            }
            try
            {
                var siparisler = await _dashboardService.GetSonSiparislerAsync(10);
                SonSiparisler = new ObservableCollection<Siparis>(siparisler ?? new());

                OnPropertyChanged(nameof(SonSiparisler));
            }
            catch
            {
                SonSiparisler.Clear();
            }
              
            }
            
        }
    }

