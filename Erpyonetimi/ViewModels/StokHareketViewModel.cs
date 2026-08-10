using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class StokHareketViewModel :BaseViewModel
    {
        private readonly IStokHareketService _service;
        private readonly IParcaService _parcaService;
        private readonly IDepoService _depoService;
        public ObservableCollection<StokHareket> Hareketler { get; set; }
        public ObservableCollection<Parca> Parcalar { get; set; }
        public ObservableCollection<string> IslemTipleri {  get; set; }
        public ObservableCollection<Depolar> Depolars { get; set; }
        private Depolar? _seciliDepo;
        public Depolar? SeciliDepo
        {
            get => _seciliDepo;
            set
            {
                _seciliDepo = value;
                OnPropertyChanged();
            }
        }
        private Parca _seciliParca;
        public Parca SeciliParca
        {
            get => _seciliParca;
            set
            {
                _seciliParca = value;
                OnPropertyChanged();
            }
        }
        private string _islemTipi;
        public string IslemTipi
        {
            get => _islemTipi;
            set
            {
                _islemTipi = value;
                OnPropertyChanged();
            }
        }
        private int _miktar;
        public int Miktar
        {
            get=> _miktar;
            set
            {
                _miktar = value;
                OnPropertyChanged();
            }
        }
        private string _aciklama;
        public string Aciklama
        {
            get => _aciklama;
            set
            {
                _aciklama = value;
                OnPropertyChanged();
            }
        }
        public ICommand StokHEkleCommand { get; }
        public ICommand StokListeleCommand { get; }

        public StokHareketViewModel(IStokHareketService service, IParcaService parcaService, IDepoService depoService)
        {
           _service = service;
            _parcaService = parcaService;
            _depoService = depoService;

            Hareketler = new ObservableCollection<StokHareket>(_service.GetAll());

            Parcalar = new ObservableCollection<Parca>(_parcaService.GetAllParca());

            Depolars = new ObservableCollection<Depolar>(_depoService.GetAll());

            StokHEkleCommand = new RelayCommand(Ekle);
            IslemTipleri = new ObservableCollection<string>
            {
                "Giriş",
                "Çıkış"
            };
            StokHEkleCommand = new RelayCommand(Ekle);
            StokListeleCommand = new RelayCommand(Listele);
        }
        private void Listele()
        {
            Hareketler = new ObservableCollection<StokHareket>(_service.GetAll());
            OnPropertyChanged(nameof(Hareketler));
        }
        private void Ekle()
        {
            if (SeciliDepo == null)
            {
                MessageBox.Show("Depo seçiniz");
                return;
            }
            if (SeciliParca == null)
            {
                MessageBox.Show("Parça seçiniz");
                return;
            }
                

            var hareket = new StokHareket
            {
                ParcaId = SeciliParca.Id,
                KullaniciId= UserSession.CurrentUser.Id,
                DepoId=SeciliDepo.Id,
                IslemTipi=IslemTipi,
                Miktar= Miktar,
                Aciklama=Aciklama,
                
                Tarih=DateTime.Now

            };
            if (IslemTipi == "Giriş")
            {
                SeciliParca.MevcutStok += Miktar;
            }
            else if (IslemTipi == "Çıkış")
            {
                if(SeciliParca.MevcutStok< Miktar)
                {
                    MessageBox.Show("Yeterli stok yok!");
                    return;
                }
                SeciliParca.MevcutStok -= Miktar;
            }
            _parcaService.UpdateParca(SeciliParca);
            _service.AddStokHareket(hareket);
            Hareketler.Add(hareket);
            MessageBox.Show("Stok hareketi eklendi");
            
        }

    }
}
