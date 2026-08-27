using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class DepoViewModel : BaseViewModel
    {
        private readonly IDepoService _depoService;

        private ObservableCollection<Depolar> _depolar = new();
        public ObservableCollection<Depolar> Depolar
        {
            get => _depolar;
            set
            {
                _depolar = value;
                OnPropertyChanged();
            }
        }
        

        private Depolar? _seciliDepo;

        private string _depaadi = "";
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
                _konum = value;
                OnPropertyChanged();
            }
        }

        private List<Depolar> _tumdepolar = new List<Depolar>();

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

        public Depolar? SeciliDepo
        {
            get => _seciliDepo;
            set
            {
                _seciliDepo = value;

                if (_seciliDepo != null)
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

            Depolar = new ObservableCollection<Depolar>();

            DepoEkleCommand = new RelayCommand(async ()=> await Ekle());
            DepoGuncelleCommand = new RelayCommand(async ()=> await Guncelle());
            DepoSilCommand = new RelayCommand(async ()=>await Sil());
            DepoTemizleCommand = new RelayCommand(Temizle);

            _ = YukleAsync();
         
        }
        private async Task YukleAsync()
        {
            await Listele();
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

                var sonuc = _tumdepolar
                    .Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Depaadi) &&
                         x.Depaadi.Contains(arama, StringComparison.OrdinalIgnoreCase))
                        ||
                        (!string.IsNullOrWhiteSpace(x.Konum) &&
                         x.Konum.Contains(arama, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();

                Depolar = new ObservableCollection<Depolar>(sonuc);
            }

            
        }

        private async Task Ekle()
        {
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

            var mevcut = await _depoService.GetByDepoadiAsync(Depaadi);

            if (mevcut != null)
            {
                MessageBox.Show("Bu isimde bir depo zaten mevcut.");
                return;
            }

            var depo = new Depolar
            {
                Depaadi = Depaadi,
                Konum = Konum
            };

            await _depoService.AddDepoAsync(depo);

            MessageBox.Show("Depo eklendi.");

            Temizle();

            await Listele();
        }

        private async Task Sil()
        {
            if (SeciliDepo == null)
            {
                MessageBox.Show("Lütfen bir depo seçiniz.");
                return;
            }

            var cevap = MessageBox.Show(
                "Seçili depoyu silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (cevap != MessageBoxResult.Yes)
                return;

            await _depoService.DeleteDepoAsync(SeciliDepo);

            MessageBox.Show("Depo silindi.");

            Temizle();

            await Listele();
        }

        private async Task Guncelle()
        {
            if (SeciliDepo == null)
            {
                MessageBox.Show("Lütfen bir depo seçiniz.");
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

            SeciliDepo.Depaadi = Depaadi;
            SeciliDepo.Konum = Konum;

            await _depoService.UpdateDepoAsync(SeciliDepo);

            MessageBox.Show("Depo güncellendi.");

            await Listele();
        }

        private async Task Listele()
        {
            try { 
            var depolar = await _depoService.GetAllAsync();

            _tumdepolar = depolar?.ToList() ?? new List<Depolar>();
                Filtrele();
}
            catch(Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken bir hata oluştu:{ ex.Message}");
            }
        }

        private void Temizle()
        {
            Depaadi = "";
            Konum = "";
            SeciliDepo = null;
        }
    }
}