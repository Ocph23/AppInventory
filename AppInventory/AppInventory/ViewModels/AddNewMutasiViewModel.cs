using AppInventory.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace AppInventory.ViewModels
{
    internal class AddNewMutasiViewModel:mutasi,IDataErrorInfo
    {
        private string error;
        #region Constructor
        public AddNewMutasiViewModel(pengadaan selectedItem)
        {
            this.PengadaanSelected = selectedItem;
            this.PengadaanId = PengadaanSelected.PengadaanId;
            this.SaveCommand = new CommandHandler { CanExecuteAction = SaveValidate, ExecuteAction = SaveAction };
            this.CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =x=> WindowClose() };
            this.Dari = selectedItem.LokasiId;
            this.Tanggal = DateTime.Now;
            this.PengadaanId = selectedItem.PengadaanId;
            this.Kondisi = selectedItem.Kondisi;
            this.UserId = 1;
            KondisiSourceView = new ObservableCollection<KondisiBarang>(Enum.GetValues(typeof(KondisiBarang))
                              .Cast<KondisiBarang>()
                              .Select(v => v)
                              .ToList());
            using (var db = new OcphDbContext())
            {
                NewLocations = new ObservableCollection<lokasi>(db.Lokasi.Select());
            }
        }
        #endregion

        #region Validate
        private bool SaveValidate(object obj)
        {
            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }
        #endregion

        #region Method
        private void SaveAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    this.MutasiId = db.Mutasi.InsertAndGetLastID(this);
                    if(this.MutasiId>0)
                    {
                        var updated = db.Pengadaan.Update(o => new { o.LokasiId }, new pengadaan { LokasiId = this.Ke }, O => O.PengadaanId == PengadaanId);
                        if( updated)
                        {
                            PengadaanSelected.LokasiId = this.Ke;
                            trans.Commit();
                            this.IsSaved = true;
                            MessageBox.Show("Data Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            WindowClose();
                        }else
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                    }else
                    {
                        throw new SystemException("Data Tidak Tersimpan");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        } 
        #endregion
      
        #region Properties

        public Action WindowClose { get; internal set; }
        public pengadaan PengadaanSelected { get; set; }
        public CommandHandler SaveCommand { get; }
        public CommandHandler CloseCommand { get; }
        private lokasi _SelectedNewLocation;

        public lokasi SelectedNewLocation
        {
            get { return _SelectedNewLocation; }
            set { _SelectedNewLocation = value;
                OnPropertyChange("SelectedNewLocation"); }
        }

        public string Error => this.error;

        public ObservableCollection<lokasi> NewLocations { get; }
        public bool IsSaved { get; private set; }
        public ObservableCollection<KondisiBarang> KondisiSourceView { get; }

        public string this[string columnName] {
            get
            {
                error = string.Empty;
                switch (columnName)
                {
                    case "PengadaanId":
                        if (this.Dari <= 0)
                            error = "Pilih Lokasi Tujuan";
                        break;
                    case "Ke":
                        if (this.Ke == this.Dari)
                            error = "Lokasi Sama Dengan Sebelumnya, \r Pilih Lokasi Yang Lain";
                        break;
                    case "Tanggal":
                        if (Tanggal == new DateTime())
                        {
                            error = "Tentukan Tanggal Mutasi";
                        }
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