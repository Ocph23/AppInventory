using System;
using AppInventory.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows;
using System.Linq;

namespace AppInventory.ViewModels
{
    internal class AddNewPengadaanViewModel:pengadaan,IDataErrorInfo
    {
        private pengadaan selectedItem;
        private string error;

        #region Constructor
        public AddNewPengadaanViewModel()
        {
            this.Load();
        }

        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = x => string.IsNullOrEmpty(Error), ExecuteAction = SaveCommandAction };
            CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =x=> WindowClose() };

            using (var db = new OcphDbContext())
            {
                var data = db.Barang.Select();
                BarangsSource = new ObservableCollection<Models.barang>(data);
                BarangViewSource = (CollectionView)CollectionViewSource.GetDefaultView(BarangsSource);
                BarangViewSource.Refresh();

                var locations = db.Lokasi.Select();
                LocationSource = new ObservableCollection<lokasi>(locations);
                LocationSourceView = (CollectionView)CollectionViewSource.GetDefaultView(LocationSource);
                LocationSourceView.Refresh();
                KondisiSourceView = new ObservableCollection<KondisiBarang>(Enum.GetValues(typeof(KondisiBarang))
                                .Cast<KondisiBarang>()
                                .Select(v => v)
                                .ToList());

            }
        }


        private void SaveCommandAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if(PengadaanId<=0)
                    {
                        PengadaanId = db.Pengadaan.InsertAndGetLastID(this);
                        if(this.PengadaanId>0)
                        {
                            IsSaved = true;
                            WindowClose();
                        }else
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                    }else
                    {
                        IsSaved = db.Pengadaan.Update(O => new { O.Harga, O.Kode, O.Kondisi, O.LokasiId, O.MasaGuna, O.StatusAktif, O.Tanggal, O.BarangId }, this,
                            O => O.PengadaanId == this.PengadaanId);
                        if(IsSaved)
                        {
                            WindowClose();
                        }
                        else
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        public AddNewPengadaanViewModel(pengadaan selectedItem)
        {
            this.selectedItem = selectedItem;
        }
        #endregion

        #region Properties
        public Action WindowClose { get; internal set; }
        public bool IsSaved { get; internal set; }
        public ObservableCollection<barang> BarangsSource { get; private set; }
        public CollectionView BarangViewSource { get; private set; }
        public ObservableCollection<lokasi> LocationSource { get; private set; }
        public CollectionView LocationSourceView { get; private set; }
        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CloseCommand { get; private set; }

        public string Error => this.error;

        public ObservableCollection<KondisiBarang> KondisiSourceView { get; private set; }

        public string this[string columnName]
        {
            get
            {
                error = string.Empty;

                switch (columnName)
                {
                    case "Kode":
                        if (string.IsNullOrEmpty(this.Kode))
                            error = "Kode Tirdak Boleh Kosong";
                        break;

                    case "Tanggal":
                        if (Tanggal == new DateTime())
                            error = "Pilih Tanggal";
                        break;
                    case "MasaGuna":
                        if (MasaGuna <= 0)
                            error = "Tentukan Masa Guna";
                        break;

                    case "Harga":
                        if (Harga <= 0)
                            error = "Tentukan Harga Beli Barang";
                        break;
                    case "BarangId":
                        if (BarangId <= 0)
                            error = "Pilih Barang";
                        break;

                    case "LokasiId":
                        if (LokasiId <= 0)
                            error = "Pilih Lokasi Barang";
                        break;
                    case "Kondisi":
                        if (Kondisi== KondisiBarang.None)
                            error = "Pilih Lokasi Barang";
                        break;

                    default:
                        error = string.Empty;
                        break;
                }

                return error;
            }
        }

        #endregion
    }
}