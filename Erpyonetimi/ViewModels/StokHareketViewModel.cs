using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
        public ICommand StokTemizleCommand { get; }
        public ICommand StokGeriAlCommand { get; }
        public StokHareketViewModel(IStokHareketService service, IParcaService parcaService, IDepoService depoService)
        {
           _service = service;
            _parcaService = parcaService;
            _depoService = depoService;

            Hareketler = new ObservableCollection<StokHareket>();

            Parcalar = new ObservableCollection<Parca>();

            Depolars = new ObservableCollection<Depolar>();

            IslemTipleri = new ObservableCollection<string>
            {
                "Giriş",
                "Çıkış"
            };
            StokHEkleCommand = new RelayCommand(Ekle);
            StokListeleCommand = new RelayCommand(Listele);
            StokGuncelleCommand = new RelayCommand(Guncelle);
            StokSilCommand = new RelayCommand(Sil);
            StokGeriAlCommand = new RelayCommand(GeriAl);
            StokTemizleCommand= new RelayCommand(Temizle);
            _ = Yukle();
        }
        private async Task Yukle()
        {
            if(!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok");
                return;
            }
            Hareketler = new ObservableCollection<StokHareket>(
                await _service.GetAllAsync());
            Parcalar = new ObservableCollection<Parca>(
                await _parcaService.GetAllParcaAsync());
            Depolars = new ObservableCollection<Depolar>(
                await _depoService.GetAllAsync());
            OnPropertyChanged(nameof(Hareketler));
            OnPropertyChanged(nameof(Parcalar));
            OnPropertyChanged(nameof(Depolars));

        }
        private async Task GeriAl()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok");
                return;
            }
            if (SeciliHareket == null)
            {
                MessageBox.Show("Lütfen geri almak istediğiniz stok hareketini seçiniz");
                return;
            }
            if(SeciliHareket.Aciklama?.Contains("Geri Alındı") == true)
            {
                MessageBox.Show("Bu hareket daha önceden geri alımış");
                return;
            }
            var cevap = MessageBox.Show("Seçili hareketi geri almak istediğinize emin misiniz?", "Stok Hareketi Geri Alma",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (cevap != MessageBoxResult.Yes)
            {
                return;
            }
            var parca = await _parcaService.GetByIdAsync(SeciliHareket.ParcaId);
            if (parca == null)
            {
                MessageBox.Show("Parça bulunamadı");
                return;
            }

            if (SeciliHareket.IslemTipi == "Giriş")
            {
                if (parca.MevcutStok < SeciliHareket.Miktar) {
                    MessageBox.Show("Bu hareket geri alnımaz.mevcut stok miktarı yeterli değil");
                    return; 
                }
            
            parca.MevcutStok -= SeciliHareket.Miktar;}
            else if (SeciliHareket.IslemTipi == "Çıkış")
            {
                parca.MevcutStok += SeciliHareket.Miktar;

            }
            await _parcaService.UpdateParcaAsync(parca);
            SeciliHareket.Aciklama = $"{SeciliHareket.Aciklama} |Geri Alındı-{DateTime.Now:dd.MM.yyyy HH:mm}";
            await _service.UpdateStokHareketAsync(SeciliHareket);
            await Listele();

            MessageBox.Show("Stok hareketi geri alındı",
                "Başarılı",
                MessageBoxButton.OK, MessageBoxImage.Information
                );

        }
       
            
        
        private async Task Sil()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            if (SeciliHareket == null)
                return;
            var parca = await _parcaService.GetByIdAsync(SeciliHareket.ParcaId);
            if (parca != null)
            {
                if (SeciliHareket.IslemTipi == "Giriş")
                    parca.MevcutStok -= SeciliHareket.Miktar;
                else
                    parca.MevcutStok += SeciliHareket.Miktar;

               await _parcaService.UpdateParcaAsync(parca);
            }
            var cevap = MessageBox.Show("Bu stok hareketini silmek istediğinize emin misiniz?",
                "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return; 

           await  _service.RemoveStokHareketAsync(SeciliHareket);
           await Listele();
            MessageBox.Show("Stok hareketi silindi.");
        }
        private async Task Guncelle()
        {

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
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
            if (SeciliHareket == null || SeciliParca == null || SeciliDepo == null)
                return;

            var parca = await _parcaService.GetByIdAsync(SeciliHareket.ParcaId);
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
           await _parcaService.UpdateParcaAsync(parca);
            SeciliHareket.ParcaId = SeciliParca.Id;
            SeciliHareket.DepoId = SeciliDepo.Id;
            SeciliHareket.Miktar = Miktar;
            SeciliHareket.Aciklama = Aciklama;
            SeciliHareket.IslemTipi = IslemTipi;

           await _service.UpdateStokHareketAsync(SeciliHareket);
           await Listele();
            MessageBox.Show("Stok hareketi güncellendi.");
        }
        private async Task Listele()
        {

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            var hareketler = await _service.GetAllAsync();
            Hareketler = new ObservableCollection<StokHareket>(hareketler);
            OnPropertyChanged(nameof(Hareketler));
        }
        private async Task Ekle()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            if (Miktar <= 0)
            {
                MessageBox.Show("Miktar 0 dan büyük olmamalıdır.");
                return;
            }
            if (string.IsNullOrWhiteSpace(IslemTipi))
            {
                MessageBox.Show("İşlem tipi seçiniz.");
                return;
            }
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
           await _parcaService.UpdateParcaAsync(SeciliParca);
            await _service.AddStokHareketAsync(hareket);
            Hareketler.Add(hareket);
            MessageBox.Show("Stok hareketi eklendi");
            
        }
        private void Temizle() {
            SeciliParca = null;
            SeciliHareket = null;
            SeciliDepo = null;
            IslemTipi = null;
            Miktar = 0;
            Aciklama = "";
            
        
        
        
        }    }
}
