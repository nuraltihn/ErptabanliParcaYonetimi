using Erpyonetimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Erpyonetimi.Views
{
    /// <summary>
    /// LoginView.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private void Passwordkutu_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if(DataContext is LoginViewModel vm)
            {
                vm.Sifre = PasswordBox.Password;
            }
        }
    }
}
