using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    [Serializable]
    class FilePermissionException : Exception
    {
        public FilePermissionException()
            : base ( String.Format ( "No write permission to database folder" ) )
        {

        }

    }
    class DBConnection : DbContext
    {
        private static bool _created = false;
        public DBConnection()
        {
            if ( !_created )
            {
                //_created = true;
                //Database.EnsureDeleted();
                try
                {
                    Database.EnsureCreated ();
                }
                catch
                { }
            }
        }
        protected override void OnConfiguring( DbContextOptionsBuilder optionbuilder )
        {
            //optionbuilder.UseSqlite(@"Data Source=c:\AgricoveBilling.db");
            //optionbuilder.UseSqlite(@"Data Source=%USERPROFILE%\AgricoveBilling.db");
            optionbuilder.UseSqlite ( @"Data Source=..\AgricoveBilling.db" );
        }

        public DbSet<Invoice> Invoice { get; set; }
        //public DbSet<ItemData> ItemData { get; set; }
        public DbSet<Items> Items { get; set; }
    }
}
