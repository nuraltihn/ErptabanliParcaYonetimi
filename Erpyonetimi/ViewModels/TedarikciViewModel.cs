using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class TedarikciViewModel : BaseViewModel
    {
        
        private string _tedarikciKodu;
        private string _firmaAdi;
        private string _yetkiliKisi;
        private string _tel;
        private string _email;
        private Tedarikci _seciliTedarikci;
        public Tedarikci SeciliTedarikci
        {
            get => _seciliTedarikci;
            set
            {
                _seciliTedarikci = value;
                if (_seciliTedarikci != null)
                {
                    TedarikciKodu = _seciliTedarikci.TedarikciKodu;
                    FirmaAdi = _seciliTedarikci.FirmaAdi;
                    YetkiliKisi = _seciliTedarikci.YetkiliKisi;
                    Tel = _seciliTedarikci.Tel;
                    Email = _seciliTedarikci.Email;
                }
                OnPropertyChanged();
            }
        }
        public string TedarikciKodu
        {
            get => _tedarikciKodu;
            set
            {
                _tedarikciKodu = value;
                OnPropertyChanged();
            }
        }

        
        public string FirmaAdi
        {
            get => _firmaAdi;
            set
            {
                _firmaAdi = value;
                OnPropertyChanged();
            }
        }

        
        public string YetkiliKisi
        {
            get => _yetkiliKisi;
            set { _yetkiliKisi = value; OnPropertyChanged(); }

        }
        
        public string Tel
        {
            get => _tel;
            set { _tel = value; OnPropertyChanged(); }
        }
        
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Tedarikci> Tedarikciler { get; set;  }

        public ICommand TedarikciEkleCommand { get;}
        public ICommand TedarikciGuncelleCommand { get; }
        public ICommand TedarikciSilCommand { get; }

        private readonly ITedarikciService _tedarikciService;

        public TedarikciViewModel(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;

            Tedarikciler = new ObservableCollection<Tedarikci>(
                _tedarikciService.GetAllTedarikci());
            TedarikciEkleCommand = new RelayCommand(Ekle);
            TedarikciGuncelleCommand = new RelayCommand(Guncelle);
            TedarikciSilCommand = new RelayCommand(Sil);
           
        }

        private void Listele()
        {
            Tedarikciler = new ObservableCollection<Tedarikci>(
                _tedarikciService.GetAllTedarikci());

            OnPropertyChanged(nameof(Tedarikciler));
        }

        private void Ekle()
        {
            _tedarikciService.AddTedarikci(
                new Tedarikci
                {
                    TedarikciKodu = TedarikciKodu,
                    FirmaAdi = FirmaAdi,
                    Tel = Tel,
                    Email = Email
                });

            Listele();
        }
        private void Sil()
        {
            if (SeciliTedarikci != null) {
               ;
            _tedarikciService.DeleteTedarikci(SeciliTedarikci.Id);
                Listele();
            }
        }
        private void Guncelle()
        {
            if(SeciliTedarikci != null)
            {
                _tedarikciService.UpdateTedarikci(SeciliTedarikci);
                Listele();
            }
        }

    }
}
