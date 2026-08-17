using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class KategoriViewModel : BaseViewModel
    {
        public ObservableCollection<Kategori> Kategoriler { get; set; }
        private Kategori _seciliKategori;
        public Kategori SeciliKategori
        {
            get => _seciliKategori;
            set
            {
                _seciliKategori = value;
                if (_seciliKategori != null)
                {
                    KategoriAdi = value.KategoriAdi;
                    Aciklama = value.Aciklama;
                    OnPropertyChanged(nameof(KategoriAdi));
                    OnPropertyChanged(nameof(Aciklama));
                }
                OnPropertyChanged();
            }
        }

        private string _kategoriAdi;
        public string KategoriAdi
        {
            get => _kategoriAdi;
            set { _kategoriAdi = value;
                OnPropertyChanged(); }

        }

        private string _aciklama;
        public string Aciklama
        {
            get => _aciklama;
            set
            {
                _aciklama= value;
                OnPropertyChanged();
            }
        }

        public ICommand KategoriEkleCommand { get; }
        public ICommand KategoriGuncelleCommand { get; }
        public ICommand KategoriSilCommand { get; }
        public ICommand ListeleKategoriCommand { get; }
        public ICommand KategoriTemizleCommand { get; }
        private readonly IKategoriService _kategoriService;
        public KategoriViewModel(IKategoriService kategoriService)
        {

            _kategoriService = kategoriService;
            Kategoriler = new ObservableCollection<Kategori>(
                _kategoriService.GetAllKategori());
            KategoriEkleCommand = new RelayCommand(Ekle);
            KategoriGuncelleCommand = new RelayCommand(Guncelle);
            KategoriSilCommand = new RelayCommand(Sil);
            ListeleKategoriCommand = new RelayCommand(Listele);
            KategoriTemizleCommand= new RelayCommand(Temizle);

        }

       
      
        private void Ekle()
        {
          

            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }
            if (string.IsNullOrWhiteSpace(KategoriAdi))
            {
                MessageBox.Show("Kategori adı zorunludur");
                return;
            }
            if(_kategoriService.GetAllKategori()
                .Any(x=>x.KategoriAdi.Equals(KategoriAdi, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Bu kategori zaten mevcut");
                return;
            }
            if (string.IsNullOrWhiteSpace(Aciklama))
            {
                MessageBox.Show("açıklama zorunludur");
                return;
            }
            var kategori = new Kategori
            {
                KategoriAdi = KategoriAdi,
                Aciklama = Aciklama
            };
            _kategoriService.AddKategori(kategori);
            Listele();
            Temizle();

        }
        private void Guncelle()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }
            if (string.IsNullOrWhiteSpace(KategoriAdi))
            {
                MessageBox.Show("Kategori adı zorunludur");
                return;
            }
            if (string.IsNullOrWhiteSpace(Aciklama))
            {
                MessageBox.Show("açıklama zorunludur");
                return;
            }
            if (SeciliKategori == null)
                return;

            SeciliKategori.KategoriAdi = KategoriAdi;
            SeciliKategori.Aciklama = Aciklama;

            _kategoriService.UpdateKategori(SeciliKategori);
            Listele();
        }
        private void Sil()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }
            if (SeciliKategori == null)
                return;
            var cevap= MessageBox.Show(
                "Seçili kategoriyi silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;
            _kategoriService.DeleteKategori(SeciliKategori.Id);
            
            Listele();
        }
        private void Listele()
        {
            Kategoriler = new ObservableCollection<Kategori>(
                _kategoriService.GetAllKategori());
            OnPropertyChanged(nameof(Kategoriler));
        }
        private void Temizle()
        {
            KategoriAdi = string.Empty;
            Aciklama = string.Empty;
            SeciliKategori = null;
        }
    }
}
