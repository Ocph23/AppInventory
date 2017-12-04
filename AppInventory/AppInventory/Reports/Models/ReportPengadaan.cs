using AppInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory.Reports.Models
{
    public class ReportPengadaan : pengadaan
    {
        public string Nama { get; internal set; }
        public string Satuan { get; internal set; }
        public string Merek { get; internal set; }
        public string NamaLokasi { get; internal set; }
        public double SisaNilai { get; internal set; }
        public string KondisiValue { get; internal set; }
    }
}
