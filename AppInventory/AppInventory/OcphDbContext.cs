using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AppInventory
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IRepository<Models.barang> Barang { get { return new Repository<Models.barang>(this); } }
        public IRepository<Models.lokasi> Lokasi { get { return new Repository<Models.lokasi>(this); } }
        public IRepository<Models.mutasi> Mutasi { get { return new Repository<Models.mutasi>(this); } }
        public IRepository<Models.pengadaan> Pengadaan { get { return new Repository<Models.pengadaan>(this); } }
        public IRepository<Models.user> Users { get { return new Repository<Models.user>(this); } }


        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
