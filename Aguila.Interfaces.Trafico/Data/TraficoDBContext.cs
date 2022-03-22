using Aguila.Interfaces.Trafico.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Aguila.Interfaces.Trafico.Data
{
    public partial class TraficoDBContext : DbContext
    {
        public TraficoDBContext()
        {

        }     

        public TraficoDBContext(DbContextOptions<TraficoDBContext> options) : base(options)
        {

        }

        //public static readonly ILoggerFactory MyLoggerFactory
        //         = LoggerFactory.Create(builder => { builder.AddConsole(); });



        public virtual DbSet<SolicitudesMovimientos> SolicitudesMovimientos { get; set; }
        public virtual DbSet<SolicitudesMovimientosIntegracion> SolicitudesmovimientosIntegracion { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<TipoContenedor> TipoContenedor { get; set; }
        public virtual DbSet<Movimientos> Movimientos  { get; set; }
        public virtual DbSet<EquipoRemolque> EquipoRemolque { get; set; }
        public virtual DbSet<LocSectores> LocSectores { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<SisEstacionesTrabajo> SisEstacionesTrabajo { get; set; }
        public virtual DbSet<TipoEquipoRemolque> TipoEquipoRemolque { get; set; }
        public virtual DbSet<TiposDocumentos> TiposDocumentos { get; set; }
        public virtual DbSet<TiposMovimientos> TiposMovimientos { get; set; }
        public virtual DbSet<EquiposEstatus> EquiposEstatus { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();

            //Para ejecutar las relaciones(joins) hay que colocar Include cuando esta en false

            optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.EnableSensitiveDataLogging();            

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=srvdev1; DataBase=TraficoCLT; User=AguilaApiInterface; Password=1nt3rf@$3");                
            //}

           // optionsBuilder.UseLoggerFactory(MyLoggerFactory).EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }

    }
}
