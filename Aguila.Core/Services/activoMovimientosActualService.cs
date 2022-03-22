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
using Aguila.Interfaces.Trafico.Data;
using Aguila.Interfaces.Trafico.QueryFilters;
using Aguila.Interfaces.Trafico.Services;
using Aguila.Interfaces.Trafico.Model;
using Aguila.Core.Enumeraciones;

namespace Aguila.Core.Services
{
    public class activoMovimientosActualService : IactivoMovimientosActualService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly TraficoDBContext _traficoDBContext; 

        public activoMovimientosActualService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, TraficoDBContext traficoDBContext )
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _traficoDBContext = traficoDBContext;
        }

        public PagedList<activoMovimientosActual> GetActivoMovimientosActual(activoMovimientosActualQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activoMovimientosActual = _unitOfWork.activoMovimientosActualRepository.GetAllIncludes();

            if (filter.idActivo != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idEstado != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idEstado == filter.idEstado);
            }

            if (filter.idEstacionTrabajo != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.idServicio != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idServicio == filter.idServicio);
            }

            if (filter.idEmpleado != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.idRuta != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idRuta == filter.idRuta);
            }

            if (filter.idUsuario != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.documento != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.documento == filter.documento);
            }

            if (filter.tipoDocumento != null)
            {
                activoMovimientosActual = activoMovimientosActual.Where(e => e.tipoDocumento.ToLower().Contains(filter.tipoDocumento.ToLower()));
            }

            var pagedActivoMovimientosActual = PagedList<activoMovimientosActual>.create(activoMovimientosActual, filter.PageNumber, filter.PageSize);
            return pagedActivoMovimientosActual;
        }

        public async Task<activoMovimientosActual> GetActivoMovimientoActual(int id)
        {
            return await _unitOfWork.activoMovimientosActualRepository.GetByID(id);
        }

        public activoMovimientosActual GetActivoMovimientoActualByCodigo(string codigo, int empresaId)
        {
            var xEquipo = _unitOfWork.activoMovimientosActualRepository.GetAllIncludes()
                .Where(e => e.activoOperacion.codigo.ToUpper().Trim().Contains(codigo.ToUpper().Trim()) && e.activoOperacion.idEmpresa == empresaId)
                //.Select(e => new activoMovimientosActual
                //{
                //    documento = e.documento,
                //    idActivo = e.idActivo,
                //    idEstacionTrabajo = e.idEstacionTrabajo,
                //    idPiloto = e.idPiloto,
                //    idUsuario = e.idUsuario,
                //    idEstado = e.idEstado,
                //    idRuta = e.idRuta,
                //    idServicio = e.idServicio,
                //    ubicacionId = e.ubicacionId,
                //    observaciones = e.observaciones,
                //    cargado = e.cargado,
                //    servicio = e.servicio,
                //    lugar = e.lugar,
                //    ruta = e.ruta,
                //    tipoDocumento = e.tipoDocumento,
                //    usuario = e.usuario,
                //    fecha = e.fecha,
                //    fechaCreacion = e.fechaCreacion,
                //    estado = e.estado,

                //    activoOperacion =
                //    new activoOperaciones
                //    {
                //        codigo = e.activoOperacion.codigo,
                //        marca = e.activoOperacion.marca,
                //        categoria = e.activoOperacion.categoria
                //    },
                //    empleado = new empleados
                //    {
                //        codigo = e.empleado.codigo,
                //        nombres = e.empleado.nombres
                //    }
                //})
                .FirstOrDefault();

            return xEquipo;
        }

        public async Task InsertActivoMovimientoActual(activoMovimientosActual activoMovimientoActual)
        {
            //Insertamos la fecha de ingreso del registro
            //activoMovimientoActual.id = 0;
            activoMovimientoActual.fechaCreacion = DateTime.Now;

            await _unitOfWork.activoMovimientosActualRepository.Add(activoMovimientoActual);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task actualizarMovimientosActual(activoMovimientos movimiento, ControlActivosEventos evento)
        {
            var currentActivoMovimientoActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(movimiento.idActivo);

            if (currentActivoMovimientoActual == null)
            {
                //TODO Quitar idEstado NULLABLE en activoMovimiento
                var xActualNew = new activoMovimientosActual
                {
                    idActivo = movimiento.idActivo,
                    ubicacionId = movimiento.ubicacionId,
                    idRuta = movimiento.idRuta,
                    idEstado = (int)movimiento.idEstado,
                    idServicio = movimiento.idServicio,
                    idEstacionTrabajo = movimiento.idEstacionTrabajo,
                    idEmpleado = movimiento.idEmpleado,
                    idUsuario = movimiento.idUsuario,
                    cargado = movimiento.cargado,
                    fecha = movimiento.fecha,
                    fechaCreacion = DateTime.Now,
                    tipoDocumento = movimiento.tipoDocumento,
                    observaciones = movimiento.observaciones,
                    documento = movimiento.documento,
                    lugar = movimiento.lugar
                };

                await _unitOfWork.activoMovimientosActualRepository.Add(xActualNew);
                await _unitOfWork.SaveChangeAsync();
                return;
            }

            if (currentActivoMovimientoActual.fecha < movimiento.fecha)
            {
                var xEventosCambioPiloto = new List<ControlActivosEventos>
                {
                    ControlActivosEventos.Asignado,
                    ControlActivosEventos.CondicionIngreso,
                    ControlActivosEventos.CondicionSalida,
                    ControlActivosEventos.CondicionGeneradorTecnica,
                    ControlActivosEventos.CondicionGeneradorEstructura
                };

                currentActivoMovimientoActual.lugar = movimiento.lugar;

                if (evento == ControlActivosEventos.Egresado || evento == ControlActivosEventos.CondicionSalida)
                {
                    currentActivoMovimientoActual.lugar = currentActivoMovimientoActual.lugar;
                }

                if (xEventosCambioPiloto.Contains(evento))
                {
                    currentActivoMovimientoActual.idEmpleado = movimiento.idEmpleado;
                }

                if (evento == ControlActivosEventos.Asignado)
                {
                    currentActivoMovimientoActual.idServicio = movimiento.idServicio;
                    currentActivoMovimientoActual.idRuta = movimiento.idRuta;
                }

                if (evento == ControlActivosEventos.Reservado)
                {
                    currentActivoMovimientoActual.idRuta = movimiento.idRuta;
                }

                currentActivoMovimientoActual.ubicacionId = movimiento.ubicacionId;
                currentActivoMovimientoActual.idEstado = (int)movimiento.idEstado;
                currentActivoMovimientoActual.idEstacionTrabajo = movimiento.idEstacionTrabajo;
                currentActivoMovimientoActual.idUsuario = movimiento.idUsuario;
                currentActivoMovimientoActual.cargado = movimiento.cargado;
                currentActivoMovimientoActual.fecha = movimiento.fecha;
                currentActivoMovimientoActual.fechaCreacion = DateTime.Now;
                currentActivoMovimientoActual.tipoDocumento = movimiento.tipoDocumento;
                currentActivoMovimientoActual.observaciones = movimiento.observaciones;
                currentActivoMovimientoActual.documento = movimiento.documento;

                _unitOfWork.activoMovimientosActualRepository.Update(currentActivoMovimientoActual);
                await _unitOfWork.SaveChangeAsync();
                return;
            }
        }

        public async Task<bool> UpdateActivoMovimientoActual(activoMovimientosActual activoMovimientoActual)
        {
            var currentActivoMovimientoActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(activoMovimientoActual.idActivo);
            if (currentActivoMovimientoActual == null)
            {
                throw new AguilaException("Movimiento Actual no existente...");
            }

            currentActivoMovimientoActual.idActivo = activoMovimientoActual.idActivo;
            currentActivoMovimientoActual.idEstado = activoMovimientoActual.idEstado;
            currentActivoMovimientoActual.idEstacionTrabajo = activoMovimientoActual.idEstacionTrabajo;
            currentActivoMovimientoActual.idServicio = activoMovimientoActual.idServicio;
            currentActivoMovimientoActual.idEmpleado = activoMovimientoActual.idEmpleado;
            currentActivoMovimientoActual.idRuta = activoMovimientoActual.idRuta;
            currentActivoMovimientoActual.idUsuario = activoMovimientoActual.idUsuario;
            currentActivoMovimientoActual.documento = activoMovimientoActual.documento;
            currentActivoMovimientoActual.tipoDocumento = activoMovimientoActual.tipoDocumento;

            _unitOfWork.activoMovimientosActualRepository.Update(currentActivoMovimientoActual);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteActivoMovimientoActual(int id)
        {
            var currentActivoMovimientoActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(id);
            if (currentActivoMovimientoActual == null)
            {
                throw new AguilaException("Movimiento Actual no existente");
            }

            await _unitOfWork.activoMovimientosActualRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        // Obtiene un listado de servicios del sistema Altas - Trafico pendientes de reservar equipo
        public PagedList<SolicitudesMovimientosIntegracion> SolicitudesReservasAltas(SolicitudesMovimientosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var xSolicitudService = new SolicitudesMovimientoService(_traficoDBContext);

            var xSoliMovs =  xSolicitudService.MovimientosPendietesReservaEquipoRemolque(filter).ToList();
                                 
            var pagedSoliMovs = PagedList<SolicitudesMovimientosIntegracion>.create(xSoliMovs , filter.PageNumber, filter.PageSize);
            return pagedSoliMovs;
        }

        //Retorna un equipo con su estado actual
        public equipoRemolque GetEquipoByCodigo(string codigo, int idEmpresa)
        {
            var xEquipo = _unitOfWork.equipoRemolqueRepository.GetAllEstadoActual().
                Where(e => e.activoOperacion.codigo.ToUpper().Trim() == codigo.ToUpper().Trim() & e.activoOperacion.idEmpresa == idEmpresa).FirstOrDefault();

            return xEquipo;
        }

        //Retorna un vehiculo con su estado actual
        public vehiculos GetVehiculoByCodigo(string codigo, int idEmpresa)
        {
            var xVehiculo = _unitOfWork.vehiculosRepository.GetAllEstadoActual().
                Where(e => e.activoOperacion.codigo.ToUpper().Trim() == codigo.ToUpper().Trim() & e.activoOperacion.idEmpresa == idEmpresa).FirstOrDefault();

            return xVehiculo;
        }

        //Retorna un Generador con su estado actual
        public generadores GetGeneradorByCodigo(string codigo, int idEmpresa)
        {
            var xGenerador = _unitOfWork.generadoresRepository.GetAllEstadoActual().
                Where(e => e.activoOperacion.codigo.ToUpper().Trim() == codigo.ToUpper().Trim() & e.activoOperacion.idEmpresa == idEmpresa).FirstOrDefault();

            return xGenerador;
        }

        //Devuelve un listado de vehiculos con su estado actual
        //Enviar idEmpresa para realizar la consulta
        public PagedList<vehiculos> GetVehiculoEstadoActual(vehiculosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            ///TODO validar que venga idEmpresa
            var vehiculo = _unitOfWork.vehiculosRepository.GetAllEstadoActual().Where(v => v.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();

            if (filter.codigo != null)
            {
                vehiculo = vehiculo.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                vehiculo = vehiculo.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                vehiculo = vehiculo.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    vehiculo = vehiculo.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    vehiculo = vehiculo.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    vehiculo = vehiculo.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    vehiculo = vehiculo.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idTipoVehiculo != null)
            {
                vehiculo = vehiculo.Where(x => x.idTipoVehiculo == filter.idTipoVehiculo);
            }

            if (filter.motor != null)
            {
                vehiculo = vehiculo.Where(x => x.motor.ToLower().Contains(filter.motor.ToLower()));
            }

            if (filter.ejes != null)
            {
                vehiculo = vehiculo.Where(x => x.ejes == filter.ejes);
            }

            if (filter.tarjetaCirculacion != null)
            {
                vehiculo = vehiculo.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                vehiculo = vehiculo.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.tamanoMotor != null)
            {
                vehiculo = vehiculo.Where(x => x.tamanoMotor == filter.tamanoMotor);
            }

            if (filter.llantas != null)
            {
                vehiculo = vehiculo.Where(x => x.llantas == filter.llantas);
            }

            if (filter.distancia != null)
            {
                vehiculo = vehiculo.Where(x => x.distancia.ToLower().Equals(filter.distancia.ToLower()));
            }

            if (filter.potencia != null)
            {
                vehiculo = vehiculo.Where(x => x.potencia.ToLower().Equals(filter.potencia.ToLower()));
            }

            if (filter.tornamesaGraduable != null)
            {
                vehiculo = vehiculo.Where(x => x.tornamesaGraduable.ToLower().Equals(filter.tornamesaGraduable.ToLower()));
            }

            if (filter.capacidadCarga != null)
            {
                vehiculo = vehiculo.Where(x => x.capacidadCarga.ToLower().Equals(filter.capacidadCarga.ToLower()));
            }

            if (filter.carroceria != null)
            {
                vehiculo = vehiculo.Where(x => x.carroceria.ToLower().Equals(filter.carroceria.ToLower()));
            }

            if (filter.tipoCarga != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoCarga.ToLower().Equals(filter.tipoCarga.ToLower()));
            }

            if (filter.tipoVehiculo != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoVehiculo.ToLower().Equals(filter.tipoVehiculo.ToLower()));
            }

            if (filter.tipoMotor != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoMotor.ToLower().Equals(filter.tipoMotor.ToLower()));
            }

            var listaVehiculos = vehiculo.AsEnumerable();
            //Si no es administrador global solo devuelve los equipos en la estacion de trabajo solicitada y vehiculos en ruta
            if (filter.global == null || filter.global == false)
            {
                Func<vehiculos, bool> condicionRuta = (vehiculo) => {
                    if (vehiculo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().ToUpper().Trim()))
                        return true;
                    if (vehiculo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.Bodega.ToString().ToUpper().Trim()))
                        return true;
                    if (vehiculo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.RentaInterna.ToString().ToUpper().Trim()))
                        return true;
                    if (vehiculo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.RentaExterna.ToString().ToUpper().Trim()))
                        return true;
                    return false;
                };

                listaVehiculos = listaVehiculos.Where(
                    v => v.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo
                    || condicionRuta(v)
                );
            }

            var pagedVehiculos = PagedList<vehiculos>.create(vehiculo, filter.PageNumber, filter.PageSize);
            return pagedVehiculos;
        }

        public vehiculos GetVehiculoByID(int idActivo)
        {
            var vehiculo = _unitOfWork.vehiculosRepository.GetAllEstadoActual().Where(v => v.activoOperacion.id == idActivo).FirstOrDefault();

            return vehiculo;
        }


        //Devuelve un listado de Generadores con su estado actual
        //Enviar idEmpresa para realizar la consulta
        public PagedList<generadores> GetGeneradoresEstadoActual(generadoresQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            ///TODO validar que venga idEmpresa
            var generador = _unitOfWork.generadoresRepository.GetAllEstadoActual().Where(v => v.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();

            if (filter.codigo != null)
            {
                generador = generador.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                generador = generador.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                generador = generador.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    generador = generador.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    generador = generador.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    generador = generador.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    generador = generador.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idTipoGenerador != null)
            {
                generador = generador.Where(x => x.idTipoGenerador == filter.idTipoGenerador);
            }

            if (filter.capacidadGalones != null)
            {
                generador = generador.Where(x => x.capacidadGalones == filter.capacidadGalones);
            }

            if (filter.numeroCilindros != null)
            {
                generador = generador.Where(x => x.numeroCilindros == filter.numeroCilindros);
            }

            if (filter.marcaGenerador != null)
            {
                generador = generador.Where(x => x.marcaGenerador.ToLower().Equals(filter.marcaGenerador.ToLower()));
            }

            if (filter.tipoInstalacion != null)
            {
                generador = generador.Where(x => x.tipoInstalacion.ToLower().Equals(filter.tipoInstalacion.ToLower()));
            }

            if (filter.tipoEnfriamiento != null)
            {
                generador = generador.Where(x => x.tipoEnfriamiento.ToLower().Equals(filter.tipoEnfriamiento.ToLower()));
            }


            var listaGeneradores = generador.AsEnumerable();
            //Si no es administrador global solo devuelve los Generadores en la estacion de trabajo solicitada y vehiculos en ruta
            if (filter.global == null || filter.global == false)
            {
                Func<generadores, bool> condicionRuta = (generador) => {
                    if (generador.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains("EGRESADO"))
                        return true;
                    if (generador.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains("BODEGA"))
                        return true;
                    if (generador.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains("RENTAINTERNA"))
                        return true;
                    if (generador.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains("RENTAEXTERNA"))
                        return true;
                    return false;
                };

                listaGeneradores = listaGeneradores.Where(
                    g => g.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo
                    || condicionRuta(g)
                );
            }

            var pagedGeneradores = PagedList<generadores>.create(listaGeneradores, filter.PageNumber, filter.PageSize);
            return pagedGeneradores;
        }

        //Devuelve un generador por Id
        public generadores GetGeneradorByID(int idActivo)
        {
            //TODO incluir la ultima condicion
            var generador = _unitOfWork.generadoresRepository.GetAllEstadoActual().Where(g => g.activoOperacion.id == idActivo).FirstOrDefault();

            return generador;
        }

        //Devuelve un listado de Equipos con su estado actual
        //Enviar idEmpresa para realizar la consulta
        public PagedList<equipoRemolque> GetEquiposEstadoActual(equipoRemolqueQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            ///TODO validar que venga idEmpresa
            var equipo = _unitOfWork.equipoRemolqueRepository.GetAllEstadoActual().Where(v => v.activoOperacion.idEmpresa == filter.idEmpresa).AsQueryable();

            if (filter.codigo != null)
            {
                equipo = equipo.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                equipo = equipo.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                equipo = equipo.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    equipo = equipo.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    equipo = equipo.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    equipo = equipo.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    equipo = equipo.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idTipoEquipoRemolque != null)
            {
                equipo = equipo.Where(x => x.idTipoEquipoRemolque == filter.idTipoEquipoRemolque);
            }
            

            if (filter.noEjes != null)
            {
                equipo = equipo.Where(x => x.noEjes == filter.noEjes);
            }

            if (filter.tarjetaCirculacion != null)
            {
                equipo = equipo.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                equipo = equipo.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.tandemCorredizo != null)
            {
                equipo = equipo.Where(x => x.tandemCorredizo.ToLower().Equals(filter.tandemCorredizo.ToLower()));
            }

            if (filter.chasisExtensible != null)
            {
                equipo = equipo.Where(x => x.chasisExtensible.ToLower().Equals(filter.chasisExtensible.ToLower()));
            }

            if (filter.chasisExtensible != null)
            {
                equipo = equipo.Where(e => e.chasisExtensible.ToLower().Contains(filter.chasisExtensible.ToLower()));
            }

            if (filter.tipoCuello != null)
            {
                equipo = equipo.Where(e => e.tipoCuello.ToLower().Contains(filter.tipoCuello.ToLower()));
            }

            if (filter.acopleGenset != null)
            {
                equipo = equipo.Where(e => e.acopleGenset.ToLower().Contains(filter.acopleGenset.ToLower()));
            }

            if (filter.acopleDolly != null)
            {
                equipo = equipo.Where(e => e.acopleDolly.ToLower().Contains(filter.acopleDolly.ToLower()));
            }

            if (filter.capacidadCargaLB != null)
            {
                equipo = equipo.Where(e => e.capacidadCargaLB.ToLower().Contains(filter.capacidadCargaLB.ToLower()));
            }

            if (filter.medidaLB != null)
            {
                equipo = equipo.Where(e => e.medidaLB.ToLower().Contains(filter.medidaLB.ToLower()));
            }

            if (filter.medidaPlataforma != null)
            {
                equipo = equipo.Where(e => e.medidaPlataforma.ToLower().Contains(filter.medidaPlataforma.ToLower()));
            }

            if (filter.pechera != null)
            {
                equipo = equipo.Where(e => e.pechera.ToLower().Contains(filter.pechera.ToLower()));
            }

            if (filter.alturaContenedor != null)
            {
                equipo = equipo.Where(e => e.alturaContenedor.ToLower().Contains(filter.alturaContenedor.ToLower()));
            }

            if (filter.tipoContenedor != null)
            {
                equipo = equipo.Where(e => e.tipoContenedor.ToLower().Contains(filter.tipoContenedor.ToLower()));
            }

            if (filter.marcaUR != null)
            {
                equipo = equipo.Where(e => e.marcaUR.ToLower().Contains(filter.marcaUR.ToLower()));
            }

            if (filter.largoFurgon != null)
            {
                equipo = equipo.Where(e => e.largoFurgon.ToLower().Contains(filter.largoFurgon.ToLower()));
            }

            if (filter.suspension != null)
            {
                equipo = equipo.Where(e => e.suspension.ToLower().Contains(filter.suspension.ToLower()));
            }

            if (filter.rieles != null)
            {
                equipo = equipo.Where(e => e.rieles.ToLower().Contains(filter.rieles.ToLower()));
            }

            var listaEquipos = equipo.AsEnumerable();
            //Si no es administrador global solo devuelve los equipos en la estacion de trabajo solicitada y vehiculos en ruta
            if (filter.global == null || filter.global == false)
            {
                Func<equipoRemolque, bool> condicionRuta = (equipo) => {
                    if (equipo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.Egresado.ToString().ToUpper().Trim()))
                        return true;
                    if (equipo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.Bodega .ToString().ToUpper().Trim()))
                        return true;
                    if (equipo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.RentaInterna.ToString().ToUpper().Trim()))
                        return true;
                    if (equipo.activoOperacion.movimientoActual.estado.evento.ToString().ToUpper().Trim().Contains(ControlActivosEventos.RentaExterna.ToString().ToUpper().Trim()))
                        return true;
                    return false;
                };

                listaEquipos = listaEquipos.Where(
                e => e.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo
                || condicionRuta(e)
                );
            }

            var pagedEquipos = PagedList<equipoRemolque>.create(listaEquipos, filter.PageNumber, filter.PageSize);
            return pagedEquipos;
        }

        public IEnumerable<vehiculos> reporteInventarioVehiculos(vehiculosQueryFilter filter, int usuario)
        {
            ///valida que venga idEmpresa
            var vehiculosQuery = _unitOfWork.vehiculosRepository.reporteInventario((int)filter.idEmpresa, usuario).AsQueryable();
            if (vehiculosQuery is null)
                throw new AguilaException("Es necesario que ingrese una empresa", 404);

            if (filter.codigo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    vehiculosQuery = vehiculosQuery.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idTipoVehiculo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.idTipoVehiculo == filter.idTipoVehiculo);
            }

            if (filter.motor != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.motor == filter.motor);
            }

            if (filter.ejes != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.ejes == filter.ejes);
            }

            if (filter.tarjetaCirculacion != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.tamanoMotor != null)
            {
                vehiculosQuery = vehiculosQuery.Where(x => x.tamanoMotor == filter.tamanoMotor);
            }

            if (filter.llantas != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.llantas == filter.llantas);
            }

            if (filter.distancia != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.distancia.ToLower().Contains(filter.distancia.ToLower()));
            }

            if (filter.potencia != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.potencia.ToLower().Contains(filter.potencia.ToLower()));
            }

            if (filter.tornamesaGraduable != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.tornamesaGraduable.ToLower().Contains(filter.tornamesaGraduable.ToLower()));
            }

            if (filter.capacidadCarga != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.capacidadCarga.ToLower().Contains(filter.capacidadCarga.ToLower()));
            }

            if (filter.carroceria != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.carroceria.ToLower().Contains(filter.carroceria.ToLower()));
            }

            if (filter.tipoCarga != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.tipoCarga.ToLower().Contains(filter.tipoCarga.ToLower()));
            }

            if (filter.tipoVehiculo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.tipoVehiculo.ToLower().Contains(filter.tipoVehiculo.ToLower()));
            }

            if (filter.tipoMotor != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.tipoMotor.ToLower().Contains(filter.tipoMotor.ToLower()));
            }

            if (filter.codigo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.activoOperacion.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.idEstacionTrabajo != null)
            {
                vehiculosQuery = vehiculosQuery.Where(e => e.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            var equipos = vehiculosQuery.ToList();
            return equipos;
        }

        public IEnumerable<equipoRemolque> reporteInventarioEquipo(equipoRemolqueQueryFilter filter, int usuario)
        {
            ///valida que venga idEmpresa
            var equiposQuery = _unitOfWork.equipoRemolqueRepository.reporteInventario((int)filter.idEmpresa, usuario).AsQueryable();
            if (equiposQuery is null)
                throw new AguilaException("Es necesario que ingrese una empresa", 404);

            if (filter.codigo != null)
            {
                equiposQuery = equiposQuery.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                equiposQuery = equiposQuery.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                equiposQuery = equiposQuery.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    equiposQuery = equiposQuery.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    equiposQuery = equiposQuery.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    equiposQuery = equiposQuery.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    equiposQuery = equiposQuery.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

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

            if (filter.idEstado != null)
            {
                equiposQuery = equiposQuery.Where(e => e.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.idEstacionTrabajo != null)
            {
                equiposQuery = equiposQuery.Where(e => e.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            var equipos = equiposQuery.ToList();
            return equipos;
        }

        public IEnumerable<generadores> reporteInventarioGeneradores(generadoresQueryFilter filter, int usuario)
        {
            ///valida que venga idEmpresa
            var generadoresQuery = _unitOfWork.generadoresRepository.reporteInventario((int)filter.idEmpresa, usuario).AsQueryable();
            if (generadoresQuery is null)
                throw new AguilaException("Es necesario que ingrese una empresa", 404);

            if (filter.codigo != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEstado != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {

                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

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
                generadoresQuery = generadoresQuery.Where(x => x.tipoEnfriamiento == filter.tipoEnfriamiento);
            }

            if (filter.codigo != null)
            {
                generadoresQuery = generadoresQuery.Where(e => e.activoOperacion.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa != null)
            {
                generadoresQuery = generadoresQuery.Where(e => e.activoOperacion.idEmpresa == filter.idEmpresa);
            }

            if (filter.idEstado != null)
            {
                generadoresQuery = generadoresQuery.Where(e => e.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                generadoresQuery = generadoresQuery.Where(e => e.activoOperacion.flota.ToLower().Contains(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    generadoresQuery = generadoresQuery.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idEstacionTrabajo != null)
            {
                generadoresQuery = generadoresQuery.Where(e => e.activoOperacion.movimientoActual.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            var generadores = generadoresQuery.ToList();
            return generadores;
        }
        
        //Devuelve un equipo por Id
        public equipoRemolque GetEquipoByID(int idActivo)
        {
            var equipo = _unitOfWork.equipoRemolqueRepository.GetAllEstadoActual().Where(v => v.activoOperacion.id == idActivo).FirstOrDefault();
            
            return equipo;
        }
    }
}
