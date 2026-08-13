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
        private StokHareket? _seciliHareket;
        public StokHareket? SeciliHareket
        {
            get => _seciliHareket;
            set
            {
                _seciliHareket = value;
                if (value != null)
                {
                    SeciliParca = Parcalar.FirstOrDefault(x => x.Id == value.ParcaId);
                    SeciliDepo = Depolars.FirstOrDefault(x => x.Id == value.DepoId);
                    IslemTipi = value.IslemTipi;
                    Miktar = value.Miktar;
                    Aciklama = value.Aciklama; 

                    OnPropertyChanged(nameof(SeciliParca));
                OnPropertyChanged(nameof(SeciliDepo));
                OnPropertyChanged(nameof(IslemTipi));
                OnPropertyChanged(nameof(Miktar));
                OnPropertyChanged(nameof(Aciklama));
                }
                OnPropertyChanged();
            }
            
        }
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
        private Parca? _seciliParca;
        public Parca? SeciliParca
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
        public ICommand StokSilCommand { get; }
        public ICommand StokGuncelleCommand { get; }
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
            StokGuncelleCommand = new RelayCommand(Guncelle);
            StokSilCommand = new RelayCommand(Sil);
        }
        private void Sil()
        {
            if (SeciliHareket == null)
                return;
            var parca = _parcaService.GetById(SeciliHareket.ParcaId);
            if (parca != null)
            {
                if (SeciliHareket.IslemTipi == "Giriş")
                    parca.MevcutStok -= SeciliHareket.Miktar;
                else
                    parca.MevcutStok += SeciliHareket.Miktar;

                _parcaService.UpdateParca(parca);
            }
            _service.RemoveStokHareket(SeciliHareket);
            Listele();
            MessageBox.Show("Stok hareketi silindi.");
        }
        private void Guncelle()
        {
            if (SeciliHareket == null || SeciliParca == null || SeciliDepo == null)
                return;

            var parca = _parcaService.GetById(SeciliHareket.ParcaId);
            if (parca == null)
                return;
            if (SeciliHareket.IslemTipi == "Giriş")
                parca.MevcutStok -= SeciliHareket.Miktar;
            else
                parca.MevcutStok += SeciliHareket.Miktar;

            if (IslemTipi == "Giriş")
                parca.MevcutStok += Miktar;
            else
            {
                if (parca.MevcutStok < Miktar)
                {
                    MessageBox.Show("Yeterli stok yok.");
                    return;
                }
                parca.MevcutStok -= Miktar;
            }
            _parcaService.UpdateParca(parca);
            SeciliHareket.ParcaId = SeciliParca.Id;
            SeciliHareket.DepoId = SeciliDepo.Id;
            SeciliHareket.Miktar = Miktar;
            SeciliHareket.Aciklama = Aciklama;
            SeciliHareket.IslemTipi = IslemTipi;

            _service.UpdateStokHareket(SeciliHareket);
            Listele();
            MessageBox.Show("Stok hareketi güncellendi.");
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
