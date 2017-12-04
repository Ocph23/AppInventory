using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppInventory.Models 
{ 
     [TableName("lokasi")] 
     public class lokasi:BaseNotifyProperty  
   {
          [PrimaryKey("LokasiId")] 
          [DbColumn("LokasiId")] 
          public int LokasiId 
          { 
               get{return _lokasiid;} 
               set{ 
                      _lokasiid=value; 
                     OnPropertyChange("LokasiId");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("Keterangan")] 
          public string Keterangan 
          { 
               get{return _keterangan;} 
               set{ 
                      _keterangan=value; 
                     OnPropertyChange("Keterangan");
                     }
          } 

          private int  _lokasiid;
           private string  _nama;
           private string  _keterangan;
      }
}


