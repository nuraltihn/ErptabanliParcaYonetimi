using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.Win32;
using System.IO;
using System.Threading;
namespace Erpyonetimi.ViewModels
{
    public class RaporViewModel : BaseViewModel
    {
        private readonly IRaporService _raporService;
        private readonly SemaphoreSlim _raporSemaphore = new SemaphoreSlim(1, 1);
        public ObservableCollection<Parca> Parcalar { get; set; }
        = new ObservableCollection<Parca>();

        public ObservableCollection<StokHareket> StokHareketleri { get; set; } = new ObservableCollection<StokHareket>();

        public ObservableCollection<Siparis> Siparisler { get; set; } = new ObservableCollection<Siparis>();
        public ObservableCollection<string> Raporlar { get; set; } = new ObservableCollection<string>
        {
            "Stok Durum Raporu",
            "Kritik Stok Raporu",
            "Stok Hareket Raporu",
            "Sipariş Raporu"
        };
       
        public Visibility StokDurumVisibility=>
            SecilenRapor=="Stok Durum Raporu"?
            Visibility.Visible: Visibility.Collapsed;

        public Visibility KritikStokVisibility =>
           SecilenRapor == "Kritik Stok Raporu" ?
           Visibility.Visible : Visibility.Collapsed;

        public Visibility StokHareketVisibility =>
           SecilenRapor == "Stok Hareket Raporu" ?
           Visibility.Visible : Visibility.Collapsed;

        public Visibility SiparisVisibility =>
           SecilenRapor == "Sipariş Raporu" ?
           Visibility.Visible : Visibility.Collapsed;

