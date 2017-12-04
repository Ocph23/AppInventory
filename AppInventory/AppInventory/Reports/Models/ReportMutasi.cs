using AppInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory.Reports.Models
{
    public class ReportMutasi
    {
        public DateTime Tanggal { get; internal set; }
        public int UserId { get; internal set; }
        public string AsalNama { get; internal set; }
        public string TujuanNama { get; internal set; }
        public string Merek { get; internal set; }
        public string Satuan { get; internal set; }
        public string Nama { get; internal set; }
        public string User { get; internal set; }
        public string Kode { get; internal set; }
    }
}
