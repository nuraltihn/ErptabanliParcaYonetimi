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
    public class SiparisDetayViewModel : BaseViewModel
    {
        private readonly ISiparisDetayService _siparisDetayService;
        private readonly IParcaService _parcaService;
        private readonly ISiparisService _siparisService;
        public ObservableCollection<SiparisDetaylari> Detaylar { get; set; }
        public ObservableCollection<Parca> Parcalar { get; set; }
        public ObservableCollection<Siparis> Siparisler { get; set; }
        private List<SiparisDetaylari> _tumDetaylar;
        private int _miktar;
        public int Miktar
        {
            get => _miktar;
            set
            {
                _miktar = value;
                ToplamFiyat = BirimFiyat * value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToplamFiyat));
            }
        }
        private string _aramaMetni = "";
        public string AramaMetni
        {
            get => _aramaMetni;
            set
            {
                _aramaMetni = value;
                OnPropertyChanged();
                Filtrele();
            }
        }
        private decimal _birimFiyat;
        public decimal BirimFiyat
        {
            get => _birimFiyat;
            set {
                _birimFiyat = value;
                OnPropertyChanged();
            }
        }

        private decimal _toplamFiyat;
        public decimal ToplamFiyat
        {
            get => _toplamFiyat;
            set {  _toplamFiyat = value;
                OnPropertyChanged();
            }
        }
        private SiparisDetaylari? _seciliDetay;
        public SiparisDetaylari? SeciliDetay
        {
            get => _seciliDetay;
            set
            {
                _seciliDetay = value;
                if (value != null)
                {
                    Miktar = value.Miktar;
                    BirimFiyat = value.BirimFiyat;
                    ToplamFiyat = value.ToplamFiyat;
                    SeciliParca = Parcalar.FirstOrDefault(x => x.Id == value.ParcaId);

                    OnPropertyChanged(nameof(SeciliParca));
                    OnPropertyChanged(nameof(Miktar));
                    OnPropertyChanged(nameof(BirimFiyat));
                    OnPropertyChanged(nameof(ToplamFiyat));
                }
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
                if (value != null)
                {
                    BirimFiyat = value.SatisFiyat;
                    ToplamFiyat = Miktar * BirimFiyat;
                }
                OnPropertyChanged();
            }
        }
        private Siparis? _seciliSiparis;
        public Siparis? SeciliSiparis
        {
            get => _seciliSiparis;
            set
            {
                _seciliSiparis = value;
                OnPropertyChanged();
                if (value != null)
                {
                    Listele();
                }
            }
        }
    

        public ICommand SiparisDetayEkleCommand { get; }
        public ICommand SiparisDetayGuncelleCommand { get; }
        public ICommand SiparisDetaySilCommand { get; }
        public ICommand SiparisDetayListeleCommand { get; }
        public SiparisDetayViewModel(ISiparisDetayService siparisDetayService, ISiparisService siparisService, IParcaService parcaService)
        {
           _siparisService = siparisService;
            _siparisDetayService = siparisDetayService;
            _parcaService = parcaService;
            Detaylar = new ObservableCollection<SiparisDetaylari>();
            Parcalar = new ObservableCollection<Parca>(
                _parcaService.GetAllParca());
            Siparisler = new ObservableCollection<Siparis>(_siparisService.GetAll());
            SiparisDetayEkleCommand = new RelayCommand(Ekle);
            SiparisDetayGuncelleCommand = new RelayCommand(Guncelle);
            SiparisDetaySilCommand = new RelayCommand(Sil);
            SiparisDetayListeleCommand = new RelayCommand(Listele);
            _tumDetaylar= _siparisDetayService.GetAll();    
            Detaylar= new ObservableCollection<SiparisDetaylari>(_tumDetaylar);

        }
        public Visibility CrudVisibility
        {
            get
            {
                return UserSession.IsAdmin || UserSession.IsSatis
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void Filtrele()
        {
            var sonuc = _tumDetaylar
                .Where(x => x.Parca.ParcAdi.ToLower().Contains(AramaMetni.ToLower()) ||
                            x.Parca.ParcaKodu.ToLower().Contains(AramaMetni.ToLower()) ||
                            x.Siparis.Musteri.Ad.ToLower().Contains(AramaMetni.ToLower()) ||
                            x.Siparis.Musteri.Soyad.ToLower().Contains(AramaMetni.ToLower()))
                .ToList();
            Detaylar = new ObservableCollection<SiparisDetaylari>(sonuc);
            OnPropertyChanged(nameof(Detaylar));
        }
        private void Listele()
        {
            if(SeciliSiparis==null)
                return;
            Detaylar = new ObservableCollection<SiparisDetaylari>(_siparisDetayService.GetAll()
                .Where(x=>x.SiparisId == SeciliSiparis.Id));

            OnPropertyChanged(nameof(Detaylar));
        }
        private void Ekle()
        {
            if(SeciliSiparis == null)
            {
                MessageBox.Show("Sipariş seçiniz.");
                return;
            }
            if (SeciliParca == null)
            {
                MessageBox.Show("Parça seçiniz.");
                return;
            }
            if (Miktar <= 0)
            {
                MessageBox.Show("Miktar giriniz");
                return;

            }
            if (SeciliParca.MevcutStok < Miktar)
            {
                MessageBox.Show("Yeterli stok yok");
                return;
            }
            var detay = new SiparisDetaylari
            {
                SiparisId = SeciliSiparis.Id,
                ParcaId = SeciliParca.Id,
                Miktar = Miktar,
                BirimFiyat = SeciliParca.SatisFiyat,
                ToplamFiyat = Miktar * SeciliParca.SatisFiyat
            };
            _siparisDetayService.AddDetay(detay);

            SeciliParca.MevcutStok -= Miktar;
            _parcaService.UpdateParca(SeciliParca);
            Listele();
            MessageBox.Show("Sipariş Hareketi eklendi");

            var siparis = _siparisService.GetById(SeciliSiparis.Id);
            if(siparis != null)
            {
                siparis.ToplamTutar = _siparisDetayService
                    .GetAll()
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
                _siparisService.UpdateSiparis(siparis);
            }
        }

        private void Guncelle()
        {
            if(SeciliDetay==null || SeciliParca == null)
                return;
            
            SeciliParca.MevcutStok += SeciliDetay.Miktar;
            if(SeciliParca.MevcutStok < Miktar)
            {
                MessageBox.Show("Yeterli stok yok");
                return;
            }

            SeciliParca.MevcutStok -= Miktar;
            _parcaService.UpdateParca(SeciliParca);
            SeciliDetay.ParcaId = SeciliParca.Id;
            SeciliDetay.Miktar = Miktar;
            SeciliDetay.BirimFiyat= BirimFiyat;
            SeciliDetay.ToplamFiyat = Miktar * BirimFiyat;
            _siparisDetayService.UpdateDetay(SeciliDetay);
            Listele();
            MessageBox.Show("Sipariş detayı güncellendi");
            var siparis = _siparisService.GetById(SeciliSiparis.Id);
            if (siparis != null)
            {
                siparis.ToplamTutar = _siparisDetayService
                    .GetAll()
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
                _siparisService.UpdateSiparis(siparis);
            }

        }
        private void Sil()
        {
            if (SeciliDetay == null||SeciliParca == null)
                return;
            SeciliParca.MevcutStok += SeciliDetay.Miktar;
            _parcaService.UpdateParca(SeciliParca);
            _siparisDetayService.DeleteDetay(SeciliDetay);
            Listele();
            MessageBox.Show("Sipariş Hareketi silindi");

            var siparis = _siparisService.GetById(SeciliSiparis.Id);
            if (siparis != null)
            {
                siparis.ToplamTutar = _siparisDetayService
                    .GetAll()
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
                _siparisService.UpdateSiparis(siparis);
            }
        }

       

    }
}