        private string _secilenRapor = "Stok Durum Raporu";
        public string SecilenRapor
        {
            get => _secilenRapor;
            set
            {
                if (_secilenRapor == value)
                    return;


                _secilenRapor=value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StokDurumVisibility));
                OnPropertyChanged(nameof(KritikStokVisibility));
                OnPropertyChanged(nameof(StokHareketVisibility));
                OnPropertyChanged(nameof(SiparisVisibility));
                _ = RaporGetirAsync();
            }
        }
        public ICommand ExcelAktarCommand { get; }
      

        public RaporViewModel(IRaporService raporService)
        {
            _raporService = raporService;

            ExcelAktarCommand = new RelayCommand(ExcelAktar);
           

            _ = RaporGetirAsync();
        }

        private async Task RaporGetirAsync()
        {
            await _raporSemaphore.WaitAsync();
            
            try
            {
             
                Parcalar.Clear();
                StokHareketleri.Clear();
                Siparisler.Clear();

                if(SecilenRapor=="Stok Durum Raporu")
                {
                    var sonuc = await _raporService.GetStokDurumuAsync();
                    foreach(var parca in sonuc)
                    {
                        Parcalar.Add(parca);
                    }
                }
                else if(SecilenRapor=="Kritik Stok Raporu")
                {
                    var sonuc = await _raporService.GetKritikStokAsync();
                    foreach(var parca in sonuc)
                    {
                        Parcalar.Add(parca);
                    }
                }
                else if (SecilenRapor=="Stok Hareket Raporu")
                {
                    var sonuc = await _raporService.GetStokHareketleriAsync();
                    foreach (var hareket in sonuc)
                    {
                        StokHareketleri.Add(hareket);
                    }
                }

                else if(SecilenRapor=="Sipariş Raporu")
                {
                    var sonuc = await _raporService.GetSiparislerAsync();
                    foreach(var siparis in sonuc)
                    {
                        Siparisler.Add(siparis);
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Rapor yüklenirken hata oluştu.\n\n" +
                    ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                _raporSemaphore.Release();
               
            }
        }
        private void ExcelAktar()
        {
            try
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Excel Dosyasını Kaydet",
                    Filter = "Excel Dosyası|*.xlsx",
                    FileName = SecilenRapor + ".xlsx"
                };
                if (dialog.ShowDialog() != true)
                    return;
                using var workbook = new XLWorkbook();
                if(SecilenRapor=="Stok Durum Raporu")
                {
                    var sheet = workbook.Worksheets.Add("Stok Durumu");
                    sheet.Cell(1, 1).Value = "Parça Kodu";
                    sheet.Cell(1, 2).Value = "ParçaAdı";
                    sheet.Cell(1, 3).Value = "Marka";
                    sheet.Cell(1, 4).Value = "Kategori";
                    sheet.Cell(1, 5).Value = "Tedarikçi";
                    sheet.Cell(1, 6).Value = "Mevcut Stok";
                    sheet.Cell(1, 7).Value = "Minimum Stok";
                    sheet.Cell(1, 8).Value = "Alış Fiyatı";
                    sheet.Cell(1, 9).Value = "Satış Fiyatı";

                    int row = 2;
                    foreach(var parca in Parcalar) {
                        sheet.Cell(row, 1).Value = parca.ParcaKodu;
                        sheet.Cell(row, 2).Value = parca.ParcAdi;
                        sheet.Cell(row, 3).Value = parca.Marka;
                        sheet.Cell(row, 4).Value = parca.Kategori?.KategoriAdi;
                        sheet.Cell(row, 5).Value = parca.Tedarikci?.FirmaAdi;
                        sheet.Cell(row, 6).Value = parca.MevcutStok;
                        sheet.Cell(row, 7).Value = parca.MinimumStok;
                        sheet.Cell(row, 8).Value = parca.AlisFiyat;
                        sheet.Cell(row, 9).Value = parca.SatisFiyat;

                        row++;
                    }
                    sheet.Columns().AdjustToContents();
                }
                else if (SecilenRapor =="Kritik Stok Raporu")
                {
                    var sheet = workbook.Worksheets.Add("Kritik Stok");
                    sheet.Cell(1, 1).Value = "Parça Kodu";
                    sheet.Cell(1, 2).Value = "Parça Adı";
                    sheet.Cell(1, 3).Value = "Mevcut Stok";
                    sheet.Cell(1, 4).Value = "Minimum Stok";
                    int row = 2;
                    foreach(var parca in Parcalar)
                    {
                        sheet.Cell(row, 1).Value = parca.ParcaKodu;
                        sheet.Cell(row, 2).Value = parca.ParcAdi;
               
                        sheet.Cell(row, 3).Value = parca.MevcutStok;
                        sheet.Cell(row, 4).Value = parca.MinimumStok;

                        row++;
                    }
                    sheet.Columns().AdjustToContents();

                }
                else if(SecilenRapor=="Stok Hareket Raporu")
                {
                    var sheet = workbook.Worksheets.Add("Stok Hareketleri");
                    sheet.Cell(1, 1).Value = "Parça Kodu";
                    sheet.Cell(1, 2).Value = "Parça";
                    sheet.Cell(1, 3).Value = "İşlem";
                    sheet.Cell(1, 4).Value = "Miktar";
                    sheet.Cell(1, 5).Value = "Tarih";
                    sheet.Cell(1, 6).Value = "Depo";
                    sheet.Cell(1, 7).Value = "Kullanıcı";
                    sheet.Cell(1, 8).Value = "Açıklama";

                    int row = 2;

                    foreach(var hareket in StokHareketleri)
                    {
                        sheet.Cell(row, 1).Value = hareket.Parca?.ParcaKodu;
                        sheet.Cell(row, 2).Value = hareket.Parca?.ParcAdi;
                        sheet.Cell(row, 3).Value = hareket.IslemTipi;
                        sheet.Cell(row, 4).Value = hareket.Miktar;
                        sheet.Cell(row, 5).Value = hareket.Tarih;
                        sheet.Cell(row, 6).Value = hareket.Depo?.Depaadi;
                        sheet.Cell(row, 7).Value = hareket.Kullanici?.AdSoyad;
                        sheet.Cell(row, 8).Value = hareket.Aciklama;

                        row++;
                    }
                    sheet.Columns().AdjustToContents();
                }

                else if (SecilenRapor == "Sipariş Raporu")
                {
                    var sheet = workbook.Worksheets.Add("Siparişler");

                    sheet.Cell(1, 1).Value = "Sipariş No";
                    sheet.Cell(1, 2).Value = "Müşteri";
                    sheet.Cell(1, 3).Value = "Sipariş Tarihi";
                    sheet.Cell(1, 4).Value = "Durum";
                    sheet.Cell(1, 5).Value = "Toplam Tutar";

                    int row = 2;

                    foreach (var siparis in Siparisler)
                    {
                        sheet.Cell(row, 1).Value = siparis.SiparisNo;
                        sheet.Cell(row, 2).Value = siparis.Musteri?.FirmaAdi;
                        sheet.Cell(row, 3).Value = siparis.SiparisTarihi;
                        sheet.Cell(row, 4).Value = siparis.Durum;
                        sheet.Cell(row, 5).Value = siparis.ToplamTutar;

                        row++;
                    }

                    sheet.Columns().AdjustToContents();
                }

                workbook.SaveAs(dialog.FileName);

                MessageBox.Show(
                    "Excel dosyası başarıyla oluşturuldu.",
                    "Başarılı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel oluşturulurken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        

       
    }
}
