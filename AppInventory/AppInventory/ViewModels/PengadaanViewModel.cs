using AppInventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MahApps.Metro.Controls.Dialogs;

namespace AppInventory.ViewModels
{
    public class PengadaanViewModel:DAL.BaseNotifyProperty
    {
        private MainWindow mainWindow;
        private pengadaan _selected;
        #region Constructor

        public PengadaanViewModel(MainWindow mainWindow)
        {
            this.AddNewItemPengadaanCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewItemAction };
            this.EditItemCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditItemAction };
            this.MutasiCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = MutasiAction };
            //Menu
            this.BarangCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = BarangCommandAction };
            this.ListMutasiCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = ListMutasiCommandAction };
            this.LocationCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = LocationCommandAction };
            this.ReportCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = ReportCommandActiom };
            using (var db = new OcphDbContext())
            {
                var data = (from a in db.Pengadaan.Select()
                            join b in db.Barang.Select() on a.BarangId equals b.BarangId
                            join c in db.Lokasi.Select() on a.LokasiId equals c.LokasiId
                            select new pengadaan
                            {
                                BarangId = a.BarangId,
                                Harga = a.Harga,
                                Kode = a.Kode,
                                Kondisi = a.Kondisi,
                                Lokasi = c,
                                LokasiId = a.LokasiId,
                                MasaGuna = a.MasaGuna,
                                PengadaanId = a.PengadaanId,
                                StatusAktif = a.StatusAktif,
                                Tanggal = a.Tanggal,
                                Barang = b
                            }).ToList();

                foreach(var item in data)
                {
                    item.Penyusutan = new BiayaPenyusutan(item.Tanggal, item.MasaGuna, item.Harga);
                }
                PengadaanSource = new ObservableCollection<Models.pengadaan>(data);
                PengadaanView = (CollectionView)CollectionViewSource.GetDefaultView(PengadaanSource);
                PengadaanView.Refresh();
                BarangSource = new ObservableCollection<barang>(db.Barang.Select());
                BarangSourceView = (CollectionView)CollectionViewSource.GetDefaultView(BarangSource);
                LokasiSource = new ObservableCollection<lokasi>(db.Lokasi.Select());
                BarangSourceView.Refresh();

                var locations = db.Lokasi.Select();

            }
            this.mainWindow = mainWindow;
        }

        private void ReportCommandActiom(object obj)
        {
            var form = new Views.ReportView(PengadaanSource.ToList());
            mainWindow.Hide();
            form.ShowDialog();
            mainWindow.Show();
        }

        private void LocationCommandAction(object obj)
        {
            var form = new Views.LocationView();
            var vm = new ViewModels.LokasiViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            mainWindow.Hide();
            form.ShowDialog();
            mainWindow.Show();
        }

        private void ListMutasiCommandAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void BarangCommandAction(object obj)
        {
            var form = new Views.BarangView();
            var vm = new ViewModels.BarangViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            mainWindow.Hide();
            form.ShowDialog();
            mainWindow.Show();

        }


        #endregion

        #region Properties
        public Models.pengadaan SelectedItem {
            get { return _selected; }
            set
            {
                _selected = value;
                if(_selected.ListMutasi==null)
                {
                    _selected.ListMutasi = Helpers.GetMotasiHistory(value);
                   
                }
                   
                OnPropertyChange("SelectedItem");
            }
        }
        public CommandHandler AddNewItemPengadaanCommand { get; set; }
        public CommandHandler MutasiCommand { get; set; }
        public CommandHandler EditItemCommand { get; }
        //menu
        public CommandHandler BarangCommand { get; set; }
        public CommandHandler ListMutasiCommand { get; set; }
        public CommandHandler LocationCommand { get; set; }
        public CommandHandler ReportCommand { get; set; }

       //endmenu

        public ObservableCollection<Models.pengadaan> PengadaanSource { get; set; }
        public ObservableCollection<lokasi> LokasiSource { get; set; }
        public CollectionView PengadaanView { get; set; }
        public ObservableCollection<barang> BarangSource { get; set; }
        public CollectionView BarangSourceView { get; }
        public Action WindowClose { get; internal set; }
        public Func<string, string, MessageDialogStyle, MetroDialogSettings, Task<MessageDialogResult>> MessageShow { get; internal set; }
        #endregion


        #region Validation

        #endregion

        #region Method
        private void AddNewItemAction(object obj)
        {
            var form = new Views.AddNewPengadaanView();
            var viewmodel = new ViewModels.AddNewPengadaanViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.IsSaved)
            {
                PengadaanSource.Add(viewmodel);
                PengadaanView.Refresh();
            }
        }
        private void MutasiAction(object obj)
        {
            var form = new Views.AddNewMutasiView();
            var viewmodel = new ViewModels.AddNewMutasiViewModel(SelectedItem) { WindowClose = form.Close }; ;
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.IsSaved)
            {
                SelectedItem.LokasiId = viewmodel.Ke;
                SelectedItem.Lokasi = viewmodel.SelectedNewLocation;
            }
        }

        private void EditItemAction(object obj)
        {
            var form = new Views.AddNewPengadaanView();
            var viewmodel = new ViewModels.AddNewPengadaanViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
           
        }
        #endregion




    }
}
