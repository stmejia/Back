using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class controlEquipoAjenoService : IcontrolEquipoAjenoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoMovimientosService _activoMovimientosService;

        public controlEquipoAjenoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                           IactivoMovimientosService activoMovimientosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoMovimientosService = activoMovimientosService;
        }


        public PagedList<controlEquipoAjeno> GetEquiposEjanos(controlEquipoAjenoQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var filtroFechas = false;
            var ajenos = _unitOfWork.controlEquipoAjenoRepository.GetAllIncludes();

            if (filter.idEstacionTrabajo == null)
                throw new AguilaException("Debe Especificar una estacion de trabajo.", 404);

            if (filter.fechaInicio != null && filter.fechaFin != null)
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);

                filtroFechas = true;
            }

            //FILTRA LOS RESULTADO POR EL RANGO DE FECHAS ENVIADO EN EL FILTER.
            if (filtroFechas)
            {
                ajenos = ajenos.Where(e => e.salida >= filter.fechaInicio && e.salida < filter.fechaFin.Value.AddDays(1) 
                || e.ingreso >= filter.fechaInicio && e.ingreso < filter.fechaFin.Value.AddDays(1));
                ajenos = ajenos.OrderByDescending(v => v.fechaCreacion);
            }
            else
            {
                ajenos = ajenos.Where(e =>e.ingreso >= DateTime.Now.AddDays(-1) || e.salida >= DateTime.Now.AddDays(-1));
                ajenos = ajenos.OrderByDescending(v => v.fechaCreacion);
            }

            //Filtra por estacion de trabajo
            ajenos = ajenos.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);

            if (filter.empresa != null)
            {
                ajenos = ajenos.Where(v => v.empresa.ToLower().Trim().Equals(filter.empresa.ToLower().Trim()));
            }

            if (filter.nombrePiloto != null)
            {
                ajenos = ajenos.Where(v => v.nombrePiloto.ToLower().Trim().Contains(filter.nombrePiloto.ToLower().Trim()));
            }

            if (filter.placaCabezal != null)
            {
                ajenos = ajenos.Where(v => v.placaCabezal.ToLower().Trim().Contains(filter.placaCabezal.ToLower().Trim()));
            }

            if (filter.ingreso != null)
            {
                ajenos = ajenos.Where(v => v.ingreso.Value.Date == filter.ingreso.Value.Date );
            }

            if (filter.salida != null)
            {
                ajenos = ajenos.Where(v => v.salida.Value.Date == filter.salida.Value.Date);
            }

            if (filter.idUsuario != null)
            {
                ajenos = ajenos.Where(v => v.idUsuario == filter.idUsuario);
            }

            ajenos = ajenos.OrderByDescending(v => v.ingreso);

            var pagedAjenos = PagedList<controlEquipoAjeno>.create(ajenos, filter.PageNumber, filter.PageSize);
            return pagedAjenos;

        }

        public async Task<controlEquipoAjeno> GetAjeno(long id)
        {
            var currentAjeno = await _unitOfWork.controlEquipoAjenoRepository.GetByIdIncludes(id);
            if (currentAjeno == null)
            {
                throw new AguilaException("Ingreso de Equipo no existente...");
            }

            return currentAjeno;
        }


        public async Task InsertAjeno(controlEquipoAjeno ajeno)
        {
            ajeno.id = 0;
            ajeno.fechaCreacion = DateTime.Now;            
            ajeno.nombrePiloto = ajeno.nombrePiloto.ToUpper().Trim();

            if(ajeno.placaCabezal!=null)
                ajeno.placaCabezal = ajeno.placaCabezal.ToUpper().Trim();

            

            if(ajeno.codigoEquipo!=null)
                ajeno.codigoEquipo = ajeno.codigoEquipo.ToUpper().Trim();

            if(ajeno.tipoEquipo!=null)
                ajeno.tipoEquipo = ajeno.tipoEquipo.ToUpper().Trim();

            if(ajeno.codigoChasis!=null)
                ajeno.codigoChasis = ajeno.codigoChasis.ToUpper().Trim();

            if(ajeno.codigoGenerador != null)
                ajeno.codigoGenerador = ajeno.codigoGenerador.ToUpper().Trim();

            ajeno.cargado = ajeno.cargado;

            if (ajeno.origen != null)
                ajeno.origen = ajeno.origen.ToUpper().Trim();

            if (ajeno.destino != null)
                ajeno.destino = ajeno.destino.ToUpper().Trim();

            if (ajeno.marchamo != null)
                ajeno.marchamo = ajeno.marchamo.ToUpper().Trim();

            if (ajeno.empresa != null)
                ajeno.empresa = ajeno.empresa.ToUpper().Trim();

            ajeno.atc = ajeno.atc;


            await _unitOfWork.controlEquipoAjenoRepository.Add(ajeno);
            await _unitOfWork.SaveChangeAsync();            

        }

        public async Task<bool> UpdateAjeno(controlEquipoAjeno ajeno)
        {
            var currentAjeno = await _unitOfWork.controlEquipoAjenoRepository.GetByID(ajeno.id);
            if (currentAjeno == null)
            {
                throw new AguilaException("Equipo no existente...");
            }

            ajeno.nombrePiloto = ajeno.nombrePiloto.ToUpper().Trim();
            ajeno.placaCabezal = ajeno.placaCabezal.ToUpper().Trim();

            if (ajeno.codigoEquipo != null)
                ajeno.codigoEquipo = ajeno.codigoEquipo.ToUpper().Trim();
            else
                ajeno.codigoEquipo = ajeno.codigoEquipo;

            if (ajeno.tipoEquipo != null)
                ajeno.tipoEquipo = ajeno.tipoEquipo.ToUpper().Trim();
            else
                ajeno.tipoEquipo = ajeno.tipoEquipo;

            if (ajeno.codigoChasis != null)
                ajeno.codigoChasis = ajeno.codigoChasis.ToUpper().Trim();
            else
                ajeno.codigoChasis = ajeno.codigoChasis;

            if (ajeno.codigoGenerador != null)
                ajeno.codigoGenerador = ajeno.codigoGenerador.ToUpper().Trim();
            else
                ajeno.codigoGenerador = ajeno.codigoGenerador;

            ajeno.cargado = ajeno.cargado;

            if (ajeno.origen != null)
                ajeno.origen = ajeno.origen.ToUpper().Trim();
            else
                ajeno.origen = ajeno.origen;

            if (ajeno.destino != null)
                ajeno.destino = ajeno.destino.ToUpper().Trim();
            else
                ajeno.destino = ajeno.destino;

            if (ajeno.marchamo != null)
                ajeno.marchamo = ajeno.marchamo.ToUpper().Trim();
            else
                ajeno.marchamo = ajeno.marchamo;

            if (ajeno.empresa != null)
                ajeno.empresa = ajeno.empresa.ToUpper().Trim();
            else
                ajeno.empresa = ajeno.empresa;

            ajeno.atc = ajeno.atc;


            _unitOfWork.BeginTransaction();

            try
            {
                _unitOfWork.controlEquipoAjenoRepository.Update(currentAjeno);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {

                _unitOfWork.RollbackTransaction();
                throw new AguilaException("Error al guardar inforamcion: " + ex.InnerException.Message);
            }

            return true;
        }

        public async Task<bool> DeleteVisita(long id)
        {
            var currentAjeno = await _unitOfWork.controlEquipoAjenoRepository.GetByID(id);
            if (currentAjeno == null)
            {
                throw new AguilaException("Equipo no existente");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.controlVisitasRepository.Delete(id);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {

                _unitOfWork.RollbackTransaction();
                throw new AguilaException("Error al guardar inforamcion: " + ex.InnerException.Message);
            }

            return true;
        }


        public async Task<bool> ingresarPropios(controlGaritaDto control, long usuarioId)
        {
            var fecha = DateTime.Now;
            activoMovimientosDto evento = new activoMovimientosDto();
            evento.idEmpresa =(byte)control.idEmpresa;

            evento.fecha = DateTime.Now;
            if (control.movimiento.ToUpper().Equals("INGRESO"))
            {
                evento.evento = ControlActivosEventos.Ingresado;
                evento.tipoDocumento = "INGRESO";
                if (control.lleno) { evento.evento = ControlActivosEventos.IngresoConServicio; }

            }
            else {
                evento.tipoDocumento = "SALIDA";
                evento.evento = ControlActivosEventos.Egresado;
            }

            foreach (var equipo in control.equipos) {
                //se validad que sea un equipo propio
                if (equipo.propio)
                {
                    var activoMovActual =await  _unitOfWork.activoMovimientosActualRepository.GetByID(equipo.idActivo);
                    if(activoMovActual != null)
                    {
                        evento.idActivo = (int)equipo.idActivo;
                        //evento.ubicacionId = activoMovActual.ubicacionId;
                        //evento.idRuta = activoMovActual.idRuta;
                        //evento.idEstado = activoMovActual.idEstado;
                        //evento.idServicio = activoMovActual.idServicio;
                        evento.idEstacionTrabajo = control.idEstacionTrabajo;
                        evento.idEmpleado = activoMovActual.idEmpleado;
                        evento.idUsuario = usuarioId;
                        //evento.documento = activoMovActual.documento;                    

                        
                    }

                    await generarEventoEquipoPropio(evento);                   

                }

            }

            return true;

        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<bool> generarEventoEquipoPropio(activoMovimientosDto evento)
        {
            await _activoMovimientosService.InsertMovimientoPorEvento(evento);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

    }
}
