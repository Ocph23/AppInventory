using System;
using System.ComponentModel;
using System.Windows;
using AppInventory.Models;

namespace AppInventory.ViewModels
{
    public class AddNewBarangViewModel:Models.barang,IDataErrorInfo
    {
        #region Fields
          private string error;
        private barang selectedItem;
        #endregion

        #region Constructor
        public AddNewBarangViewModel()
        {
            this.Load();
        }

        public AddNewBarangViewModel(barang selectedItem)
        {
            this.selectedItem = selectedItem;
            this.BarangId = selectedItem.BarangId;
            this.Nama = selectedItem.Nama;
            this.Satuan = selectedItem.Satuan;
            this.Merek = selectedItem.Merek;
            this.Load();

        }
        #endregion

        #region Properties
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CloseCommand { get; set; }
        public Action WindowClose { get; internal set; }
        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChange("Error");
            }
        }

        public bool IsSaved { get; private set; }

        public string this[string columnName]
        {
            get
            {
                this.error = string.Empty;
                if(columnName=="Merek")
                {
                    if (string.IsNullOrEmpty(this.Merek))
                        this.error = "Merek Tidak Boleh Kosong";
                }

                if(columnName=="Nama")
                {
                    if(string.IsNullOrEmpty(this.Nama))
                    {
                        this.error = "Nama Tidak Boleh Kosong";
                    }
                }
                if (columnName == "Satuan")
                {
                    if (string.IsNullOrEmpty(this.Satuan))
                    {
                        this.error = "Satuan Tidak Boleh Kosong";
                    }
                }

                return error;
            }
        }
        #endregion

        #region Validate
        private bool SaveValidate(object obj)
        {
            if (string.IsNullOrEmpty(this.Error))
                return true ;
            else
                return false;

        }
        #endregion

        #region Method
        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidate, ExecuteAction = SaveAction };
            CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        public void SaveAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if(this.BarangId==0)
                    {
                        BarangId = db.Barang.InsertAndGetLastID(this);
                        if(BarangId>0)
                        {
                            IsSaved = true;
                            MessageBox.Show("Data Tersimpan ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            WindowClose();
                        }
                        else
                        {
                            throw new ArgumentException("Gagal Tersimpan");
                        }
                    }else
                    {
                        var isUpdated = db.Barang.Update(O => new { O.Nama, O.Satuan, O.Merek },this, O=>O.BarangId==this.BarangId);
                        if (isUpdated)
                        {
                            IsSaved = true;
                            MessageBox.Show("Data Tersimpan ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            WindowClose();
                        }
                        else
                        {
                            throw new ArgumentException("Gagal Tersimpan");
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion
     

    }
}