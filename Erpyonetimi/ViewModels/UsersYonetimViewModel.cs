using Erpyonetimi.Commands;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class UsersYonetimViewModel :BaseViewModel
    {
        public ObservableCollection<Users> Userlist { get; set; }

        private Users _selecteduser;
        public Users SelectedUser
        {
            get => _selecteduser;
            set
            {
                _selecteduser = value;
                OnPropertyChanged();
            }
        }

        public ICommand UsersEkleCommand { get; }
        public ICommand UsersGuncelCommand { get; }
        public ICommand UsersSilCommand { get; }
        public UsersYonetimViewModel()
        {
            Userlist = new ObservableCollection<Users>();
            UsersEkleCommand = new RelayCommand(UsersEkleme);
            UsersGuncelCommand = new RelayCommand(UsersGuncelleme);
            UsersSilCommand = new RelayCommand(UsersSilme);
        }

        private void UsersEkleme()
        {
            
        }
        private void UsersGuncelleme()
        {

        }
        private void UsersSilme()
        {

        }
    }
}
