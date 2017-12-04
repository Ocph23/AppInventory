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
    public class AddNewBarangViewModelTests
    {
        [TestCase("Nama")]
        [TestCase("Satuan")]
        [TestCase("Merek")]
        public void SaveValidation_WillFalse_WhenParameter_NullOrEmptyTest(string nama)
        {
            var VM = new ViewModels.AddNewBarangViewModel();
            VM.Nama = "";
            VM.Merek = "Liqna";
            VM.Satuan = "pcs";

            if (nama == "Merek")
                VM.Merek = "";
            if (nama == "Satuan")
                VM.Satuan = "";
            if (nama == "Nama")
                VM.Nama = "";
            var messageError = VM[nama];
            Assert.IsFalse(string.IsNullOrEmpty(messageError));
        }

        [Test]
        public void SaveValidation_WillTrue_WhenAllPropertiesNotNullOrEmptyTest()
        {
            var VM = new ViewModels.AddNewBarangViewModel();
            VM.Nama = "Meja";
            VM.Merek = "Liqna";
            VM.Satuan = "pcs";

            Assert.IsTrue(string.IsNullOrEmpty(VM.Error));
        }



        [Test()]
        public void EditActionTest()
        {
            var VM = new ViewModels.AddNewBarangViewModel();
            VM.Nama = "Kursi Direktur";
            VM.Merek = "Liqna";
            VM.Satuan = "Pcs";
            VM.BarangId = 1;

            VM.SaveCommand.Execute(null);
            Assert.IsTrue(VM.IsSaved);
        }

        [Test]
        public void SaveActionTest()
        {
            var VM = new ViewModels.AddNewBarangViewModel();
            VM.Nama = "Kursi";
            VM.Merek = "Liqna";
            VM.Satuan = "pcs";
            VM.SaveCommand.Execute(null);
            Assert.IsTrue(VM.IsSaved);
        }


    }
}