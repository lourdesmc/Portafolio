using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOLOTOL.Models;

namespace YOLOTOL.Data
{
    public class YolotolContext : DbContext
    {
        public YolotolContext(DbContextOptions<YolotolContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<Productos> Producto { get; set; }
        public DbSet<Categorias> Categoria { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=tcp:yolotoldb.database.windows.net,1433;Initial Catalog=YOLOTOL;Persist Security Info=False;User ID=yolotolb5;Password=YOLOTOL2021-2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
          // options.UseSqlServer("Server=LA-MAMALONA;Database=YOLOTOL;Trusted_Connection=True;");

        }

        
    }
}
