using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AppInventory.Models 
{ 
     [TableName("pengadaan")] 
     public class pengadaan:BaseNotifyProperty
   {
          [PrimaryKey("PengadaanId")] 
          [DbColumn("PengadaanId")] 
          public int PengadaanId 
          { 
               get{return _pengadaanid;} 
               set{ 
                      _pengadaanid=value; 
                     OnPropertyChange("PengadaanId");
                     }
          } 

          [DbColumn("Kode")] 
          public string Kode 
          { 
               get{return _kode;} 
               set{ 
                      _kode=value; 
                     OnPropertyChange("Kode");
                     }
          } 

          [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get
            {
                if (_tanggal == new DateTime())
                    _tanggal = DateTime.Now;
                return _tanggal;
            }
            set
            {
                _tanggal = value;
                OnPropertyChange("Tanggal");
            }
        } 

          

          [DbColumn("MasaGuna")] 
          public int MasaGuna 
          { 
               get{return _masaguna;} 
               set{ 
                      _masaguna=value; 
                     OnPropertyChange("MasaGuna");
                     }
          } 

          [DbColumn("Harga")] 
          public double Harga 
          { 
               get{return _harga;} 
               set{ 
                      _harga=value; 
                     OnPropertyChange("Harga");
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

          [DbColumn("BarangId")] 
          public int BarangId 
          { 
               get{return _barangid;} 
               set{ 
                      _barangid=value; 
                     OnPropertyChange("BarangId");
                     }
          } 

          [DbColumn("Lokasi")] 
          public int LokasiId 
          { 
               get{return _lokasi;} 
               set{ 
                      _lokasi=value; 
                     OnPropertyChange("LokasiId");
                     }
          } 

          [DbColumn("Kondisi")] 
          public KondisiBarang Kondisi 
          { 
               get{return _kondisi;} 
               set{ 
                      _kondisi=value; 
                     OnPropertyChange("Kondisi");
                     }
          }


        private lokasi lokasi;

        public lokasi Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; OnPropertyChange("Lokasi"); }
        }


        private barang _barang;

        public barang Barang
        {
            get { return _barang; }
            set { _barang = value; OnPropertyChange("Barang"); }
        }

        private BiayaPenyusutan _biayaPenyusutan;

        public BiayaPenyusutan Penyusutan
        {
            get { return _biayaPenyusutan; }
            set { _biayaPenyusutan = value;OnPropertyChange("Penyusutan"); }
        }

        public ObservableCollection<mutasi> ListMutasi { get; internal set; }

        private int  _pengadaanid;
           private string  _kode;
           private DateTime  _tanggal;
           private int  _masaguna;
           private double  _harga;
           private StatusAktif  _statusaktif;
           private int  _barangid;
           private int  _lokasi;
           private KondisiBarang  _kondisi;
    }
}


