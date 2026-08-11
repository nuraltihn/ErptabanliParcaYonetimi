using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Erpyonetimi.ViewModels
{
    public class SiparisViewModel : BaseViewModel
    {
        private readonly ISiparisService _siparisService;
        private readonly IMusteriService _musteriService;
        public ObservableCollection<Siparis> Siparisler { get; }
        public ObservableCollection<Musteri> Musterier { get; }


        private Musteri? _seciliMusteri;
        public Musteri? SeciliMusteri
        {
            get => _seciliMusteri;
            set
            {
                _seciliMusteri = value;
                OnPropertyChanged();
            }
        }
        private string _siparisNo = "";
        public string SiparisNo
        {
            get => _siparisNo;
            set
            {
                _siparisNo = value;
                OnPropertyChanged();
            }
        }

       
    }
}
