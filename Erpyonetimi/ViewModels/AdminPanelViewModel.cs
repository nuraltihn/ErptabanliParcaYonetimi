using Erpyonetimi.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel
    {
        private Object _currentAdminView;
        public Object CurrentAdminView
        {
            get => _currentAdminView;
            set { _currentAdminView = value;
                OnPropertyChanged();
            }
        }

        public ICommand UserYonCommand { get; }
        public ICommand KategoriYonCommand { get; }
        public ICommand TedarikciYonCommand { get; }
        public ICommand ParcaYonCommand { get; }

        public AdminPanelViewModel()
        {
            UserYonCommand = new RelayCommand(Usersyonetim);
            KategoriYonCommand = new RelayCommand(Kategoriyonetim);
            ParcaYonCommand = new RelayCommand(Parcayonetim);
            TedarikciYonCommand = new RelayCommand(Tedarikciyonetim);
        }
        private void Usersyonetim() 
        {
            CurrentAdminView = new UsersYonetimViewModel();
           
        }
        private void Kategoriyonetim()
        {
            CurrentAdminView = new KategoriYonetimViewModel();
        }
        private void Parcayonetim()
        {
            CurrentAdminView= new ParcaYonetimViewModel();
        }
        private void Tedarikciyonetim()
        {
            CurrentAdminView = new TedarikciYonetimViewmodel();
        }
    }
}
