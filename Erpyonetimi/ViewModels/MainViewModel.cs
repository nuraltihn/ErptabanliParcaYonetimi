using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView= value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CurrentView = new TedarikciViewModel();
        }
    }
}
