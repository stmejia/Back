using Aguila.Core.Exceptions;
using Aguila.Infrastructure.Data;
using Aguila.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AguilaDBContext _context;
        private DbTransaction _transaccion;
        private readonly IAsigUsuariosEstacionesTrabajoRepository _asigUsuariosEstacionesTrabajoRepository;
        private readonly IAsigUsuariosModulosRepository _asigUsuariosModulosRepository;
        private readonly IAsigUsuariosRecursosAtributosRepository _asigUsuariosRecursosAtributosRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstacionesTrabajoRepository _estacionesTrabajoRepository;
        private readonly IModulosMnuRepository _modulosMnuRepository;
        private readonly IModulosRepository _modulosRepository;
        private readonly IRecursosAtributosRepository _recursosAtributosRepository;
        private readonly IRecursosRepository _recursosRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUsuariosRolesRepository _usuariosRolesRepository;
        private readonly IUsuariosRecursosRepository _usuariosRecursosRepository;
        private readonly IImagenesRecursosConfiguracionRepository _imagenesRecursosConfiguracionRepository;
        private readonly IImagenesRecursosRepository _imagenesRecursosRepository;
        private readonly IImagenesRepository _imagenesRepository;
        private readonly IpaisesRepository _paisesRepository;
        private readonly ItiposListaRepository _tiposListaRepository;
        private readonly IdepartamentosRepository _departamentosRepository;
        private readonly ImunicipiosRepository _municipiosRepository;
        private readonly IubicacionesRepository _ubicacionesRepository;
        private readonly IdireccionesRepository _direccionesRepository;
        private readonly IentidadComercialRepository _entidadComercialRepository;
        private readonly IentidadesComercialesDireccionesRepository _entidadesComercialesDireccionesRepository;
        private readonly ItipoClientesRepository _tipoClientesRepository;
        private readonly IclientesRepository _clientesRepository;
        private readonly IcorporacionesRepository _corporacionesRepository;
        private readonly IlistasRepository _listasRepository;
        private readonly ItipoReparacionesRepository _tipoReparacionesRepository;
        private readonly IreparacionesRepository _reparacionesRepository;
        private readonly ItipoActivosRepository _tipoActivosRepository;
        private readonly IactivoGeneralesRepository _activoGeneralesRepository;
        private readonly ItipoProveedoresRepository _tipoProveedoresRepository;
        private readonly IpilotosTiposRepository _pilotosTiposRepository;
        private readonly ItipoMecanicosRepository _tipoMecanicosRepository;
        private readonly IllantaTiposRepository _llantaTiposRepository;
        private readonly ItipoEquipoRemolqueRepository _tipoEquipoRemolqueRepository;
        private readonly ItipoVehiculosRepository _tipoVehiculosRepository;
        private readonly ItipoGeneradoresRepository _tipoGeneradoresRepository;
        private readonly IproveedoresRepository _proveedoresRepository;
        private readonly ItransportesRepository _transportesRepository;
        private readonly IrutasRepository _rutasRepository;
        private readonly IserviciosRepository _serviciosRepository;
        private readonly IclienteServiciosRepository _clienteServiciosRepository;
        private readonly IempleadosRepository _empleadosRepository;
        private readonly IasesoresRepository _asesoresRepository;
        private readonly IpilotosRepository _pilotosRepository;
        private readonly IpilotosDocumentosRepository _pilotosDocumentosRepository;
        private readonly IactivoOperacionesRepository _activoOperacionesRepository;
        private readonly IvehiculosRepository _vehiculosRepository;
        private readonly ImedidasRepository _medidasRepository;
        private readonly IinvCategoriaRepository _invCategoriaRepository;
        private readonly IinvSubCategoriaRepository _invSubCategoriaRepository;
        private readonly IproductosRepository _productosRepository;
        private readonly IinvProductoBodegaRepository _invProductoBodegaRepository;
        private readonly IequipoRemolqueRepository _equipoRemolqueRepository;
        private readonly IgeneradoresRepository _generadoresRepository;
        private readonly IinvUbicacionBodegaRepository _invUbicacionBodegaRepository;
        private readonly IproductosBusquedaRepository _productosBusquedaRepository;
        private readonly IestadosRepository _estadosRepository;
        private readonly IactivoEstadosRepository _activoEstadosRepository;
        private readonly IactivoUbicacionesRepository _activoUbicacionesRepository;
        private readonly ImecanicosRepository _mecanicosRepository;
        private readonly IllantasRepository _llantasRepository;
        private readonly IllantaActualRepository _llantaActualRepository;
        private readonly ItarifarioRepository _tarifarioRepository;
        private readonly IclienteTarifasRepository _clienteTarifasRepository;
        private readonly IcondicionActivosRepository _condicionActivosRepository;
        private readonly IactivoMovimientosRepository _activoMovimientosRepository;
        private readonly IactivoMovimientosActualRepository _activoMovimientosActualRepository;
        private readonly IcondicionCabezalRepository _condicionCabezalRepository;
        private readonly IcondicionEquipoRepository _condicionEquipoRepository;
        private readonly IcondicionCisternaRepository _condicionCisternaRepository;
        private readonly IcondicionFurgonRepository _condicionFurgonRepository;
        private readonly IcondicionGenSetRepository _condicionGenSetRepository;
        private readonly IcondicionTecnicaGenSetRepository _condicionTecnicaGenSetRepository;
        private readonly IconeccionesSistemasRepository _coneccionesSistemasRepository;
        private readonly IeventosControlEquipoRepository _eventosControlEquipoRepository;
        private readonly IcontrolVisitasRepository _controlVisitasRepository;
        private readonly IcontrolContratistasRepository _controlContratistasRepository;
        private readonly IcontrolEquipoAjenoRepository _icontrolEquipoAjenoRepository;
        private readonly IempleadosIngresosRepository _empleadosIngresosRepository;
        private readonly IcondicionTallerVehiculoRepository _condicionTallerVehiculoRepository;
        private readonly IdetalleCondicionRepository _detalleCondicionRepository;
        private readonly ICondicionTallerRepository _condicionTallerRepository;
        private readonly IcodigoPostalRepository _codigoPostalRepository;
        private readonly IcondicionContenedorRepository _condicionContenedorRepository;


        public UnitOfWork(AguilaDBContext context)
        {
            _context = context;
        }

        public DbContext DbContext { get { return _context; } }

        //Se puede obtener la transaccion para compartirla en otros contextos, siempre y cuando SaveChangues(trues) se ejecutara en true
        public DbTransaction  transaccion { get { return _transaccion; } }

        public IAsigUsuariosEstacionesTrabajoRepository AsigUsuariosEstacionesTrabajoRepository =>
               _asigUsuariosEstacionesTrabajoRepository ?? new AsigUsuariosEstacionesTrabajoRepository(_context);
        public IAsigUsuariosModulosRepository AsigUsuariosModulosRepository =>
               _asigUsuariosModulosRepository ?? new AsigUsuariosModulosRepository(_context);
        public IAsigUsuariosRecursosAtributosRepository AsigUsuariosRecursosAtributosRepository =>
            _asigUsuariosRecursosAtributosRepository ?? new AsigUsuariosRecursosAtributosRepository(_context);
        public IEmpresaRepository EmpresaRepository =>
            _empresaRepository ?? new EmpresaRepository(_context);
        public IEstacionesTrabajoRepository EstacionesTrabajoRepository =>
            _estacionesTrabajoRepository ?? new EstacionesTrabajoRepository(_context);
        public IModulosMnuRepository ModulosMnuRepository =>
            _modulosMnuRepository ?? new ModulosMnuRepository(_context);
        public IModulosRepository ModulosRepository =>
            _modulosRepository ?? new ModulosRepository(_context);
        public IRecursosAtributosRepository RecursosAtributosRepository =>
            _recursosAtributosRepository ?? new RecursosAtributosRepository(_context);
        public IRecursosRepository RecursosRepository =>
            _recursosRepository ?? new RecursosRepository(_context);
        public ISucursalRepository SucursalRepository =>
            _sucursalRepository ?? new SucursalRepository(_context);
        public IUsuariosRepository UsuariosRepository =>
            _usuariosRepository ?? new UsuariosRepository(_context);
        public IRolesRepository RolesRepository =>
            _rolesRepository ?? new RolesRepository(_context);
        public IUsuariosRolesRepository UsuariosRolesRepository =>
            _usuariosRolesRepository ?? new UsuariosRolesRepository(_context);
        public IUsuariosRecursosRepository UsuariosRecursosRepository =>
            _usuariosRecursosRepository ?? new UsuariosRecursosRepository(_context);
        public IImagenesRecursosConfiguracionRepository ImagenesRecursosConfiguracionRepository =>
            _imagenesRecursosConfiguracionRepository ?? new ImagenesRecursosConfiguracionRepository(_context);
        public IImagenesRecursosRepository ImagenesRecursosRepository =>
            _imagenesRecursosRepository ?? new ImagenesRecursosRepository(_context);
        public IImagenesRepository ImagenesRepository =>
            _imagenesRepository ?? new ImagenesRepository(_context);
        public IpaisesRepository paisesRepository =>
            _paisesRepository ?? new paisesRepository(_context);
        public ItiposListaRepository tiposListaRepository =>
            _tiposListaRepository ?? new tiposListaRepository(_context);
        public IdepartamentosRepository departamentosRepository =>
            _departamentosRepository ?? new departamentosRepository(_context);
        public ImunicipiosRepository municipiosRepository =>
            _municipiosRepository ?? new municipiosRepository(_context);
        public IubicacionesRepository ubicacionesRepository =>
            _ubicacionesRepository ?? new ubicacionesRepository(_context);
        public IdireccionesRepository direccionesRepository =>
            _direccionesRepository ?? new direccionesRepository(_context);
        public IentidadComercialRepository entidadComercialRepository =>
            _entidadComercialRepository ?? new entidadComercialRepository(_context);
        public IentidadesComercialesDireccionesRepository entidadesComercialesDireccionesRepository =>
            _entidadesComercialesDireccionesRepository ?? new entidadesComercialesDireccionesRepository(_context);
        public ItipoClientesRepository tipoClientesRepository =>
            _tipoClientesRepository ?? new tipoClientesRepository(_context);
        public IclientesRepository clientesRepository =>
            _clientesRepository ?? new clientesRepository(_context);
        public IcorporacionesRepository corporacionesRepository =>
            _corporacionesRepository ?? new corporacionesRepository(_context);
        public IpilotosTiposRepository pilotosTiposRepository =>
            _pilotosTiposRepository ?? new pilotosTiposRepository(_context);
        public ItipoMecanicosRepository tipoMecanicosRepository =>
            _tipoMecanicosRepository ?? new tipoMecanicosRepository(_context);
        public IllantaTiposRepository llantaTiposRepository =>
            _llantaTiposRepository ?? new llantaTiposRepository(_context);
        public ItipoEquipoRemolqueRepository tipoEquipoRemolqueRepository =>
            _tipoEquipoRemolqueRepository ?? new tipoEquipoRemolqueRepository(_context);
        public ItipoVehiculosRepository tipoVehiculosRepository =>
            _tipoVehiculosRepository ?? new tipoVehiculosRepository(_context);
        public ItipoGeneradoresRepository tipoGeneradoresRepository =>
            _tipoGeneradoresRepository ?? new tipoGeneradoresRepository(_context);
        public IproveedoresRepository proveedoresRepository =>
            _proveedoresRepository ?? new proveedoresRepository(_context);
        public ItransportesRepository transportesRepository =>
            _transportesRepository ?? new transportesRepository(_context);
        public IrutasRepository rutasRepository =>
            _rutasRepository ?? new rutasRepository(_context);
        public IserviciosRepository serviciosRepository =>
            _serviciosRepository ?? new serviciosRepository(_context);
        public IclienteServiciosRepository clienteServiciosRepository =>
            _clienteServiciosRepository ?? new clienteServiciosRepository(_context);
        public IempleadosRepository empleadosRepository =>
            _empleadosRepository ?? new empleadosRepository(_context);
        public IasesoresRepository asesoresRepository =>
            _asesoresRepository ?? new asesoresRepository(_context);
        public IpilotosRepository pilotosRepository =>
            _pilotosRepository ?? new pilotosRepository(_context);
        public IpilotosDocumentosRepository pilotosDocumentosRepository =>
            _pilotosDocumentosRepository ?? new pilotosDocumentosRepository(_context);
        public IactivoOperacionesRepository activoOperacionesRepository =>
            _activoOperacionesRepository ?? new activoOperacionesRepository(_context);
        public ImedidasRepository medidasRepository =>
            _medidasRepository ?? new medidasRepository(_context);
        public IinvCategoriaRepository invCategoriaRepository =>
            _invCategoriaRepository ?? new invCategoriaRepository(_context);
        public IinvSubCategoriaRepository invSubCategoriaRepository =>
            _invSubCategoriaRepository ?? new invSubCategoriaRepository(_context);
        public IproductosRepository productosRepository =>
            _productosRepository ?? new productosRepository(_context);
        public IinvProductoBodegaRepository invProductoBodegaRepository =>
            _invProductoBodegaRepository ?? new invProductoBodegaRepository(_context);
        public IequipoRemolqueRepository equipoRemolqueRepository =>
            _equipoRemolqueRepository ?? new equipoRemolqueRepository(_context);
        public IgeneradoresRepository generadoresRepository =>
            _generadoresRepository ?? new generadoresRepository(_context);
        public IinvUbicacionBodegaRepository invUbicacionBodegaRepository =>
            _invUbicacionBodegaRepository ?? new invUbicacionBodegaRepository(_context);
        public IproductosBusquedaRepository productosBusquedaRepository =>
            _productosBusquedaRepository ?? new productosBusquedaRepository(_context);
        public IestadosRepository estadosRepository =>
            _estadosRepository ?? new estadosRepository(_context);
        public IactivoEstadosRepository activoEstadosRepository =>
            _activoEstadosRepository ?? new activoEstadosRepository(_context);
        public IactivoUbicacionesRepository activoUbicacionesRepository =>
            _activoUbicacionesRepository ?? new activoUbicacionesRepository(_context);
        public ImecanicosRepository mecanicosRepository =>
            _mecanicosRepository ?? new mecanicosRepository(_context);
        public IllantasRepository llantasRepository =>
            _llantasRepository ?? new llantasRepository(_context);
        public IllantaActualRepository llantaActualRepository =>
            _llantaActualRepository ?? new llantaActualRepository(_context);
        public ItarifarioRepository tarifarioRepository =>
            _tarifarioRepository ?? new tarifarioRepository(_context);
        public IclienteTarifasRepository clienteTarifasRepository =>
            _clienteTarifasRepository ?? new clienteTarifasRepository(_context);

        public IlistasRepository listasRepository =>
            _listasRepository ?? new listasRepository(_context);
        public ItipoReparacionesRepository tipoReparacionesRepository =>
            _tipoReparacionesRepository ?? new tipoReparacionesRepository(_context);
        public IreparacionesRepository reparacionesRepository =>
            _reparacionesRepository ?? new reparacionesRepository(_context);
        public ItipoActivosRepository tipoActivosRepository =>
            _tipoActivosRepository ?? new tipoActivosRepository(_context);
        public IactivoGeneralesRepository activoGeneralesRepository =>
            _activoGeneralesRepository ?? new activoGeneralesRepository(_context);
        public ItipoProveedoresRepository tipoProveedoresRepository =>
            _tipoProveedoresRepository ?? new tipoProveedoresRepository(_context);
        public IvehiculosRepository vehiculosRepository =>
            _vehiculosRepository ?? new vehiculosRepository(_context);

        public IcondicionActivosRepository condicionActivosRepository =>
            _condicionActivosRepository ?? new condicionActivosRepository(_context);
        public IactivoMovimientosRepository activoMovimientosRepository =>
            _activoMovimientosRepository ?? new activoMovimientosRepository(_context);
        public IactivoMovimientosActualRepository activoMovimientosActualRepository =>
            _activoMovimientosActualRepository ?? new activoMovimientosActualRepository(_context);
        public IcondicionCabezalRepository condicionCabezalRepository =>
            _condicionCabezalRepository ?? new condicionCabezalRepository(_context);
        public IcondicionEquipoRepository condicionEquipoRepository =>
            _condicionEquipoRepository ?? new condicionEquipoRepository(_context);
        public IcondicionCisternaRepository condicionCisternaRepository =>
            _condicionCisternaRepository ?? new condicionCisternaRepository(_context);
        public IcondicionFurgonRepository condicionFurgonRepository =>
            _condicionFurgonRepository ?? new condicionFurgonRepository(_context);
        public IcondicionGenSetRepository condicionGenSetRepository =>
            _condicionGenSetRepository ?? new condicionGenSetRepository(_context);
        public IcondicionTecnicaGenSetRepository condicionTecnicaGenSetRepository =>
            _condicionTecnicaGenSetRepository ?? new condicionTecnicaGenSetRepository(_context);
        public IconeccionesSistemasRepository coneccionesSistemasRepository =>
            _coneccionesSistemasRepository ?? new coneccionesSistemasRepository(_context);
        public IeventosControlEquipoRepository eventosControlEquipoRepository =>
            _eventosControlEquipoRepository ?? new eventosControlEquipoRepository(_context);
        public IcontrolVisitasRepository controlVisitasRepository =>
            _controlVisitasRepository ?? new controlVisitasRepository(_context);
        public IcontrolContratistasRepository controlContratistasRepository =>
            _controlContratistasRepository ?? new controlContratistasRepository(_context);
        public IcontrolEquipoAjenoRepository controlEquipoAjenoRepository =>
            _icontrolEquipoAjenoRepository ?? new controlEquipoAjenoRepository(_context);
        public IempleadosIngresosRepository empleadosIngresosRepository =>
            _empleadosIngresosRepository ?? new empleadosIngresosRepository(_context);
        public IcondicionTallerVehiculoRepository condicionTallerVehiculoRepository =>
            _condicionTallerVehiculoRepository ?? new condicionTallerVehiculoRepository(_context);
        public IdetalleCondicionRepository detalleCondicionRepository =>
            _detalleCondicionRepository ?? new detalleCondicionRepository(_context);
        public ICondicionTallerRepository condicionTallerRepository =>
        _condicionTallerRepository ?? new CondicionTallerRepository(_context);
        public IcondicionContenedorRepository condicionContenedorRepository =>
            _condicionContenedorRepository ?? new condicionContenedorRepository(_context);

        public IcodigoPostalRepository codigoPostalRepository =>
            _codigoPostalRepository ?? new codigoPostalRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        //Inicia una transaccion
        public void BeginTransaction()
        {
            //se agrea validacion de currentTransaction, cuando hay una existente se desecha con Dispose() para que que pueda tomar una nueva
            //esto debido a que cuando se realizan varias transacciones en un mismo contexto, no de error al iniciar una nueva transaccion
            var tran = _context.Database.CurrentTransaction;
            if(tran !=null)
            tran.Dispose();
           
                _transaccion = _context.Database.BeginTransaction().GetDbTransaction();          
            
        }

        public void CommitTransaction()
        {
            if(_transaccion ==null)
                throw new AguilaException("DbError : no hay ninguna transaccion iniciada" );

            try
            {
                _transaccion.Commit();
                //agreganfo dispose
                _transaccion.Dispose();

                _transaccion = null;
                
            }
            catch (Exception msjError)
            {
                _transaccion.Rollback();
                _transaccion = null;
                throw new AguilaException("DbError : " + msjError.Message);
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if(_transaccion != null ) 
                    _transaccion.Rollback(); 

                _transaccion = null;

            }
            catch (Exception msjError)
            {
                _transaccion = null;
                throw new AguilaException("DbError : " + msjError.Message);
            }

        }

        //Si ocurre un error se revierte toda la transaccion en el momento automaticamente y no se guarda nada
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
               
            }
            catch (Exception msjError)
            {
                if(_transaccion != null)
                {
                    _transaccion.Rollback();
                    _transaccion = null;
                }
                throw new AguilaException("DbError : " + msjError.Message );
            }            
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception msjError)
            {
                if (_transaccion != null)
                {
                    _transaccion.Rollback();
                    _transaccion = null;
                }
                throw new AguilaException("DbError : " + msjError.Message +System.Environment.NewLine +" -> Detalle:" +msjError.InnerException.Message);
            }
        }

        
        public void CancelChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
       

}
}
