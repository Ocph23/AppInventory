using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppInventory.Models 
{ 
     [TableName("barang")] 
     public class barang:BaseNotifyProperty  
   {
          [PrimaryKey("BarangId")] 
          [DbColumn("BarangId")] 
          public int BarangId 
          { 
               get{return _barangid;} 
               set{ 
                      _barangid=value; 
                     OnPropertyChange("BarangId");
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

          [DbColumn("Satuan")] 
          public string Satuan 
          { 
               get{return _satuan;} 
               set{ 
                      _satuan=value; 
                     OnPropertyChange("Satuan");
                     }
          } 

          [DbColumn("Merek")] 
          public string Merek 
          { 
               get{return _merek;} 
               set{ 
                      _merek=value; 
                     OnPropertyChange("Merek");
                     }
          } 

          private int  _barangid;
           private string  _nama;
           private string  _satuan;
           private string  _merek;
      }
}


