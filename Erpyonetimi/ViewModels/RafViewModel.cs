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
                RafKodu = value.RafKodu;
                    SeciliDepo = Depolar
                        .FirstOrDefault(x => x.Id == value.DepoId);
                   
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
        
            Depolar= new ObservableCollection<Depolar>(_depoService.GetAll());
            RafEkleCommand = new RelayCommand(Ekle);
            RafSilCommand = new RelayCommand(Sil);
            RafGuncelleCommand = new RelayCommand(Guncelle);
            ListeleRafCommand= new RelayCommand(Listele);
            RafTemizleCommand= new RelayCommand(Temizle);
            _tumRaflar = new List<Raflar>(_rafService.GetAll());
            Raflar = new ObservableCollection<Raflar>(_tumRaflar);
        }

        private void Filtrele()
        {
            if (string.IsNullOrWhiteSpace(AramaMetni))
            {
                Raflar = new ObservableCollection<Raflar>(_tumRaflar);
            }
            else
            {
                var filtrelenmisRaflar = _tumRaflar
                    .Where(r => (r.RafKodu?.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (r.Depo?.Depaadi?.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
                Raflar = new ObservableCollection<Raflar>(filtrelenmisRaflar);
            }
            OnPropertyChanged(nameof(Raflar));
        }
        private void Listele()
        {
            Raflar = new ObservableCollection<Raflar>(_rafService.GetAll());

            OnPropertyChanged(nameof(Raflar));
        }
        private void Temizle()
        {
            RafKodu = "";
            SeciliDepo = null;
            SeciliRaf = null;
        }
        private void Ekle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            if (_rafService.GetByKod(RafKodu) != null)
            {
                MessageBox.Show("Bu isimde bir raf zaten mevcut.");
                return;
            }

            if (SeciliDepo == null)
            {
                MessageBox.Show("Depo seçiniz");
                return;
            }
            if (string.IsNullOrWhiteSpace(RafKodu)) 
            {
                MessageBox.Show("Raf kodu giriniz.");
                return;
            }
            var raf = new Raflar
            {
                RafKodu = RafKodu,
                DepoId = SeciliDepo.Id

            };
            _rafService.AddRaf(raf);
            Listele();
            Temizle();
            MessageBox.Show("Raf eklendi.");
        }
        private void Guncelle()
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
            _rafService.UpdateRaf(SeciliRaf);
            Listele();
            MessageBox.Show("Raf güncellendi");
        }
       
        private void Sil()
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
            _rafService.RemoveRaf(SeciliRaf);
            Listele();
            Temizle();
            MessageBox.Show("Raf silindi.");
        }
    }
}
