using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using AppInventory.Models;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace AppInventory.ViewModels
{
   public class BarangViewModel:BaseNotifyProperty
   {
        private barang selectedItem;
     


        #region Properties
        private ObservableCollection<Models.barang> Barangs { get; set; }
        public CollectionView BarangView { get; set; }
        public Models.barang SelectedItem {
            get { return selectedItem;}
            set
            {
                selectedItem = value;
                OnPropertyChange("SelectedItem");
            }

        }
        public CommandHandler AddNewItemCommand { get; set; }
        public CommandHandler EditItemCommand { get; set; }
        public CommandHandler CloseCommand { get; }
        public Action WindowClose { get; set; }
        public Func<string, string, MessageDialogStyle, MetroDialogSettings, Task<MessageDialogResult>> ShowMessage { get; internal set; }

        #endregion

        #region Constructor
        public BarangViewModel()
        {
            AddNewItemCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewItemAction };
            EditItemCommand = new CommandHandler { CanExecuteAction = EditValidate, ExecuteAction = EditAction };
            CloseCommand = new CommandHandler { CanExecuteAction =x=>true, ExecuteAction =x=>WindowClose() };
            using (var db = new OcphDbContext())
            {
                Barangs = new ObservableCollection<barang>(db.Barang.Select());
                BarangView = (CollectionView)CollectionViewSource.GetDefaultView(Barangs);
                BarangView.Refresh();
            }
        }
        #endregion



        #region Validate
        public bool EditValidate(object obj)
        {
            if (SelectedItem == null)
                return false;
            else
                return true;
        }
        #endregion

        #region Method
        public void AddNewItemAction(object obj)
        {
            var form = new Views.AddNewBarangView();
            var viewmodel = new ViewModels.AddNewBarangViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                Barangs.Add(viewmodel);
                BarangView.Refresh();
                MessageBox.Show("Data Tersimpan");
            }
        }



        public void EditAction(object obj)
        {
            try
            {
                if (this.SelectedItem == null)
                    throw new ArgumentException("Selected Is Null");
                else
                {
                  
                    var form = new Views.AddNewBarangView();
                    var viewmodel = new ViewModels.AddNewBarangViewModel(SelectedItem) { WindowClose = form.Close };
                    form.DataContext = viewmodel;
                    form.ShowDialog();
                    if (viewmodel.IsSaved)
                    {
                        SelectedItem.Nama = viewmodel.Nama;
                        SelectedItem.Merek = viewmodel.Merek;
                        SelectedItem.Satuan = viewmodel.Satuan;
                        MessageBox.Show("Data Tersimpan");
                    }
                    BarangView.Refresh();

                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion


    }
}
