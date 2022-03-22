using Aguila.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        void Dispose();
        void SaveChanges();
        Task SaveChangeAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        DbTransaction transaccion  { get; }
        IAsigUsuariosEstacionesTrabajoRepository AsigUsuariosEstacionesTrabajoRepository { get; }
        IAsigUsuariosModulosRepository AsigUsuariosModulosRepository { get; }
        IAsigUsuariosRecursosAtributosRepository AsigUsuariosRecursosAtributosRepository { get; }
        IEmpresaRepository EmpresaRepository { get; }
        IEstacionesTrabajoRepository EstacionesTrabajoRepository { get; }
        IModulosMnuRepository ModulosMnuRepository { get; }
        IModulosRepository ModulosRepository { get; }
        IRecursosAtributosRepository RecursosAtributosRepository { get; }
        IRecursosRepository RecursosRepository { get; }
        ISucursalRepository SucursalRepository { get; }
        IUsuariosRepository UsuariosRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUsuariosRolesRepository UsuariosRolesRepository { get; }
        IUsuariosRecursosRepository UsuariosRecursosRepository { get; }
        IImagenesRecursosConfiguracionRepository ImagenesRecursosConfiguracionRepository { get; }
        IImagenesRecursosRepository ImagenesRecursosRepository { get; }
        IImagenesRepository ImagenesRepository { get; }
        IpaisesRepository paisesRepository { get;  }
        ItiposListaRepository tiposListaRepository { get; }
        IdepartamentosRepository departamentosRepository { get; }
        ImunicipiosRepository municipiosRepository { get; }
        IubicacionesRepository ubicacionesRepository { get;  }
        IdireccionesRepository direccionesRepository { get; }
        IlistasRepository listasRepository { get; }
        ItipoReparacionesRepository tipoReparacionesRepository { get; }
        IreparacionesRepository reparacionesRepository { get; }
        IentidadComercialRepository entidadComercialRepository { get; }
        IentidadesComercialesDireccionesRepository entidadesComercialesDireccionesRepository { get;  }
        ItipoClientesRepository tipoClientesRepository { get; }
        IclientesRepository clientesRepository { get; }
        IpilotosTiposRepository pilotosTiposRepository { get; }
        ItipoActivosRepository tipoActivosRepository { get; }
        IactivoGeneralesRepository activoGeneralesRepository { get; }
        IcorporacionesRepository corporacionesRepository { get;  }
        ItipoProveedoresRepository tipoProveedoresRepository { get; }
        ItipoMecanicosRepository tipoMecanicosRepository { get;  }
        IllantaTiposRepository llantaTiposRepository { get;  }
        ItipoEquipoRemolqueRepository tipoEquipoRemolqueRepository { get;  }
        ItipoVehiculosRepository tipoVehiculosRepository { get;  }
        ItipoGeneradoresRepository tipoGeneradoresRepository { get;  }
        IproveedoresRepository proveedoresRepository { get;  }
        ItransportesRepository transportesRepository { get;  }
        IrutasRepository rutasRepository { get;  }
        IserviciosRepository serviciosRepository { get;  }
        IclienteServiciosRepository clienteServiciosRepository { get;  }
        IempleadosRepository empleadosRepository { get;  }
        IasesoresRepository asesoresRepository { get; }
        IpilotosRepository pilotosRepository { get;  }
        IpilotosDocumentosRepository pilotosDocumentosRepository { get;  }
        IactivoOperacionesRepository activoOperacionesRepository { get;  }
        IvehiculosRepository vehiculosRepository { get; }
        ImedidasRepository medidasRepository { get; }
        IinvCategoriaRepository invCategoriaRepository { get;  }
        IinvSubCategoriaRepository invSubCategoriaRepository { get; }
        IproductosRepository productosRepository { get;  }
        IinvProductoBodegaRepository invProductoBodegaRepository { get;  }
        IequipoRemolqueRepository equipoRemolqueRepository { get;  }
        IgeneradoresRepository generadoresRepository { get; }
        IinvUbicacionBodegaRepository invUbicacionBodegaRepository { get; }
        IproductosBusquedaRepository productosBusquedaRepository { get;  }
        IestadosRepository estadosRepository { get; }
        IactivoEstadosRepository activoEstadosRepository { get; }
        IactivoUbicacionesRepository activoUbicacionesRepository { get; }
        ImecanicosRepository mecanicosRepository { get;  }
        IllantasRepository llantasRepository { get;  }
        IllantaActualRepository llantaActualRepository { get; }
        ItarifarioRepository tarifarioRepository { get;  }
        IclienteTarifasRepository clienteTarifasRepository { get;  }
        IcondicionActivosRepository condicionActivosRepository { get; }
        IactivoMovimientosRepository activoMovimientosRepository { get; }
        IactivoMovimientosActualRepository activoMovimientosActualRepository { get; }
        IcondicionCabezalRepository condicionCabezalRepository { get; }
        IcondicionEquipoRepository condicionEquipoRepository { get; }
        IcondicionCisternaRepository condicionCisternaRepository { get; }
        IcondicionFurgonRepository condicionFurgonRepository { get; }
        IcondicionGenSetRepository condicionGenSetRepository { get; }
        IcondicionTecnicaGenSetRepository condicionTecnicaGenSetRepository { get; }
        IconeccionesSistemasRepository coneccionesSistemasRepository { get; }
        IeventosControlEquipoRepository eventosControlEquipoRepository { get; }
        IcontrolVisitasRepository controlVisitasRepository { get; }
        IcontrolContratistasRepository controlContratistasRepository { get; }
        IcontrolEquipoAjenoRepository controlEquipoAjenoRepository { get; }
        IempleadosIngresosRepository empleadosIngresosRepository { get; }
        IcondicionTallerVehiculoRepository condicionTallerVehiculoRepository { get; }
        IdetalleCondicionRepository detalleCondicionRepository { get; }
        IcodigoPostalRepository codigoPostalRepository { get; }
        ICondicionTallerRepository condicionTallerRepository { get; }
        IcondicionContenedorRepository condicionContenedorRepository { get; }
    }
}