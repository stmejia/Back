using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aguila.Infrastructure.Data
{
    public partial class AguilaDBContext : DbContext
    {
        public AguilaDBContext()
        {

        }

        public AguilaDBContext(DbContextOptions<AguilaDBContext> options) : base(options)
        {
            
        }

        public virtual DbSet<AsigUsuariosEstacionesTrabajo> AsigUsuariosEstacionesTrabajo { get; set; }
        public virtual DbSet<AsigUsuariosModulos> AsigUsuariosModulos { get; set; }
        public virtual DbSet<AsigUsuariosRecursosAtributos> AsigUsuariosRecursosAtributos { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<EstacionesTrabajo> EstacionesTrabajo { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<ModulosMnu> ModulosMnu { get; set; }
        public virtual DbSet<Recursos> Recursos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RecursosAtributos> RecursosAtributos { get; set; }
        public virtual DbSet<Sucursales> Sucursales { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; }
        public virtual DbSet<UsuariosRecursos> UsuariosRecursos { get; set; }
        public virtual DbSet<ImagenRecursoConfiguracion> ImagenesRecursosConfiguracion { get; set; }
        public virtual DbSet<ImagenRecurso> ImagenesRecursos { get; set; }
        public virtual DbSet<Imagen> Imagenes{ get; set; }
        //Nuevas entidades
        public virtual DbSet<paises> paises{ get; set; }
        public virtual DbSet<departamentos> departamentos { get; set; }
        public virtual DbSet<municipios> municipios { get; set; }
        public virtual DbSet<ubicaciones> ubicaciones { get; set; }
        public virtual DbSet<direcciones> direcciones { get; set; }
        public virtual DbSet<entidadComercial> entidadComercial { get; set; }
        public virtual DbSet<entidadesComercialesDirecciones> entidadesComercialesDirecciones { get; set; }
        public virtual DbSet<tipoClientes> tipoClientes { get; set; }
        public virtual DbSet<corporaciones> corporaciones { get; set; }
        public virtual DbSet<clientes> clientes { get; set; }
        public virtual DbSet<pilotosTipos> pilotosTipos { get; set; }
        public virtual DbSet<llantaTipos> llantaTipos { get; set;  }
        public virtual DbSet<tipoMecanicos> tipoMecanicos { get; set; }
        public virtual DbSet<tiposLista> tiposLista { get; set; }
        public virtual DbSet<listas> listas { get; set; }
        public virtual DbSet<tipoReparaciones> tipoReparaciones { get; set; }
        public virtual DbSet<reparaciones> reparaciones { get; set; }
        public virtual DbSet<tipoActivos> tipoActivos { get; set; }
        public virtual DbSet<activoGenerales> activoGenerales { get; set; }
        public virtual DbSet<tipoProveedores> tipoProveedores { get; set; }
        public virtual DbSet<tipoEquipoRemolque> tipoEquipoRemolque { get; set; }
        public virtual DbSet<tipoVehiculos> tipoVehiculos { get; set; }
        public virtual DbSet<tipoGeneradores> tipoGeneradores { get; set; }
        public virtual DbSet<proveedores> proveedores { get; set; }
        public virtual DbSet<transportes> transportes { get; set; }
        public virtual DbSet<rutas> rutas { get; set; }
        public virtual DbSet<servicios> servicios { get; set; }
        public virtual DbSet<clienteServicios> clienteServicios { get; set; }
        public virtual DbSet<empleados> empleados { get; set; }
        public virtual DbSet<asesores> asesores { get; set; }
        public virtual DbSet<pilotos> pilotos { get; set; }
        public virtual DbSet<pilotosDocumentos> pilotosDocumentos { get; set; }
        public virtual DbSet<activoOperaciones> activoOperaciones { get; set; }
        public virtual DbSet<vehiculos> vehiculos { get; set; }
        public virtual DbSet<medidas> medidas { get; set; }
        public virtual DbSet<invCategoria> invCategoria { get; set; }
        public virtual DbSet<invSubCategoria> invSubCategoria { get; set; }
        public virtual DbSet<productos> productos { get; set; }
        public virtual DbSet<invProductoBodega> invProductoBodega { get; set; }
        public virtual DbSet<equipoRemolque> equipoRemolque { get; set; }
        public virtual DbSet<generadores> generadores { get; set; }
        public virtual DbSet<invUbicacionBodega> invUbicacionBodega { get; set; }
        public virtual DbSet<productosBusqueda> productosBusqueda { get; set; }
        public virtual DbSet<estados> estados { get; set; }
        public virtual DbSet<activoEstados> activoEstados { get; set; }
        public virtual DbSet<activoUbicaciones> activoUbicaciones { get; set; }
        public virtual DbSet<mecanicos> mecanicos { get; set; }
        public virtual DbSet<llantas> llantas { get; set; }
        public virtual DbSet<llantaActual> llantaActual { get; set; }
        public virtual DbSet<tarifario> tarifario { get; set; }
        public virtual DbSet<clienteTarifas> clienteTarifas { get; set; }
        public virtual DbSet<condicionActivos> condicionActivos { get; set; }
        public virtual DbSet<activoMovimientos> activoMovimientos { get; set; }
        public virtual DbSet<activoMovimientosActual> activoMovimientosActual { get; set; }
        public virtual DbSet<condicionCabezal> condicionCabezal { get; set; }
        public virtual DbSet<condicionEquipo> condicionEquipo { get; set; }
        public virtual DbSet<condicionCisterna> condicionCisterna { get; set; }
        public virtual DbSet<condicionFurgon> condicionFurgon { get; set; }
        public virtual DbSet<condicionGenSet> condicionGenSet { get; set; }
        public virtual DbSet<condicionTecnicaGenSet> condicionTecnicaGenSet { get; set; }
        public virtual DbSet<coneccionesSistemas> coneccionesSistemas { get; set; }
        public virtual DbSet<eventosControlEquipo> eventosControlEquipo { get; set; }
        public virtual DbSet<controlVisitas> controlVisitas { get; set; }
        public virtual DbSet<controlContratistas> controlContratistas { get; set; }
        public virtual DbSet<controlEquipoAjeno> controlEquipoAjeno { get; set; }
        public virtual DbSet<empleadosIngresos> empleadosIngresos { get; set; }
        public virtual DbSet<condicionTallerVehiculo> condicionTallerVehiculo { get; set; }
        public virtual DbSet<detalleCondicion> detalleCondicion { get; set; }
        public virtual DbSet<Documento> documento { get; set; }
        public virtual DbSet<CondicionTaller> condicionesTaller { get; set; }
        public virtual DbSet<codigoPostal> codigoPostal { get; set; }
        //aqui podemos colocar nuevamente el metedo Onconfiguring si fuera requerido

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies(false);          
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<CondicionTaller>().ToTable("CondicionesTaller");


        }        
    }
}
