using System;
using AppInventory.Models;
using System.ComponentModel;
using System.Windows;

namespace AppInventory.ViewModels
{
    internal class AddNewLocationViewModel:Models.lokasi,IDataErrorInfo
    {
        private lokasi selectedItem;
        private string error;

        #region Constructor
        public AddNewLocationViewModel()
        {
            this.Load();
        }

        public AddNewLocationViewModel(lokasi selectedItem)
        {
            this.selectedItem = selectedItem;
            this.Nama = selectedItem.Nama;
            this.Keterangan = selectedItem.Keterangan;
            this.LokasiId = selectedItem.LokasiId;
            this.Load();
        }

        public string this[string columnName]
        {
            get
            {
                error = string.Empty;
                if(columnName=="Nama")
                {
                    if (string.IsNullOrEmpty(this.Nama))
                        error = "Nama Lokasi Tidak Boleh Kosong";
                }

                if(columnName=="Keterangan")
                {
                    if (string.IsNullOrEmpty(this.Keterangan))
                        error = "Keterangan Tidak Boleh Kosong";
                }

                return error;

            }
        }
        #endregion

        #region Properties
        public Action WindowClose { get; set; }
        public bool IsSaved { get; internal set; }
        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CloseCommand { get; private set; }

        public string Error => this.error;
        #endregion


        #region Method
        private void Load()
        {
            this.SaveCommand = new CommandHandler { CanExecuteAction = SaveValiation, ExecuteAction = SaveAction };
            this.CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =x=> WindowClose() };
        }

        private void SaveAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if(this.LokasiId<=0)
                    {
                        this.LokasiId = db.Lokasi.InsertAndGetLastID(this);
                        if (LokasiId > 0)
                        {
                            IsSaved = true;
                            MessageBox.Show("Data Tersimpan ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            WindowClose();
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }else
                    {
                        IsSaved = db.Lokasi.Update(o => new { o.Nama, o.Keterangan }, this, O => O.LokasiId == this.LokasiId);
                        if (IsSaved)
                        {
                            MessageBox.Show("Data Tersimpan ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            WindowClose();
                        }
                         
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool SaveValiation(object obj)
        {
            if (string.IsNullOrEmpty(Error))
                return true;
            else
                return false;
        }
        #endregion
    }
}