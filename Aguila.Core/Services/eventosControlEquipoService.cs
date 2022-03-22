using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
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
    public class eventosControlEquipoService : IeventosControlEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public eventosControlEquipoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<eventosControlEquipo> GetEventosControl(eventosControlEquipoQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //VALIDA QUE EXISTE UN RANGO DE FECHAS VALIDO PARA OBTENER LA INFORMNACION DE EVENTOS REGISTRADOS
            if (filter.fechaInicio == null || filter.fechaFin == null)
            {
                throw new AguilaException("Debe Especificar una Fecha de Inicio y una Fecha Final para Obtener los datos.", 404);
            }
            else
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);
            }

            //FILTRA LOS RESULTADO POR EL RANGO DE FECHAS ENVIADO EN EL FILTER.
            var eventosControl = _unitOfWork.eventosControlEquipoRepository.GetAllIncludes()
                                                .Where(e => e.fechaCreacion >= filter.fechaInicio && e.fechaCreacion < filter.fechaFin.Value.AddDays(1)).AsQueryable();

            //var eventosControl = _unitOfWork.eventosControlEquipoRepository.GetAll()
            //                                    .Where(e => e.fechaCreacion >= filter.fechaInicio && e.fechaCreacion < filter.fechaFin.Value.AddDays(1));


            if (filter.idActivo != null)
            {
                eventosControl = eventosControl.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.descripcionEvento != null)
            {
                eventosControl = eventosControl.Where(e => e.descripcionEvento.ToLower().Trim().Contains(filter.descripcionEvento.ToLower().Trim()));
            }

            if (filter.idUsuarioCreacion != null)
            {
                eventosControl = eventosControl.Where(e => e.idUsuarioCreacion == filter.idUsuarioCreacion);
            }

            if (filter.idUsuarioRevisa != null)
            {
                eventosControl = eventosControl.Where(e => e.idUsuarioRevisa == filter.idUsuarioRevisa);
            }

            if (filter.idUsuarioResuelve != null)
            {
                eventosControl = eventosControl.Where(e => e.idUsuarioResuelve == filter.idUsuarioResuelve);
            }

            if (filter.idUsuarioAnula != null)
            {
                eventosControl = eventosControl.Where(e => e.idUsuarioAnula == filter.idUsuarioAnula);
            }

            if (filter.idEstacionTrabajo != null)
            {
                eventosControl = eventosControl.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.categoria != null)
            {
                eventosControl = eventosControl.Where(e => e.activoOperacion.categoria.ToLower().Trim().Equals(filter.categoria.ToLower().Trim()));
            }

            if (filter.tipoActivo != null)
            {
                //eventosControl = eventosControl.Where(e => e.activoOperacion.codigo.Substring(0,4).ToLower().Trim().Equals(filter.tipoActivo.ToLower().Trim()));
                eventosControl = eventosControl.Where(e => e.activoOperacion.codigo.ToLower().Trim().StartsWith(filter.tipoActivo.ToLower().Trim()));
            }

            eventosControl = eventosControl.OrderByDescending(e => e.fechaCreacion);

            var pagedeventosControl = PagedList<eventosControlEquipo>.create(eventosControl, filter.PageNumber, filter.PageSize);
            return pagedeventosControl;
        }

        public async Task<eventosControlEquipo> GetEventoControl(long id)
        {
            return await _unitOfWork.eventosControlEquipoRepository.GetByIdIncludes(id);
            //return await _unitOfWork.eventosControlEquipoRepository.GetByID(id);
        }

        public async Task<eventosControlEquipoDto> InsertEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario)
        {

            _unitOfWork.BeginTransaction();

            var currentEventoControl = new eventosControlEquipo()
            {
                idActivo = eventoControlDto.idActivo,
                idUsuarioCreacion = eventoControlDto.idUsuarioCreacion,
                fechaCreacion = eventoControlDto.fechaCreacion,
                descripcionEvento = eventoControlDto.descripcionEvento,
                bitacoraObservaciones =DateTime.Now.ToString()+ " CREADO - " + nombreUsuario + ": " + eventoControlDto.bitacoraObservaciones,
                fechaRevisado = null,
                idUsuarioRevisa = null,
                fechaResuelto = null,
                idUsuarioResuelve = null,
                fechaAnulado = null,
                idUsuarioAnula = null,
                idEstacionTrabajo = eventoControlDto.idEstacionTrabajo
            };

            try
            {
                await _unitOfWork.eventosControlEquipoRepository.Add(currentEventoControl);
                await _unitOfWork.SaveChangeAsync();
                eventoControlDto.id = currentEventoControl.id;
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new AguilaException(ex.Message, 422);
                
            }

            //await _unitOfWork.eventosControlEquipoRepository.Add(currentEventoControl);
            //await _unitOfWork.SaveChangeAsync();
            //eventoControlDto.id = currentEventoControl.id;

            return eventoControlDto;
        }

        public async Task<bool> UpdateEventoControl(eventosControlEquipoDto eventoControlDto)
        {
            var currrentEventoControl = await _unitOfWork.eventosControlEquipoRepository.GetByID(eventoControlDto.id);
            if (currrentEventoControl == null)
            {
                throw new AguilaException("Evento No Existente, verifique sus datos.", 404);
            }
            currrentEventoControl.descripcionEvento = eventoControlDto.descripcionEvento;

            _unitOfWork.eventosControlEquipoRepository.Update(currrentEventoControl);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> RevisarEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario)
        {
            var currrentEventoControl = await _unitOfWork.eventosControlEquipoRepository.GetByID(eventoControlDto.id);
            if (currrentEventoControl == null)
            {
                throw new AguilaException("Evento No Existente, verifique sus datos.", 404);
            }


            currrentEventoControl.bitacoraObservaciones = currrentEventoControl.bitacoraObservaciones + System.Environment.NewLine +" | "
                                                          + DateTime.Now.ToString() + " REVISADO - "+nombreUsuario + ": "+ eventoControlDto.bitacoraObservaciones;
            currrentEventoControl.fechaRevisado = eventoControlDto.fechaRevisado;
            currrentEventoControl.fechaResuelto = null;
            currrentEventoControl.fechaAnulado = null;
            currrentEventoControl.idUsuarioResuelve = null;
            currrentEventoControl.idUsuarioAnula = null;
            currrentEventoControl.idUsuarioRevisa = eventoControlDto.idUsuarioRevisa;

            _unitOfWork.eventosControlEquipoRepository.Update(currrentEventoControl);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> ResolverEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario)
        {
            var currrentEventoControl = await _unitOfWork.eventosControlEquipoRepository.GetByID(eventoControlDto.id);
            if (currrentEventoControl == null)
            {
                throw new AguilaException("Evento No Existente, verifique sus datos.", 404);
            }


            currrentEventoControl.bitacoraObservaciones = currrentEventoControl.bitacoraObservaciones + System.Environment.NewLine + " | "
                                                           + DateTime.Now.ToString() + " RESUELTO - " + nombreUsuario + ": " + eventoControlDto.bitacoraObservaciones;

            currrentEventoControl.fechaResuelto = eventoControlDto.fechaResuelto;
            currrentEventoControl.idUsuarioResuelve = eventoControlDto.idUsuarioResuelve;
            currrentEventoControl.fechaAnulado = null;
            currrentEventoControl.idUsuarioAnula = null;

            _unitOfWork.eventosControlEquipoRepository.Update(currrentEventoControl);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> AnularEventoControl(eventosControlEquipoDto eventoControlDto, string nombreUsuario)
        {
            var currrentEventoControl = await _unitOfWork.eventosControlEquipoRepository.GetByID(eventoControlDto.id);
            if (currrentEventoControl == null)
            {
                throw new AguilaException("Evento No Existente, verifique sus datos.", 404);
            }


            currrentEventoControl.bitacoraObservaciones = currrentEventoControl.bitacoraObservaciones + System.Environment.NewLine + " | "
                                                           + DateTime.Now.ToString() + " ANULADO - " + nombreUsuario + ": " + eventoControlDto.bitacoraObservaciones;
            currrentEventoControl.fechaAnulado = eventoControlDto.fechaAnulado;
            currrentEventoControl.idUsuarioAnula = eventoControlDto.idUsuarioAnula;

            _unitOfWork.eventosControlEquipoRepository.Update(currrentEventoControl);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteEventoControl(long id)
        {
            var currrentEventoControl = await _unitOfWork.eventosControlEquipoRepository.GetByID(id);
            if (currrentEventoControl == null)
            {
                throw new AguilaException("Evento No Existente, verifique sus datos.", 404);
            }

            await _unitOfWork.eventosControlEquipoRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
