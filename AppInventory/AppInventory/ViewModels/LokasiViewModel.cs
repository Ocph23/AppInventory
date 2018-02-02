using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppInventory.Models;
using System.Windows;

namespace AppInventory.ViewModels
{
    public class LokasiViewModel:DAL.BaseNotifyProperty
    {
        private lokasi _selected;
        #region Properties
        public CommandHandler AddNewItemCommand { get; set; }
        public CommandHandler EditItemCommand { get; set; }
        public CommandHandler CloseCommand { get; set; }
        public Models.lokasi SelectedItem
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChange("SelectedItem");
            }
        }
        public ObservableCollection<Models.lokasi> LokasiSource { get; set; }
        public CollectionView LokasiView { get; set; }
        public Action WindowClose { get; internal set; }

        #endregion

        #region Constructor
        public LokasiViewModel()
        {
            AddNewItemCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewLocationAction };
            EditItemCommand = new CommandHandler { CanExecuteAction = x => SelectedItem!=null, ExecuteAction = EditLocationAction };
            CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =x=>WindowClose()};

            using (var db = new OcphDbContext())
            {
                LokasiSource = new ObservableCollection<Models.lokasi>(db.Lokasi.Select());
                LokasiView = (CollectionView)CollectionViewSource.GetDefaultView(LokasiSource);
                LokasiView.Refresh();
            }
        }
        #endregion

        #region Method
        public void AddNewLocationAction(object obj)
        {
            var form = new Views.AddNewLocationView();
            var viewmodel = new ViewModels.AddNewLocationViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.IsSaved)
            {
                this.LokasiSource.Add(viewmodel);
                LokasiView.Refresh();
              
            }
        }

        public void EditLocationAction(object obj)
        {
            var form = new Views.AddNewLocationView();
            var viewmodel = new ViewModels.AddNewLocationViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
              var a=  App.Current.MainWindow.DataContext as ViewModels.PengadaanViewModel;
                var source = a.PengadaanSource.Where(O => O.LokasiId == SelectedItem.LokasiId).FirstOrDefault();
                if (source != null)
                {
                    source.Lokasi.Nama = SelectedItem.Nama;
                    a.PengadaanView.Refresh();
                }
                 
                SelectedItem.Nama = viewmodel.Nama;
                SelectedItem.Keterangan = viewmodel.Keterangan;
            }
        }

        #endregion

        #region Validate

        #endregion
    }
}
