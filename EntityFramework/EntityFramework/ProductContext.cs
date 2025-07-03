using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ExamTask
{
    public class ProductContext : DbContext
    {
        const string ConnectionString = "Data Source=products.db";

        public DbSet<Table> Tables { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);

        }
    }

}
