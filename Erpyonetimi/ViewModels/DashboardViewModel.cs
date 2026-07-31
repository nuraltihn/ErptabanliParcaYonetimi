using Erpyonetimi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Helpers;
using Erpyonetimi.Data.Helpers;

namespace Erpyonetimi.ViewModels
{
    public class DashboardViewModel
    {
        public string KullaniciAdi => UserSession.CurrentUser?.AdSoyad;
        public string RolAdi => UserSession.CurrentUser?.Rol?.RolAdi;
        private readonly DashboardService _dashboardservice;
        
        
        public int Tedarikcisayisi { get; set; }
        public int Parcasayisi { get; set; }
        public int Musterisayisi { get; set; }
        public int Siparissayisi { get; set; }

        
        public DashboardViewModel()
        {
            _dashboardservice = new DashboardService();

            Tedarikcisayisi = _dashboardservice.Tedarikcisayial();
            Parcasayisi = _dashboardservice.Parcasayisial();
            Musterisayisi = _dashboardservice.Musterisayial();
            Siparissayisi = _dashboardservice.Siparissayiisial();
        }
    }
}
