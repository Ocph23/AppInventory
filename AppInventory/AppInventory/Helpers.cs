using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppInventory.Models;

namespace AppInventory
{
    public class Helpers
    {
        internal static ObservableCollection<mutasi> GetMotasiHistory(pengadaan value)
        {
            using (var db = new OcphDbContext())
            {
                var data = from a in db.Mutasi.Where(O => O.PengadaanId == value.PengadaanId)
                           join b in db.Lokasi.Select() on a.Dari equals b.LokasiId
                           join c in db.Lokasi.Select() on a.Ke equals c.LokasiId
                           select new mutasi
                           {
                               Dari = a.Dari,
                               Ke = a.Ke,
                               MutasiId = a.MutasiId,
                               PengadaanId = a.PengadaanId,
                               Tanggal = a.Tanggal,
                               UserId = a.UserId,
                               Tujuan = b,
                               Asal = c
                           };
               return new ObservableCollection<mutasi>(data.OrderByDescending(O=>O.Tanggal));
            }
        }
    }
}
