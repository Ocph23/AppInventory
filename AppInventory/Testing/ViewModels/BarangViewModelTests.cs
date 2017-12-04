using NUnit.Framework;
using AppInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory.ViewModels.Tests
{
    [TestFixture()]
    public class BarangViewModelTests
    {
        [Test]

        public void EditActionThrowArgumentExeprtionTest()
        {
            var vm = new ViewModels.BarangViewModel();
            var ex = Assert.Throws<ArgumentException>(() => vm.EditItemCommand.Execute(null), "Selected Is Null");
            Assert.IsTrue(ex.Message.Contains("Selected Is Null"));
        }

    }
}