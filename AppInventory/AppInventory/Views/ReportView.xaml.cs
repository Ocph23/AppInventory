using MahApps.Metro.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AppInventory.Models;
using AppInventory.Reports.Models;

namespace AppInventory.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : MetroWindow
    {
        public List<pengadaan> DataPengadaan { get; }

        public ReportView(List<Models.pengadaan> datapengadaan)
        {
            InitializeComponent();
            this.DataPengadaan = datapengadaan;
            this.Loaded += ReportView_Loaded;
        }

        private void ReportView_Loaded(object sender, RoutedEventArgs e)
        {


            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.PageWidth;
        }

        private void pengadaan_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.LocalReport.DataSources.Clear();
            List<Reports.Models.ReportPengadaan> data = new List<Reports.Models.ReportPengadaan>();
            foreach (var item in DataPengadaan)
            {
                ReportPengadaan newItem = new ReportPengadaan
                {
                    BarangId = item.BarangId,
                    Harga = item.Harga,
                    Kode = item.Kode,
                    KondisiValue = item.Kondisi.ToString(),
                    Lokasi = item.Lokasi,
                    LokasiId = item.LokasiId,
                    MasaGuna = item.MasaGuna,
                    Nama = item.Barang.Nama,
                    Satuan = item.Barang.Satuan,
                    Merek = item.Barang.Merek,
                    NamaLokasi = item.Lokasi.Nama,
                    SisaNilai = item.Penyusutan.SisaNilaiBarang, Tanggal=item.Tanggal
                };
                data.Add(newItem);
            }

            ReportDataSource DataSet1 = new ReportDataSource();
            DataSet1.Name = "DataSet1";
            DataSet1.Value = data;

            reportViewer.LocalReport.ReportEmbeddedResource = "AppInventory.Reports.Layouts.Pengadaan.rdlc";
            reportViewer.LocalReport.DataSources.Add(DataSet1);
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            reportViewer.ResetPageSettings();
            reportViewer.SetPageSettings(ps);
            reportViewer.RefreshReport();
        }

        private void mutasi_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.LocalReport.DataSources.Clear();

            using (var db = new OcphDbContext())
            {
                var data = from a in db.Mutasi.Select()
                         
                           join b in db.Lokasi.Select() on a.Dari equals b.LokasiId
                           join c in db.Lokasi.Select() on a.Ke equals c.LokasiId
                           join d in db.Pengadaan.Select() on a.PengadaanId equals d.PengadaanId
                           join f in db.Barang.Select() on d.BarangId equals f.BarangId
                           join u  in db.Users.Select() on a.UserId equals u.UserId
                           select new ReportMutasi
                           {
                               Tanggal = a.Tanggal,
                               UserId = a.UserId,
                               AsalNama=b.Nama,
                               TujuanNama=c.Nama,
                               Nama=f.Nama,
                               Satuan=f.Satuan,
                               Merek=f.Merek,
                               User=u.Nama,Kode=d.Kode
                           };

                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1";
                DataSet1.Value = data;

                reportViewer.LocalReport.ReportEmbeddedResource = "AppInventory.Reports.Layouts.Mutasi.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = true;
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                reportViewer.ResetPageSettings();
                reportViewer.SetPageSettings(ps);
                reportViewer.RefreshReport();
            }

           
        }

        private void lokasi_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.LocalReport.DataSources.Clear();

            using (var db = new OcphDbContext())
            {
                var data = db.Lokasi.Select().ToList();

                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1";
                DataSet1.Value = data;


                reportViewer.LocalReport.ReportEmbeddedResource = "AppInventory.Reports.Layouts.Lokasi.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = false;
               
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;

                reportViewer.ResetPageSettings();
                reportViewer.SetPageSettings(ps);
                reportViewer.RefreshReport();
            }
        }

        private void barang_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.LocalReport.DataSources.Clear();

            using (var db = new OcphDbContext())
            {
                var data = db.Barang.Select();

                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1";
                DataSet1.Value = data;

                reportViewer.LocalReport.ReportEmbeddedResource = "AppInventory.Reports.Layouts.Barang.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);

                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = false;
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                reportViewer.ResetPageSettings();
                reportViewer.SetPageSettings(ps);

                reportViewer.RefreshReport();
            }
        }
    }
}
