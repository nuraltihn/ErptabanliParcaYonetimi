using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Helpers;
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

            VeriYukle();
        }

        private void VeriYukle()
        {
            try
            {
                ToplamKullanici = _dashboardService.GetToplamKullanici();
                ToplamParca = _dashboardService.GetToplamParca();
                ToplamKategori = _dashboardService.GetToplamKategori();
                ToplamTedarikci = _dashboardService.GetToplamTedarikci();
                KrittikStokSayisi = _dashboardService.GetKritikStokSayisi();

                SonKullanicilar = new ObservableCollection<Users>(
                    _dashboardService.GetSonKullanicilar(10));

                SonParcalar = new ObservableCollection<Parca>(
                    _dashboardService.GetSonParcalar(10));

                SonSiparisler = new ObservableCollection<Siparis>(
                    _dashboardService.GetSonSiparisler(10));

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
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
