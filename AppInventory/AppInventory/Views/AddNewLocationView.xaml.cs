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
using MahApps.Metro.Controls;

namespace AppInventory.Views
{
    /// <summary>
    /// Interaction logic for AddNewLocationView.xaml
    /// </summary>
    public partial class AddNewLocationView : MetroWindow
    {
        private lokasi selectedItem;

        public AddNewLocationView()
        {
            InitializeComponent();
        }

        public AddNewLocationView(lokasi selectedItem)
        {
            this.selectedItem = selectedItem;
        }
    }
}
