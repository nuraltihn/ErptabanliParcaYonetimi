using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Services;
using Erpyonetimi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class KategoriYonetimViewModel : BaseViewModel
    {
        private readonly IKategoriService _kategoriService;
        public ObservableCollection<Kategori> KategoriList { get; set; }
        private Kategori _selectedKategori;
        public Kategori SelectedKategori
        {
            get => _selectedKategori;
            set
            {
                _selectedKategori = value;
                OnPropertyChanged();
            }
        }
        public ICommand KategoriEkleCommand { get; }
        public ICommand KategoriGuncelleCommand { get; }
        public ICommand KategoriSilCommand { get; }

        public KategoriYonetimViewModel()
        {
            var context = new ErpDbContextFactory().CreateDbContext(Array.Empty<string>());
            _kategoriService = new KategoriService(new KategoriRepository(context));

            KategoriList = new ObservableCollection<Kategori>(
                _kategoriService.GetAllKategori());
            KategoriEkleCommand = new RelayCommand(KategoriEkle);
            KategoriGuncelleCommand = new RelayCommand(KategoriGuncelle);
            KategoriSilCommand = new RelayCommand(KategoriSil);
        }

        private void KategoriEkle()
        {

        }
        private void KategoriGuncelle()
        {

        }
        private void KategoriSil()
        {

        }
    }
}
