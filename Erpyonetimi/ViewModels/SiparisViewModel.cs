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
    public class SiparisViewModel : BaseViewModel
    {
        private readonly ISiparisService _siparisService;
        private readonly IMusteriService _musteriService;
        public ObservableCollection<Siparis> Siparisler { get; set; }
        private List<Siparis> _tumSiparisler;
        public ObservableCollection<Musteri> Musteriler { get; set; }

        private Siparis? _seciliSiparis;
        public Siparis? SeciliSiparis
        {
            get => _seciliSiparis;
            set {  _seciliSiparis = value;
                if (value != null)
                {
                    SiparisNo = value.SiparisNo;
                    SiparisTarihi = value.SiparisTarihi;
                    ToplamTutar= value.ToplamTutar;
                    Durum= value.Durum;

                    SeciliMusteri = Musteriler.FirstOrDefault(x => x.Id == value.MusteriId);
                }
            }
        }

        private Musteri? _seciliMusteri;
        public Musteri? SeciliMusteri
        {
            get => _seciliMusteri;
            set
            {
                _seciliMusteri = value;
                OnPropertyChanged();
            }
        }
        private string _siparisNo = "";
        public string SiparisNo
        {
            get => _siparisNo;
            set
            {
                _siparisNo = value;
                if(string.IsNullOrWhiteSpace(SiparisNo))
                {
                    MessageBox.Show("Sipariş no hiniz");
                    return;
                }
                OnPropertyChanged();
            }
        }
        private DateTime _siparisTarihi = DateTime.Now;
        public DateTime SiparisTarihi
        {
            get => _siparisTarihi;
            set { _siparisTarihi= value;
                OnPropertyChanged();
            }
        }

        private decimal _toplamTutar;
        public decimal ToplamTutar
        {
            get => _toplamTutar;
            set
            {
                _toplamTutar = value; OnPropertyChanged();
            }
        }
        private string _aramaMetni;
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
        private string? _durum;
        public string? Durum
        {
            get => _durum;
            set { _durum = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> Durumlar { get; set; } = new()
        {
            "Beklemede",
            "Onaylandı",
            "Hazırlanıyor",
            "Sevk Edildi",
            "Tamamlandı",
            "İptal Edildi"
        };
       public ICommand SiparisEkleCommand { get; }
        public ICommand SiparisGuncelleCommand { get; }
        public ICommand SiparisSilCommand { get; }
        public ICommand DetayAcCommand { get; }

        public SiparisViewModel(ISiparisService siparisService, IMusteriService musteriService)
        {
            _siparisService = siparisService;
            _musteriService = musteriService;
            _tumSiparisler = _siparisService.GetAll();
            
            Siparisler = new ObservableCollection<Siparis>(_tumSiparisler);
            Siparisler = new ObservableCollection<Siparis>(_siparisService.GetAll());
            Musteriler = new ObservableCollection<Musteri>(_musteriService.GetAll());

            SiparisEkleCommand = new RelayCommand(Ekle);
            SiparisSilCommand = new RelayCommand(Sil);
            SiparisGuncelleCommand = new RelayCommand(Guncelle);
            DetayAcCommand = new RelayCommand(obj=>
            {
                if (obj is Siparis siparis)
                    DetayAc(siparis);
            });
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
        private void DetayAc(Siparis siparis)
        {
            if (siparis == null)
                return;
            string detaylar =
                $"Sipariş No: {siparis.SiparisNo}\n" +
                $"Müşteri: {siparis.Musteri?.FirmaAdi}\n" +
                $"Durum: {siparis.Durum}\n" +
                $"Tarih: {siparis.SiparisTarihi:d}\n" +
                $"Toplam Tutar: {siparis.ToplamTutar}\n\n";
            detaylar+="----- Sipariş Detayları -----\n\n";
            foreach (var item in siparis.SiparisDetaylari)
            {
                detaylar += $"Parca: {item.Parca?.ParcAdi}\n" +
                           $"Miktar: {item.Miktar}\n" +
                           $"Birim Fiyat: {item.BirimFiyat}\n"+
                           $"Toplam: {item.ToplamFiyat}\n\n";
            }
            MessageBox.Show(detaylar, $"Sipariş {siparis.SiparisNo}");
        }
        private void Filtrele()
        {
            var sonuc = _tumSiparisler
                .Where(x =>
                string.IsNullOrWhiteSpace(AramaMetni)
                || x.SiparisNo.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)).ToList();

            Siparisler = new ObservableCollection<Siparis>(sonuc);
            OnPropertyChanged(nameof(Siparisler));
        }
        private void Listele()
        {
            Siparisler = new ObservableCollection<Siparis>(_siparisService.GetAll());
            OnPropertyChanged(nameof(Siparisler));

        }
        private void Temizle()
        {
            SiparisNo = "";
            ToplamTutar = 0;
            Durum = "";
            SiparisTarihi = DateTime.Now;
            SeciliMusteri = null;
            SeciliSiparis = null;
        }
        private void Ekle()
        {
            if (string.IsNullOrWhiteSpace(SiparisNo))
            {
                MessageBox.Show("Sipariş no giriniz.");
                return;
                if (_siparisService.GetByNo(SiparisNo) != null)
                {
                    MessageBox.Show("bu sipariş numaras zaten kayıtlı.");
                    return;
                }
            }
            if (SeciliMusteri == null)
            {
                MessageBox.Show("Müşteri seçiniz.");
                return;
            }
            var siparis = new Siparis
            {
                SiparisNo = SiparisNo,
                MusteriId = SeciliMusteri.Id,
                SiparisTarihi = SiparisTarihi,
                ToplamTutar = ToplamTutar,
                Durum = Durum
            };
            _siparisService.AddSiparis(siparis);
            Listele();
            Temizle();

            MessageBox.Show("Sipariş eklendi.");
        }

        private void Guncelle()
        {
            if (SeciliSiparis == null || SeciliMusteri == null)
                return;
            SeciliSiparis.SiparisNo = SiparisNo;
            SeciliSiparis.MusteriId= SeciliMusteri.Id;
            SeciliSiparis.SiparisTarihi = SiparisTarihi;
            SeciliSiparis.ToplamTutar = ToplamTutar;
            SeciliSiparis.Durum = Durum;

            _siparisService.UpdateSiparis(SeciliSiparis);

            Listele();
            MessageBox.Show("Sipariş güncellendi");

        }
        private void Sil()
        {
            if (SeciliSiparis == null)
                return;

            _siparisService.RemoveSiparis(SeciliSiparis);
            Listele();
            Temizle();
            MessageBox.Show("Sipariş silindi");
        }
    }
}
