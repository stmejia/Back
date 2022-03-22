using Aguila.Core.CustomEntities;
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
    public class reparacionesService : IreparacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public reparacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<reparaciones> GetReparaciones(reparacionesQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var reparaciones = _unitOfWork.reparacionesRepository.GetAll();

            if (filter.codigo != null)
            {
                reparaciones = reparaciones.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                reparaciones = reparaciones.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.nombre != null)
            {
                reparaciones = reparaciones.Where(x => x.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.descripcion != null)
            {
                reparaciones = reparaciones.Where(x => x.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            if (filter.idCategoria != null)
            {
                reparaciones = reparaciones.Where(x => x.idCategoria == filter.idCategoria);
            }

            if (filter.idTipoReparacion != null)
            {
                reparaciones = reparaciones.Where(x => x.idTipoReparacion == filter.idTipoReparacion);
            }

            var pagedReparaciones = PagedList<reparaciones>.create(reparaciones, filter.PageNumber, filter.PageSize);

            return pagedReparaciones;
        }

        public async Task<reparaciones> GetReparacion(int id)
        {
            return await _unitOfWork.reparacionesRepository.GetByID(id);
        }

        public async Task InsertReparacion(reparaciones reparacion)
        {

            reparacionesQueryFilter filter = new reparacionesQueryFilter();
            filter.codigo = reparacion.codigo;
            filter.idEmpresa = reparacion.idEmpresa;
            

            var currentReparacion = GetReparaciones(filter);
            if (currentReparacion.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este codigo en la empresa indicada....", 406);
            }

            reparacion.id = 0;
            reparacion.fechaCreacion = DateTime.Now;
            await _unitOfWork.reparacionesRepository.Add(reparacion);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateReparacion(reparaciones reparacion)
        {
            var currentReparacion = await _unitOfWork.reparacionesRepository.GetByID(reparacion.id);
            if (currentReparacion == null)
            {
                throw new AguilaException("Reparacion No Existente!....");
            }

            //currentReparacion.codigo = reparacion.codigo; ;
            currentReparacion.nombre = reparacion.nombre;
            currentReparacion.descripcion = reparacion.descripcion;
            currentReparacion.idCategoria = reparacion.idCategoria;
            currentReparacion.idTipoReparacion = reparacion.idTipoReparacion;
            currentReparacion.horasHombre = reparacion.horasHombre;
            currentReparacion.idEmpresa = reparacion.idEmpresa;


            _unitOfWork.reparacionesRepository.Update(currentReparacion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteReparacion(int id)
        {
            var currentReparacion = await _unitOfWork.reparacionesRepository.GetByID(id);
            if (currentReparacion == null)
            {
                throw new AguilaException("Reparacion No Existente!....");
            }

            await _unitOfWork.reparacionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
