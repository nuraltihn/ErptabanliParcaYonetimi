using Erpyonetimi.Domain.Entities;
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
using System.Windows.Shapes;

namespace Erpyonetimi.Views
{
    /// <summary>
    /// Interaction logic for DetayView.xaml
    /// </summary>
    public partial class DetayView : Window
    {
        public DetayView( Siparis siparis)
        {
            InitializeComponent();
            DataContext = siparis;
        }
    }
}
