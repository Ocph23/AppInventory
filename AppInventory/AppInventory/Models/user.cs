using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppInventory.Models 
{ 
     [TableName("user")] 
     public class user:BaseNotifyProperty  
   {
          [PrimaryKey("UserId")] 
          [DbColumn("UserId")] 
          public int UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
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

          [DbColumn("UserName")] 
          public string UserName 
          { 
               get{return _username;} 
               set{ 
                      _username=value; 
                     OnPropertyChange("UserName");
                     }
          } 

          [DbColumn("Password")] 
          public string Password 
          { 
               get{return _password;} 
               set{ 
                      _password=value; 
                     OnPropertyChange("Password");
                     }
          } 

          [DbColumn("StatusAktif")] 
          public StatusAktif StatusAktif 
          { 
               get{return _statusaktif;} 
               set{ 
                      _statusaktif=value; 
                     OnPropertyChange("StatusAktif");
                     }
          } 

          private int  _userid;
           private string  _nama;
           private string  _username;
           private string  _password;
           private StatusAktif  _statusaktif;
      }
}


