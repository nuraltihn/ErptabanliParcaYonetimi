using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
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
        private string _adres;
        private string _vergino;
        private string _fax;
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
        public Visibility AdminButtonVisibil
        {
            get
            {
                return UserSession.IsAdmin
                    ? Visibility.Visible
                    : Visibility.Collapsed;
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
        public string Adres
        {
            get => _adres;
            set
            {
                _adres = value;
                OnPropertyChanged();
            }
        }
        public string VergiNo
        {
            get => _vergino;
            set
            {
                _vergino = value;
                OnPropertyChanged();
            }
        }
        public string Fax
        {
            get => _fax;
            set
            {
                _fax = value;
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
        private List<Tedarikci> _tumtedarikciler
;        private string _aramaMetni;
        public string AramaMetni
        {
            get => _aramaMetni;
            set { _aramaMetni = value; OnPropertyChanged();
                Filtrele();
            }
        }
        public ObservableCollection<Tedarikci> Tedarikciler { get; set;  }

        public ICommand TedarikciEkleCommand { get;}
        public ICommand TedarikciGuncelleCommand { get; }
        public ICommand TedarikciSilCommand { get; }
        public ICommand TedarikciListeleCommand { get; }
        private readonly ITedarikciService _tedarikciService;

        public TedarikciViewModel(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;

            Tedarikciler = new ObservableCollection<Tedarikci>(
                _tedarikciService.GetAllTedarikci());
            TedarikciEkleCommand = new RelayCommand(Ekle);
            TedarikciGuncelleCommand = new RelayCommand(Guncelle);
            TedarikciSilCommand = new RelayCommand(Sil);
            TedarikciListeleCommand = new RelayCommand(Listele);
            _tumtedarikciler = _tedarikciService.GetAllTedarikci();
            Tedarikciler = new ObservableCollection<Tedarikci>(_tumtedarikciler);
           
        }
        private void Filtrele()
        {
            Tedarikciler = new ObservableCollection<Tedarikci>(_tedarikciService.GetAllTedarikci()
                .Where(x => string.IsNullOrWhiteSpace(AramaMetni)
                    || x.FirmaAdi?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true
                    || x.TedarikciKodu?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true
                    || x.YetkiliKisi?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true));
            OnPropertyChanged(nameof(Tedarikciler));
        }

        private void Listele()
        {
            Tedarikciler = new ObservableCollection<Tedarikci>(
                _tedarikciService.GetAllTedarikci());

            OnPropertyChanged(nameof(Tedarikciler));
        }

        private void Ekle()
        {
            if (TedarikciKodu == null || FirmaAdi==null|| YetkiliKisi==null)
                return;
            if (!UserSession.IsAdmin)
                return;

            _tedarikciService.AddTedarikci(
                new Tedarikci
                {
                    TedarikciKodu = TedarikciKodu,
                    FirmaAdi = FirmaAdi,
                    YetkiliKisi = YetkiliKisi,
                    Tel = Tel,
                    Email = Email,
                    Adres= Adres,
                    VergiNo= VergiNo,
                    Fax= Fax
                    
                });

            Listele();
        }
        private void Sil()
        {
            if (!UserSession.IsAdmin)
                return;  


            if (SeciliTedarikci != null) {
               ;
            _tedarikciService.DeleteTedarikci(SeciliTedarikci.Id);
                Listele();
            }
        }
        private void Guncelle()
        {
            
            if (!UserSession.IsAdmin)
                return;

            if (SeciliTedarikci == null)
                return;

            SeciliTedarikci.TedarikciKodu = TedarikciKodu;
            SeciliTedarikci.FirmaAdi= FirmaAdi;
            SeciliTedarikci.YetkiliKisi= YetkiliKisi;
            SeciliTedarikci.Tel= Tel;
            SeciliTedarikci.Email = Email;
            SeciliTedarikci.Adres = Adres;
            SeciliTedarikci.VergiNo = VergiNo;
            SeciliTedarikci.Fax = Fax;


            _tedarikciService.UpdateTedarikci(SeciliTedarikci);
            Listele();
           
        }

    }
}
