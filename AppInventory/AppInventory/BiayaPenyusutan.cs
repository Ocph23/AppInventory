using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory
{
    public class BiayaPenyusutan
    {
        private double _sisaNilaiBarang;
        private double _lamabulanSejakPembelian;

        public BiayaPenyusutan(DateTime TanggalBeli, int MasaGuna, double HargaBeli)
        {
            this.TanggalBeli = TanggalBeli;
            this.MasaGuna = MasaGuna;
            this.HargaBeli = HargaBeli;
            this.TanggalSekarang = DateTime.Now;
            this.TanggalBerakhir = TanggalBeli.AddMonths(MasaGuna);
            this.LamaBulanSejakPembelian = TanggalSekarang.Subtract(TanggalBeli).Days / (365.2425 / 12);
            this.BiayaPenyusutanPerBulan = HargaBeli / MasaGuna;
            this.TotalPenyusutan = BiayaPenyusutanPerBulan * LamaBulanSejakPembelian;
            this.SisaNilaiBarang = HargaBeli - TotalPenyusutan;
        }

        public void Hitung()
        {
          

        }


        public DateTime TanggalBeli { get; }
        public int MasaGuna { get; }
        public double HargaBeli { get; }
        public DateTime TanggalSekarang { get; }
        public DateTime TanggalBerakhir { get; private set; }
        public double LamaBulanSejakPembelian
        {
            get
            {

                return _lamabulanSejakPembelian;
            }
            set
            {
                if (value > MasaGuna)
                {
                    _lamabulanSejakPembelian = MasaGuna;
                }
                else
                    _lamabulanSejakPembelian = value;

            }
        }

        public double BiayaPenyusutanPerBulan { get; private set; }
        public double TotalPenyusutan { get; private set; }
        public double SisaNilaiBarang
        {
            get
            {
                if (_sisaNilaiBarang < 0)
                    _sisaNilaiBarang = 0;
                return _sisaNilaiBarang;
            }
            set { _sisaNilaiBarang = value; }
        }
    }
}
