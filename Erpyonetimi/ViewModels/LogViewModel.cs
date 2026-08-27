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
    public class LogViewModel : BaseViewModel
    {
        private readonly ILogService _logService;
        public ObservableCollection<Log> Loglar { get; set; }
        public ICommand LogListeleCommand { get; }

        private bool _mesgulMu;
        public bool MesgulMu
        {
            get => _mesgulMu;
            set
            {
                _mesgulMu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Mesguldegil));
            }
        }
        public bool Mesguldegil => !_mesgulMu;
        public LogViewModel(ILogService logService)
        {
            _logService = logService;
            Loglar = new ObservableCollection<Log>();
            LogListeleCommand = new RelayCommand(async ()=> await Listele());

           
        }

        private async Task Listele()
        {
            if (MesgulMu) return;
            MesgulMu = true;
            try {
            var loglar = await _logService.GetAllAsync();
                Loglar.Clear();
                foreach (var log in loglar)
                    Loglar.Add(log);
            }
            catch(Exception ex) {

                MessageBox.Show($"Loglar yüklenirken bir hata oluştu:{ex.Message}");

            }
            finally { MesgulMu = false; }
            
        }
    }
}
