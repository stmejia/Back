using Aguila.Core.Interfaces.Services;
using Aguila.Core.Entities;
using Aguila.Core.CustomEntities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Interfaces.Trafico.Data;
using Aguila.Interfaces.Trafico.Services;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Aguila.Core.DTOs.DTOsRespuestas;

namespace Aguila.Core.Services
{
    public class activoMovimientosService : IactivoMovimientosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IestadosService _estadosService;
        private readonly TraficoDBContext _traficoDBContext;
        private readonly IAguilaMap _aguilaMap;
        private readonly IeventosControlEquipoService _eventosControlEquipoService;
        private readonly IUsuariosService _usuariosService;

        public activoMovimientosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                        IactivoMovimientosActualService activoMovimientosActualService,
                                        IestadosService estadosService,
                                        TraficoDBContext traficoDBContext,
                                        IAguilaMap aguilaMap,
                                        IeventosControlEquipoService eventosControlEquipoService,
                                        IUsuariosService usuariosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoMovimientosActualService = activoMovimientosActualService;
            _estadosService = estadosService;
            _traficoDBContext = traficoDBContext;
            _aguilaMap = aguilaMap;
            _eventosControlEquipoService = eventosControlEquipoService;
            _usuariosService = usuariosService;
        }

        public PagedList<activoMovimientos> GetActivoMovimientos(activoMovimientosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activoMovimientos = _unitOfWork.activoMovimientosRepository.GetAllIncludes();

            if (filter.idActivo != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.ubicacionId != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.ubicacionId==filter.ubicacionId);
            }

            if (filter.idEstado != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idEstado == filter.idEstado);
            }

            if (filter.idServicio != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idServicio == filter.idServicio);
            }

            if (filter.idEstacionTrabajo != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.idEmpleado != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.idUsuario != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.lugar != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.lugar.ToLower().Contains(filter.lugar.ToLower()));
            }

            if (filter.fecha != null)
            {
                activoMovimientos = activoMovimientos.Where(e => e.fecha == filter.fecha);
            }

            activoMovimientos = activoMovimientos.OrderByDescending(e => e.fechaCreacion);
            var pagedActivoMovimientos = PagedList<activoMovimientos>.create(activoMovimientos, filter.PageNumber, filter.PageSize);
            return pagedActivoMovimientos;
        }

        public async Task<activoMovimientos> GetActivoMovimiento(int id)
        {
            return await _unitOfWork.activoMovimientosRepository.GetByID(id);
        }

        public async Task InsertActivoMovimiento(activoMovimientos activoMovimiento, ControlActivosEventos evento)
        {
            var xMsjError = await validarMovimiento(activoMovimiento.idActivo, evento);

            if (!string.IsNullOrEmpty(xMsjError))
                throw new AguilaException(xMsjError, 404);

            //Insertamos la fecha de ingreso del registro
            activoMovimiento.id = 0;
            activoMovimiento.fechaCreacion = DateTime.Now;

            if (activoMovimiento.fecha > DateTime.Now)
            {
                if ((Convert.ToDateTime(activoMovimiento.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days >= 1)
                {
                    throw new AguilaException("No es posible registrar el movimiento con fecha mayor al dia de hoy", 404);
                }
                if ((Convert.ToDateTime(activoMovimiento.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days == 0)
                {
                    activoMovimiento.fecha = DateTime.Now;
                }
            }

            await _unitOfWork.activoMovimientosRepository.Add(activoMovimiento);
            await _unitOfWork.SaveChangeAsync();

            //Actualizar activoMomientoActual  
            await _activoMovimientosActualService.actualizarMovimientosActual(activoMovimiento, evento);         
        }

        //Utilizar este metodo solo cuando no tenemos el idEstado
        //Este metodo no guarda el registro, ejecutar despues InsertActivoMovimiento
        public async Task<activoMovimientos> InsertMovimientoPorEvento(activoMovimientosDto evento)
        {
            if (evento.fecha == default)
            {
                evento.fecha = DateTime.Now;
            }

            var movimiento = _aguilaMap.Map<activoMovimientos>(evento);
                       
            if (evento.lugar == null)
            {
                var nombreEstacion = (await _unitOfWork.EstacionesTrabajoRepository.GetByID(evento.idEstacionTrabajo)).Nombre;
                movimiento.lugar = nombreEstacion;
            }

            if (evento.idEstado == null || evento.idEstado == 0)
            {
                var xIdEstado = _estadosService.GetEstadoByEvento(evento.idEmpresa, "ACTIVOESTADOS", evento.evento.ToString()).id;
                movimiento.idEstado = xIdEstado;

                //captura el estado actual del activo antes de asignarle el nuevo estado
                var movActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(movimiento.idActivo);
                evento.idEstado = movActual.idEstado;
            }

            //Inicio de transaccion siempre desde el nivel mas alto, donde se inicia ahi se debe de terminar la transaccion
            _unitOfWork.BeginTransaction();

            if (evento.idServicio != null && (evento.evento == ControlActivosEventos.Reservado || evento.evento == ControlActivosEventos.PreReserva))
            {
                var respuesta = await Reserva((int)evento.idServicio, evento.idActivo, false);

                if (respuesta == false)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new AguilaException("No fue posible reservar este equipo en el sistema ALTAS", 404);
                }
            }

            if (evento.evento == ControlActivosEventos.QuitarReserva)
            {
                var respuesta =  await Reserva(null, evento.idActivo, true);

                if (respuesta == false)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new AguilaException("No fue posible reservar este equipo en el sistema ALTAS", 404);
                }

            }

            //Controlamos la transaccion si existe algun error devolvemos todos los cambios
            try{
                
                await InsertActivoMovimiento(movimiento, evento.evento);
                _unitOfWork.CommitTransaction();  

            }
            catch(Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new AguilaException(ex.Message, 422);

            }
            //*SE VERIFICA SI SE DEBE CREAR UN EVENTO DE ALERTA EN EL BOLSON
            await verificarEvento(evento);
            return movimiento;
        }

        public async Task<bool> UpdateActivoMovimiento(activoMovimientos activoMovimiento)
        {
            var currentActivoMovimiento = await _unitOfWork.activoMovimientosRepository.GetByID(activoMovimiento.id);
            if (currentActivoMovimiento == null)
            {
                throw new AguilaException("Movimiento no existente...");
            }

            currentActivoMovimiento.idActivo = activoMovimiento.idActivo;
            currentActivoMovimiento.ubicacionId = activoMovimiento.ubicacionId;
            currentActivoMovimiento.idEstado = activoMovimiento.idEstado;
            currentActivoMovimiento.idServicio = activoMovimiento.idServicio;
            currentActivoMovimiento.idEstacionTrabajo = activoMovimiento.idEstacionTrabajo;
            currentActivoMovimiento.idEmpleado = activoMovimiento.idEmpleado;
            currentActivoMovimiento.idUsuario = activoMovimiento.idUsuario;
            currentActivoMovimiento.lugar = activoMovimiento.lugar;

            _unitOfWork.activoMovimientosRepository.Update(currentActivoMovimiento);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteActivoMovimiento(int id)
        {
            var currentActivoMovimiento = await _unitOfWork.activoMovimientosRepository.GetByID(id);
            if (currentActivoMovimiento == null)
            {
                throw new AguilaException("Movimiento no existente");
            }

            await _unitOfWork.activoMovimientosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<bool> Reserva(int? solicitudMovimientoIntegracionId, int idEquipo, bool quitarReserva)
        {
            var xEquipo = await _unitOfWork.activoOperacionesRepository.GetByID(idEquipo);

            //Utilizamos coneccion de sistema para averiguar el id de la empresa a la que se tiene que conectar para buscar el activo

            var xconeccionSistemas = _unitOfWork.coneccionesSistemasRepository.GetAll()
                .Where(c => c.idEmpresa == xEquipo.idEmpresa && c.modulo.ToUpper().Trim() == eModulos.AguilaControlActivos.ToString().ToUpper().Trim()
                       && c.moduloExterno.Trim().ToUpper() == eModulos.AltasTrafico.ToString().ToUpper().Trim()
                ).FirstOrDefault();

            if(xconeccionSistemas == null)
                return false;

            var xSolicitud = new SolicitudesMovimientoService(_traficoDBContext);
            var respuesta = xSolicitud.reservarEquipo(solicitudMovimientoIntegracionId, xEquipo.codigo, xconeccionSistemas.idEmpresaExterno, quitarReserva);

            return respuesta;
        }

        public async Task<string> validarMovimiento(int idActivo, ControlActivosEventos evento)
        {
            var currentActivoMovimientoActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(idActivo);

            //No hay estado actual para comparar
            if (currentActivoMovimientoActual == null)
            {
                return "";
            }

            var xEstadoActual = await _unitOfWork.estadosRepository.GetByID(currentActivoMovimientoActual.idEstado);

            // Validar solo de que eventos puede pasar a este
            string mensajeError = "";

            switch (evento)
            {
                case ControlActivosEventos.PreReserva:
                    if (!xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().Trim().ToUpper()))
                        mensajeError = "Debe de estar en Ruta para Pre Reservar.!";
                    break;

                case ControlActivosEventos.Bodega:
                    if (!xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().Trim().ToUpper()))
                        mensajeError = "Debe de estar en Ruta.!";
                    break;

                case ControlActivosEventos.RentaInterna:
                    if (!xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().Trim().ToUpper()))
                        mensajeError = "Debe de estar en Ruta.!";
                    break;

                case ControlActivosEventos.RentaExterna:
                    if (!xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().Trim().ToUpper()))
                        mensajeError = "Debe de estar en Ruta.!";
                    break;

                case ControlActivosEventos.Reservado:
                    if (!xEstadoActual.disponible)
                        mensajeError = "Debe de estar disponible para Reservar.!";
                    break;
            }

            //Condiciones deben de estar en un predio
            if (evento == ControlActivosEventos.CondicionIngreso || evento == ControlActivosEventos.CondicionSalida || evento == ControlActivosEventos.CondicionGeneradorTecnica)
            {
                if (xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().Trim().ToUpper())
                    || xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.Bodega.ToString().Trim().ToUpper())
                    || xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.RentaExterna.ToString().Trim().ToUpper())
                    || xEstadoActual.evento.ToUpper().Trim().Contains(ControlActivosEventos.RentaInterna.ToString().Trim().ToUpper())
                    )
                {
                    return mensajeError = "No se puede realizar condicion a un equipo fuera de un predio.!";

                }
            }
            return mensajeError;

        }

        public IEnumerable<movimientoEquipoRemolqueDto> reporteMovimientosEquipos(ReporteMovimientosEquiposRemolqueQueryFilter filter)
        {            
            //Filtramos en especifico para equipos
            var equiposQuery = _unitOfWork.equipoRemolqueRepository.GetAllIncludes().Where(e => e.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();          

            if (filter.idTipoEquipoRemolque != null)
            {
                equiposQuery = equiposQuery.Where(x => x.idTipoEquipoRemolque == filter.idTipoEquipoRemolque);
            }

            if (filter.noEjes != null)
            {
                equiposQuery = equiposQuery.Where(x => x.noEjes == filter.noEjes);
            }

            if (filter.tarjetaCirculacion != null)
            {
                equiposQuery = equiposQuery.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                equiposQuery = equiposQuery.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.tandemCorredizo != null)
            {
                equiposQuery = equiposQuery.Where(x => x.tandemCorredizo.ToLower().Equals(filter.tandemCorredizo.ToLower()));
            }

            if (filter.chasisExtensible != null)
            {
                equiposQuery = equiposQuery.Where(x => x.chasisExtensible.ToLower().Equals(filter.chasisExtensible.ToLower()));
            }

            if (filter.chasisExtensible != null)
            {
                equiposQuery = equiposQuery.Where(e => e.chasisExtensible.ToLower().Contains(filter.chasisExtensible.ToLower()));
            }

            if (filter.tipoCuello != null)
            {
                equiposQuery = equiposQuery.Where(e => e.tipoCuello.ToLower().Contains(filter.tipoCuello.ToLower()));
            }

            if (filter.acopleGenset != null)
            {
                equiposQuery = equiposQuery.Where(e => e.acopleGenset.ToLower().Contains(filter.acopleGenset.ToLower()));
            }

            if (filter.acopleDolly != null)
            {
                equiposQuery = equiposQuery.Where(e => e.acopleDolly.ToLower().Contains(filter.acopleDolly.ToLower()));
            }

            if (filter.capacidadCargaLB != null)
            {
                equiposQuery = equiposQuery.Where(e => e.capacidadCargaLB.ToLower().Contains(filter.capacidadCargaLB.ToLower()));
            }

            if (filter.medidaLB != null)
            {
                equiposQuery = equiposQuery.Where(e => e.medidaLB.ToLower().Contains(filter.medidaLB.ToLower()));
            }

            if (filter.medidaPlataforma != null)
            {
                equiposQuery = equiposQuery.Where(e => e.medidaPlataforma.ToLower().Contains(filter.medidaPlataforma.ToLower()));
            }

            if (filter.pechera != null)
            {
                equiposQuery = equiposQuery.Where(e => e.pechera.ToLower().Contains(filter.pechera.ToLower()));
            }

            if (filter.alturaContenedor != null)
            {
                equiposQuery = equiposQuery.Where(e => e.alturaContenedor.ToLower().Contains(filter.alturaContenedor.ToLower()));
            }

            if (filter.tipoContenedor != null)
            {
                equiposQuery = equiposQuery.Where(e => e.tipoContenedor.ToLower().Contains(filter.tipoContenedor.ToLower()));
            }

            if (filter.marcaUR != null)
            {
                equiposQuery = equiposQuery.Where(e => e.marcaUR.ToLower().Contains(filter.marcaUR.ToLower()));
            }

            if (filter.largoFurgon != null)
            {
                equiposQuery = equiposQuery.Where(e => e.largoFurgon.ToLower().Contains(filter.largoFurgon.ToLower()));
            }

            if (filter.suspension != null)
            {
                equiposQuery = equiposQuery.Where(e => e.suspension.ToLower().Contains(filter.suspension.ToLower()));
            }

            if (filter.rieles != null)
            {
                equiposQuery = equiposQuery.Where(e => e.rieles.ToLower().Contains(filter.rieles.ToLower()));
            }

            var equipos = equiposQuery.ToList();

            var idsActivos = equipos.Select(e => e.idActivo).Distinct();

            if (idsActivos.Count() == 0) {
                var movs = new List<movimientoEquipoRemolqueDto>();
                return movs.AsEnumerable();
            }
                

            var xRepMovFilter = _aguilaMap.Map<reporteActivoMovimientosQueryFilter>(filter);
            xRepMovFilter.idsActivos = idsActivos;
            xRepMovFilter.categoria = "E";

            var movimientos = reporteMovimientos(xRepMovFilter).ToList();

            var movimientosEquipoRemolqueDto = _aguilaMap.Map<IEnumerable<movimientoEquipoRemolqueDto>>(movimientos);

            foreach(movimientoEquipoRemolqueDto xMov in movimientosEquipoRemolqueDto)
            {
                xMov.activoOperacion.equipoRemolque = _aguilaMap.Map<equipoRemolqueDto2>(equipos.Where(e => e.idActivo == xMov.idActivo).FirstOrDefault());
            }

            return movimientosEquipoRemolqueDto;
        }

        public IEnumerable<movimientoVehiculoDto> reporteMovimientosVehiculos(ReporteMovimientosVehiculosQueryFilter filter)
        {
            //Filtramos en especifico para equipos
            var vehiculosQuery = _unitOfWork.vehiculosRepository.GetAllIncludes().Where(e => e.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();

            if (filter.idTipoVehiculo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.idTipoVehiculo  == filter.idTipoVehiculo );
            }                      

            if (filter.tarjetaCirculacion != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }              

            var vehiculos = vehiculosQuery.ToList();

            var idsActivos = vehiculos.Select(e => e.idActivo).Distinct();

            if (idsActivos.Count() == 0)
            {
                var movs = new List<movimientoVehiculoDto>();
                return movs.AsEnumerable();
            }
                

            var xRepMovFilter = _aguilaMap.Map<reporteActivoMovimientosQueryFilter>(filter);
            xRepMovFilter.idsActivos = idsActivos;
            xRepMovFilter.categoria = "V";

            var movimientos = reporteMovimientos(xRepMovFilter).ToList();

            var movimientosVehiculosDto = _aguilaMap.Map<IEnumerable<movimientoVehiculoDto>>(movimientos);

            foreach (movimientoVehiculoDto xMov in movimientosVehiculosDto)
            {
                xMov.activoOperacion.vehiculo  = _aguilaMap.Map<vehiculosDto2>(vehiculos.Where(e => e.idActivo == xMov.idActivo).FirstOrDefault());
            }

            return movimientosVehiculosDto;
        }

        public IEnumerable<movimientoGeneradoresDto> reporteMovimientosGeneradores(reporteMovimientosGeneradoresQueryFilter filter)
        {
            //Filtramos en especifico para generadores
            var generadoresQuery = _unitOfWork.generadoresRepository.GetAllIncludes().Where(e => e.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();

            if (filter.idTipoGenerador != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.idTipoGenerador == filter.idTipoGenerador);
            }

            if (filter.capacidadGalones != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.capacidadGalones == filter.capacidadGalones);
            }

            if (filter.numeroCilindros != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.numeroCilindros == filter.numeroCilindros);
            }

            if (filter.marcaGenerador != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.marcaGenerador.ToLower().Contains(filter.marcaGenerador.ToLower()));
            }

            if (filter.tipoInstalacion != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.tipoInstalacion.ToLower().Contains(filter.tipoInstalacion.ToLower()));
            }

            if (filter.tipoEnfriamiento != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.tipoEnfriamiento.ToLower().Contains(filter.tipoEnfriamiento.ToLower()));
            }

            var generadores = generadoresQuery.ToList();

            var idsActivos = generadores.Select(e => e.idActivo).Distinct();

            if (idsActivos.Count() == 0) {
                var movs = new List<movimientoGeneradoresDto>();
                return movs.AsEnumerable();
            }
            

            var xRepMovFilter = _aguilaMap.Map<reporteActivoMovimientosQueryFilter>(filter);
            xRepMovFilter.idsActivos = idsActivos;
            xRepMovFilter.categoria = "G";

            var movimientos = reporteMovimientos(xRepMovFilter).ToList();

            var movimientosGeneradoresDto = _aguilaMap.Map<IEnumerable<movimientoGeneradoresDto>>(movimientos);

            foreach (movimientoGeneradoresDto xMov in movimientosGeneradoresDto)
            {
                xMov.activoOperacion.generadores = _aguilaMap.Map<generadoresDto2>(generadores.Where(e => e.idActivo == xMov.idActivo).FirstOrDefault());
            }

            return movimientosGeneradoresDto;
        }

        public IEnumerable<activoMovimientos> reporteMovimientos(reporteActivoMovimientosQueryFilter filter)
        {
            //Reporte de movimientos Generalizado
            if (filter.idUsuario == 0)
                throw new AguilaException("Se necesita usuario", 404);                   

            if (filter.fechaInicial  == default || filter.fechaFinal == default)
                throw new AguilaException("Indique rango o intervalo de fecha para la generacion del reporte", 404);

            if ((filter.fechaFinal - filter.fechaInicial).TotalDays >= 92 )
                throw new AguilaException("Solo puede generar este reporte con un rango maximo de fechas de 3 meses", 404);

            if (filter.idEmpresa == 0)
                throw new AguilaException("Favor indicar empresa ", 404);

            var movimientos = _unitOfWork.activoMovimientosRepository
                .ReporteMovimientosByUser(filter.idEmpresa, filter.idUsuario, filter.fechaInicial, filter.fechaFinal)
                .AsQueryable();

            movimientos = movimientos.Where(m => m.activoOperacion.categoria.ToUpper().Trim() == filter.categoria.Trim().ToLower());

            //Verificamos si filtraron en especifico equipos de remolque con sus atributos, deben de envia lista de activos requeridos
            if (filter.idsActivos.Count() > 0)
                movimientos = movimientos.Where(m => filter.idsActivos.Contains(m.activoOperacion.id));
            
            if (filter.tipoDocumento != null)
                movimientos = movimientos.Where(x => x.tipoDocumento.ToLower().Contains(filter.tipoDocumento.ToLower()));
            
            if (filter.documento != null)
                movimientos = movimientos.Where(x => x.documento == filter.documento);

            if (filter.codigo != null)
            {
                movimientos = movimientos.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            //Filtramos por los estados que elija el usuario , puede ser solamente 1
            if (filter.listaIdEstados != null)
            {
                var xEstados = filter.listaIdEstados.Split(",").Select(Int32.Parse).ToList();
                if(xEstados.Count > 0)
                    movimientos = movimientos.Where(x => xEstados.Contains((int)x.idEstado));
            }


            if (filter.idEstacionTrabajo != null)
            {
                movimientos = movimientos.Where(x => x.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.flota != null)
            {
                movimientos = movimientos.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    movimientos = movimientos.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {
                    movimientos = movimientos.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }


            return movimientos;
        }

        public async Task<bool> verificarEvento(activoMovimientosDto movimiento)
        {
            activoMovimientosActualQueryFilter filter = new activoMovimientosActualQueryFilter()
            {
                idActivo = movimiento.idActivo
            };

            eventosControlEquipoDto evento = new eventosControlEquipoDto();
            var movimientoActual = _activoMovimientosActualService.GetActivoMovimientosActual(filter);
            var estadoActual = movimientoActual.FirstOrDefault().estado;
            var estadoAnterior = new estados();
                
            //buscamos el estado anterior del activo por medio del evento enviado a verificar
            if (movimiento.idEstado != null)
            {
                estadoAnterior = await _unitOfWork.estadosRepository.GetByID(movimiento.idEstado);
            }
             
            var nombreEstadoAnteior = "---";
            if(estadoAnterior.nombre!=null) { nombreEstadoAnteior = estadoAnterior.nombre; }

            //evento de INGRESO
            if (movimiento.evento == ControlActivosEventos.Ingresado && !estadoActual.evento.Trim().ToUpper().Contains("EGRESADO"))
            {
                //se crea evento de equipo ingresado sin estar con estado en RUTA
                evento.idUsuarioCreacion = movimiento.idUsuario;
                evento.idEstacionTrabajo = movimiento.idEstacionTrabajo;
                evento.idActivo = movimiento.idActivo;
                evento.descripcionEvento = "Ingreso de Equipo " + movimientoActual.FirstOrDefault().activoOperacion.codigo;
                evento.bitacoraObservaciones = "Equipo ingreso a predio con estado "+ nombreEstadoAnteior+ ", se asigna estado " +estadoActual.nombre.ToUpper().Trim();
                evento.fechaCreacion = DateTime.Now;

                var nombreUsuario = "";
                var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                if (usuario != null) nombreUsuario = usuario.Nombre;

                await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
            }
            //evento de SALIDA
            else if (movimiento.evento == ControlActivosEventos.Egresado && !estadoActual.evento.Trim().ToUpper().Contains("CONDICIONSALIDA"))
            {
                //se crea evento de equipo egresado sin estar con estado LISTO PARA SALIR
                //se crea evento de equipo ingresado sin estar con estado en RUTA
                evento.idUsuarioCreacion = movimiento.idUsuario;
                evento.idEstacionTrabajo = movimiento.idEstacionTrabajo;
                evento.idActivo = movimiento.idActivo;
                evento.descripcionEvento = "Salida de Equipo " + movimientoActual.FirstOrDefault().activoOperacion.codigo;
                evento.bitacoraObservaciones = "Equipo sale de predio con estado " + nombreEstadoAnteior + ", se asigna estado " + estadoActual.nombre.ToUpper().Trim();
                evento.fechaCreacion = DateTime.Now;

                var nombreUsuario = "";
                var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                if (usuario != null) nombreUsuario = usuario.Nombre;

                await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
            }
            //evento de PONER DISPONIBLE
            else if(movimiento.evento==ControlActivosEventos.PonerDisponible )
            {
                evento.idUsuarioCreacion = movimiento.idUsuario;
                evento.idEstacionTrabajo = movimiento.idEstacionTrabajo;
                evento.idActivo = movimiento.idActivo;
                evento.descripcionEvento = "Se Pone Disponible el equipo " + movimientoActual.FirstOrDefault().activoOperacion.codigo;
                evento.bitacoraObservaciones = "Equipo se pone en Disponible con estado " + nombreEstadoAnteior + ", se asigna estado " + estadoActual.nombre.ToUpper().Trim();
                evento.fechaCreacion = DateTime.Now;

                var nombreUsuario = "";
                var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                if (usuario != null) nombreUsuario = usuario.Nombre;

                await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
            }

            return true;
        }

    }
}
