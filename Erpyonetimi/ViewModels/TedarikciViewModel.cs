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

        public ICommand EkleCommand { get;}

        private readonly ITedarikciService _tedarikciService;

        public TedarikciViewModel(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;

            EkleCommand = new RelayCommand(Ekle);

            Listele();
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


    }
}
