using InterfaceTrafico.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InterfaceTrafico.Data
{
    public partial class TraficoDBContext : DbContext
    {
        public TraficoDBContext()
        {

        }

        public TraficoDBContext(DbContextOptions<TraficoDBContext> options) : base(options)
        {

        }

        public virtual DbSet<SolicitudesMovimientos> SolicitudesMovimientos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=srvdev1; DataBase=TraficoCLT; User=AguilaApiInterface; Password=1nt3rf@$3");                
            //}

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
