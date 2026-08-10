using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
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
        private string _rafKodu = "";
        public string RafKodu
        {
            get => _rafKodu;
            set
            {
                _rafKodu= value; OnPropertyChanged(); 
            }
        }

        private string _rafAdi = "";
        
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
        public RafViewModel(IRafService rafService, IDepoService depoService)
        {
            _rafService = rafService;
            _depoService = depoService;
            Raflar = new ObservableCollection<Raflar>(_rafService.GetAll());
            Depolar= new ObservableCollection<Depolar>(_depoService.GetAll());
            RafEkleCommand = new RelayCommand(Ekle);
            RafSilCommand = new RelayCommand(Sil);
            RafGuncelleCommand = new RelayCommand(Guncelle);
            ListeleRafCommand= new RelayCommand(Listele);
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
            if(SeciliDepo == null)
            {
                MessageBox.Show("Depo seçiniz");
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
            if (SeciliRaf == null)
            {
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
            if (SeciliRaf == null)
                return;
            _rafService.RemoveRaf(SeciliRaf);
            Listele();
            Temizle();
            MessageBox.Show("Raf silindi.");
        }
    }
}
