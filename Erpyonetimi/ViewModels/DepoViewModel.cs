using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class DepoViewModel : BaseViewModel
    {
        private readonly IDepoService _depoService;
        public ObservableCollection<Depolar> Depolar { get; set; }
        private Depolar? _seciliDepo;
        private string _depaadi = "";
        private List<Depolar> _tumdepolar;
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
        public string Depaadi
        {
            get => _depaadi;
            set
            {
                _depaadi = value;
                OnPropertyChanged();
            }
        }
        private string _konum = "";
        public string Konum
        {
            get => _konum;
            set
            {
                _konum= value;
                OnPropertyChanged();
            }
        }
        public Depolar? SeciliDepo
        {
            get => _seciliDepo;
            set
            {
                _seciliDepo = value;
                if (_seciliDepo!= null)
                {
                    Depaadi = _seciliDepo.Depaadi;
                    Konum = _seciliDepo.Konum;
                }
                OnPropertyChanged();
            }
        }
        public ICommand DepoEkleCommand { get; }
        public ICommand DepoGuncelleCommand { get; }
        public ICommand DepoSilCommand { get; }
        public ICommand DepoTemizleCommand { get; }

        public DepoViewModel(IDepoService depoService)
        {
            _depoService = depoService;
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            DepoEkleCommand = new RelayCommand(Ekle);
            DepoGuncelleCommand = new RelayCommand(Guncelle);
            DepoSilCommand = new RelayCommand(Sil);
            DepoTemizleCommand= new RelayCommand(Temizle);
            _tumdepolar = new List<Depolar>(_depoService.GetAll());
            Depolar = new ObservableCollection<Depolar>(_tumdepolar);
        }
        private void Filtrele()
        {
            if (string.IsNullOrWhiteSpace(AramaMetni))
            {
                Depolar = new ObservableCollection<Depolar>(_tumdepolar);

            }
            else
            {
                var arama = AramaMetni.Trim();
            

            var sonuc =_tumdepolar
                .Where(x => x.Depaadi.ToLower().Contains(AramaMetni.ToLower()) ||
                            x.Konum.ToLower().Contains(AramaMetni.ToLower()))
                .ToList();
            Depolar = new ObservableCollection<Depolar>(sonuc);
            }
            OnPropertyChanged(nameof(Depolar));
        }
        private void Ekle()
        {
            //if(!Aktifmi && Raflar.Count > 0)
            // {
            //     MessageBox.Show("İçinde bulunan depo pasif yapılamaz");
            //     return;
            // }
            if (_depoService.GetByDepoadi(Depaadi) != null )
            {
                MessageBox.Show("Bu isimde bir depo zaten mevcut");
                return;
            }
            if (string.IsNullOrWhiteSpace(Depaadi))
            {
                MessageBox.Show("Depo adı zorunludur.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Konum))
            {
                MessageBox.Show("Konum zorunludur.");
                return;
            }
            var depo = new Depolar
            {
                Depaadi = Depaadi,
                Konum= Konum
            };
            _depoService.AddDepo(depo);
            Depolar.Add(depo);
            MessageBox.Show("Depo eklendi");
            Temizle();
            
        }
        private void Sil()
        {
            if(SeciliDepo== null) return;
            var cevap = MessageBox.Show(
                "Seçili depoyu silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;
            _depoService.DeleteDepo(SeciliDepo);
           
            MessageBox.Show("Depo silindi");
            Temizle();
           
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            OnPropertyChanged(nameof(Depolar));
        }
        private void Guncelle()

        {
        if(string.IsNullOrWhiteSpace(Konum) || string.IsNullOrWhiteSpace(Depaadi))
            {
                MessageBox.Show("Depo adı ve konum zorunludur");
                return;
            }
            if (SeciliDepo == null)
                return;
            
            SeciliDepo.Depaadi = Depaadi;
            SeciliDepo.Konum = Konum;
           

          
            MessageBox.Show("Depo Güncellendi");
            _depoService.UpdateDepo(SeciliDepo);  
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            Listele();
            OnPropertyChanged(nameof(Depolar));
        }
        private void Listele()
        {
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            OnPropertyChanged(nameof(Depolar));
        }

        private void Temizle()
        {
            Depaadi = "";
            Konum = "";
            SeciliDepo = null;
        }
    }
}
