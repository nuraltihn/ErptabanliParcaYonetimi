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

        public DepoViewModel(IDepoService depoService)
        {
            _depoService = depoService;
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            DepoEkleCommand = new RelayCommand(Ekle);
            DepoGuncelleCommand = new RelayCommand(Guncelle);
            DepoSilCommand = new RelayCommand(Sil);
            
        }

        private void Ekle()
        {
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
            if (SeciliDepo == null)
                return;

            _depoService.DeleteDepo(SeciliDepo);
           
            MessageBox.Show("Depo silindi");
            Temizle();
          
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            OnPropertyChanged(nameof(Depolar));
        }
        private void Guncelle()
        {
            if (SeciliDepo == null)
                return;
            SeciliDepo.Depaadi = Depaadi;
            SeciliDepo.Konum = Konum;
            _depoService.UpdateDepo(SeciliDepo);

            Listele();
            MessageBox.Show("Depo Güncellendi");
            _depoService.UpdateDepo(SeciliDepo);
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            OnPropertyChanged(nameof(Depolar));
        }
        private void Listele()
        {
            Depolar = new ObservableCollection<Depolar>(_depoService.GetAll());
            OnPropertyChanged(nameof(Listele));
        }

        private void Temizle()
        {
            Depaadi = "";
            Konum = "";
            SeciliDepo = null;
        }
    }
}
