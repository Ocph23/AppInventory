using MahApps.Metro.Controls;
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

namespace AppInventory.Views
{
    /// <summary>
    /// Interaction logic for Flash.xaml
    /// </summary>
    public partial class Flash : MetroWindow
    {
        private bool canClose;

        public bool CanClose
        {
            get { return canClose; }
            set { canClose= value; }
        }

        public Flash()
        {
            InitializeComponent();
            this.DataContext = this;
            CanClose = true;
            this.Loaded += Flash_Loaded;
            this.MouseDown += Flash_MouseEnter;
            this.btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Flash_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CanClose = false;
            this.btn.Visibility = Visibility.Visible;
        }

        private async void Flash_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(7000);
            if(CanClose)
                this.Close();

        }
    }
}
