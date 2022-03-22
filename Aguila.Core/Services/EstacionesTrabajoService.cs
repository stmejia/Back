using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class EstacionesTrabajoService : IEstacionesTrabajoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EstacionesTrabajoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //lista las estaciones de trabajo existentes
        public PagedList<EstacionesTrabajo> GetEstacionesTrabajo(EstacionesTrabajoQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var estacioneTrabajo = _unitOfWork.EstacionesTrabajoRepository.GetAll();

            if (filter.Nombre!= null)
            {
                estacioneTrabajo = estacioneTrabajo.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if (filter.Tipo != null)
            {
                estacioneTrabajo = estacioneTrabajo.Where(x => x.Tipo.ToLower().Contains(filter.Tipo.ToLower()));
            }

            if(filter.Activa != null)
            {
                estacioneTrabajo = estacioneTrabajo.Where(x => x.Activa == filter.Activa);
            }

            var pagedEstaciones = PagedList<EstacionesTrabajo>.create(estacioneTrabajo, filter.PageNumber, filter.PageSize);

            return pagedEstaciones;
        }

        public async Task<EstacionesTrabajo> GetEstacionTrabajo(int id)
        {
            return await _unitOfWork.EstacionesTrabajoRepository.GetByID(id);
        }

        public async Task InsertEstacionTrabajo(EstacionesTrabajo estacion)
        {
            //valida que existe la sucursal que se esta asociando a la estacion de trabajo
            var existeSucursal = await _unitOfWork.SucursalRepository.GetByID(estacion.SucursalId);

            if (existeSucursal == null)
            {
                throw new AguilaException("Sucursal de Estacion No Existente!....");
            }

            //reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            estacion.Id = 0;
            estacion.FchCreacion = DateTime.Now;

            await _unitOfWork.EstacionesTrabajoRepository.Add(estacion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateEstacionTrabajo(EstacionesTrabajo estacion)
        {
            //valida que la estacion de trabajo a actualizar exista
            var currentEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(estacion.Id);
            if (currentEstacion == null)
            {
                throw new AguilaException("Estacion de Trabajo No Existente!....");
            }

            //valida que la sucursal a actualizar exista
            var existeSucursal = await _unitOfWork.SucursalRepository.GetByID(estacion.SucursalId);
            if (existeSucursal == null)
            {
                throw new AguilaException("Sucursal de Estacion No Existente!....");
            }

            currentEstacion.SucursalId = estacion.SucursalId;
            currentEstacion.Tipo = estacion.Tipo;
            currentEstacion.Codigo = estacion.Codigo;
            currentEstacion.Nombre = estacion.Nombre;
            currentEstacion.Activa = estacion.Activa;
            currentEstacion.FchCreacion = estacion.FchCreacion;

            _unitOfWork.EstacionesTrabajoRepository.Update(currentEstacion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteEstacionTrabajo(int id)
        {
            //se valida que la Estacion a eliminar exista
            var estacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(id);
            if (estacion == null)
            {
                throw new AguilaException("Estacion de Trabajo No Existente!....");
            }

            await _unitOfWork.EstacionesTrabajoRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
