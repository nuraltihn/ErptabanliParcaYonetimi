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
    public class RafViewModel : BaseViewModel
    {
        public ObservableCollection<Raflar> Raflar { get; set; }
        public ObservableCollection<Depolar> Depolar { get; set; }
        private List<Raflar> _tumRaflar;
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
        private string _rafKodu = "";
        public string RafKodu
        {
            get => _rafKodu;
            set
            {
                _rafKodu= value; OnPropertyChanged(); 
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

        private Raflar? _seciliRaf;
        public Raflar? SeciliRaf
        {
            get => _seciliRaf;
            set
            {
                _seciliRaf = value;
                if(value!= null)
                {
                RafKodu = _seciliRaf.RafKodu ??"";
                    SeciliDepo = Depolar?.FirstOrDefault(x => x.Id == _seciliRaf.DepoId);
                   
                }
                else
                {
                    RafKodu = "";
                    SeciliDepo = null;
                }
                OnPropertyChanged();
            }
        }

        private readonly IRafService _rafService;
        private readonly IDepoService _depoService;

        public ICommand RafEkleCommand{ get;}
        public ICommand RafGuncelleCommand { get; }
        public ICommand RafSilCommand { get; }
        public ICommand ListeleRafCommand { get; }
        public ICommand RafTemizleCommand { get; }
        public RafViewModel(IRafService rafService, IDepoService depoService)
        {
            _rafService = rafService;
            _depoService = depoService;
        
            Depolar= new ObservableCollection<Depolar>(); 
            Raflar = new ObservableCollection<Raflar>();
            _tumRaflar = new List<Raflar>();
            RafEkleCommand = new RelayCommand(async()=> await Ekle());
            RafSilCommand = new RelayCommand(async()=> await Sil());
            RafGuncelleCommand = new RelayCommand(async()=> await Guncelle());
            ListeleRafCommand= new RelayCommand(async ()=> await Listele());
            RafTemizleCommand= new RelayCommand(Temizle);
            _ = YukleAsync();
           
        }
        private async Task YukleAsync()
        {
            var depolar = await _depoService.GetAllAsync();
            Depolar = new ObservableCollection<Depolar>(depolar);
            var raflar = await _rafService.GetAllAsync();
            _tumRaflar = raflar;
            Raflar = new ObservableCollection<Raflar>(raflar);
            OnPropertyChanged(nameof(Depolar));
            OnPropertyChanged(nameof(Raflar));
        }
        private void Filtrele()
        {
            if (_tumRaflar == null) return;
            if (string.IsNullOrWhiteSpace(AramaMetni))
            {
                Raflar = new ObservableCollection<Raflar>(_tumRaflar);
            }
            else
            {
                var arama = AramaMetni.Trim();
                var filtrelenmisRaflar = _tumRaflar
                    .Where(r => (r.RafKodu!= null && r.RafKodu.Contains(arama, StringComparison.OrdinalIgnoreCase)) ||
                                (r.Depo !=null && r.Depo.Depaadi !=null && r.Depo.Depaadi.Contains(arama, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                Raflar = new ObservableCollection<Raflar>(filtrelenmisRaflar);
            }
            OnPropertyChanged(nameof(Raflar));
        }
        private async Task Listele()
        {
            var raflar = await _rafService.GetAllAsync();
            _tumRaflar = raflar ?? new List<Raflar>();
            if (!string.IsNullOrWhiteSpace(AramaMetni))
            {
                Filtrele();
            }
            else
            {
               Raflar = new ObservableCollection<Raflar>(_tumRaflar);
                OnPropertyChanged(nameof(Raflar)); 
            }
            

            
        }
        private void Temizle()
        {
            RafKodu = "";
            SeciliDepo = null;
            SeciliRaf = null;
        }
        private async Task Ekle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            var kod = RafKodu?.Trim();
            if (string.IsNullOrWhiteSpace(kod))
            {
                MessageBox.Show("Raf kodu giriniz.");
                return;
            }
            var mevcut = await _rafService.GetByKodAsync(kod);
            if (mevcut != null)
            {
                MessageBox.Show("Bu isimde bir raf zaten mevcut");
                return;
            }
            var raf = new Raflar
            {
                RafKodu = RafKodu,
                DepoId = SeciliDepo.Id

            };
            await _rafService.AddRafAsync(raf);
            await Listele();
            Temizle();
            MessageBox.Show("Raf eklendi.");
        }
        private async Task Guncelle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(RafKodu))
            {
                MessageBox.Show("Raf kodu giriniz");
                return;
            }
            if (SeciliRaf == null)
            {
                MessageBox.Show("Raf seçiniz");
                return;
            }
            if (SeciliDepo == null)
            {
                MessageBox.Show("Depo seçiniz");
                return;
            }
            SeciliRaf.RafKodu = RafKodu;
            SeciliRaf.DepoId = SeciliDepo.Id;
            await _rafService.UpdateRafAsync(SeciliRaf);
            await Listele();
            MessageBox.Show("Raf güncellendi");
        }
       
        private async Task Sil()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }

            if (SeciliRaf == null)
                return;
            var cevap = MessageBox.Show(
                "Seçili rafı silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes){
                return;
            }
            await _rafService.RemoveRafAsync(SeciliRaf);
            await Listele();
            Temizle();
            MessageBox.Show("Raf silindi.");
        }
    }
}
