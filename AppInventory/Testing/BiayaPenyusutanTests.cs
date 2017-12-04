using NUnit.Framework;
using AppInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory.Tests
{
    [TestFixture()]
    public class BiayaPenyusutanTests
    {
        private BiayaPenyusutan menyusutan = new BiayaPenyusutan(new DateTime(2016, 12, 3), 12, 12000000);
        [Test()]
        public void HitungSisabarangTest()
        {
           
            menyusutan.Hitung();
            Assert.AreEqual(0, menyusutan.SisaNilaiBarang);

        }

        [Test()]
        public void HitungBiayaPenyusutanPerbulanTest()
        {
            menyusutan.Hitung();
            Assert.AreEqual(1000000,menyusutan.BiayaPenyusutanPerBulan);

        }

        [Test()]
        public void HitungTotalPenyusutanHinggaSekarang()
        {
            menyusutan.Hitung();
            Assert.AreEqual(12000000, menyusutan.TotalPenyusutan);

        }
    }
}