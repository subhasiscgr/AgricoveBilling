using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    class DBConnection : DbContext
    {
        private static bool _created = false;
        public DBConnection()
        {
            if (!_created)
            {
                //_created = true;
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=c:\AgricoveBilling.db");
        }

        public DbSet<Invoice> Invoice { get; set; }
        //public DbSet<ItemData> ItemData { get; set; }
        public DbSet<Items> Items { get; set; }
    }
}
