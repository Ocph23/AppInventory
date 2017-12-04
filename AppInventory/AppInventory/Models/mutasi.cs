using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppInventory.Models 
{ 
     [TableName("mutasi")] 
     public class mutasi:BaseNotifyProperty  
   {
          [PrimaryKey("MutasiId")] 
          [DbColumn("MutasiId")] 
          public int MutasiId 
          { 
               get{return _mutasiid;} 
               set{ 
                      _mutasiid=value; 
                     OnPropertyChange("MutasiId");
                     }
          } 

          [DbColumn("PengadaanId")] 
          public int PengadaanId 
          { 
               get{return _pengadaanid;} 
               set{ 
                      _pengadaanid=value; 
                     OnPropertyChange("PengadaanId");
                     }
          } 

          [DbColumn("Dari")] 
          public int Dari 
          { 
               get{return _dari;} 
               set{ 
                      _dari=value; 
                     OnPropertyChange("Dari");
                     }
          } 

          [DbColumn("Ke")] 
          public int Ke 
          { 
               get{return _ke;} 
               set{ 
                      _ke=value; 
                     OnPropertyChange("Ke");
                     }
          } 

          [DbColumn("UserId")] 
          public int UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          } 

         

          [DbColumn("Tanggal")] 
          public DateTime Tanggal 
          { 
               get{return _tanggal;} 
               set{ 
                      _tanggal=value; 
                     OnPropertyChange("Tanggal");
                     }
          }

        public lokasi Asal { get; internal set; }
        public lokasi Tujuan { get; internal set; }

        private int  _mutasiid;
           private int  _pengadaanid;
           private int  _dari;
           private int  _ke;
           private int  _userid;
           private string  _mutasicol;
           private DateTime  _tanggal;
      }
}


