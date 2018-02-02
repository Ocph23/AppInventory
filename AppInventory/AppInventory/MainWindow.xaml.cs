using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppInventory.ViewModels;
using System.Windows.Interactivity;

namespace AppInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :MetroWindow
    {

        public MainWindow()
        {
            var flash = new Views.Flash();
            flash.ShowDialog();
            var form = new Views.LoginView();
            var loginViewmodel = new LoginViwModel() {WindowClose=form.Close};
            form.DataContext = loginViewmodel;
            form.ShowDialog();

            if (loginViewmodel.LoginSuccess)
            {
                InitializeComponent();
                this.DataContext = new ViewModels.PengadaanViewModel(this) { WindowClose = this.Close, MessageShow = this.ShowMessageAsync };
            }
            else
                this.Close();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                grid.Focus();
        }
    }
  
}
