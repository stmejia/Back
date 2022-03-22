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

    public class tipoReparacionesService : ItipoReparacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoReparacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoReparaciones> GetTiposReparaciones(tipoReparacionesQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipos = _unitOfWork.tipoReparacionesRepository.GetAll();

            if (filter.codigo != null)
            {
                tipos = tipos.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                tipos = tipos.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.nombre != null)
            {
                tipos = tipos.Where(x => x.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.descripcion != null)
            {
                tipos = tipos.Where(x => x.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            tipos = tipos.OrderBy(x => x.fechaCreacion);

            var pagedTipos = PagedList<tipoReparaciones>.create(tipos, filter.PageNumber, filter.PageSize);

            return pagedTipos;
        }

        public async Task<tipoReparaciones> GetTipoReparacion(int id)
        {
            return await _unitOfWork.tipoReparacionesRepository.GetByID(id);
        }

        public async Task InsertTipoReparacion(tipoReparaciones tipo)
        {
            tipoReparacionesQueryFilter filter = new tipoReparacionesQueryFilter();
            filter.codigo = tipo.codigo;
            filter.idEmpresa = tipo.idEmpresa;

            var currentTipo =  GetTiposReparaciones(filter);
            if (currentTipo.LongCount()>0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este codigo en la empresa indicada....", 406);
            }

            tipo.id = 0;
            tipo.fechaCreacion = DateTime.Now;
            await _unitOfWork.tipoReparacionesRepository.Add(tipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoReparacion(tipoReparaciones tipo)
        {
            var currentTipo = await _unitOfWork.tipoReparacionesRepository.GetByID(tipo.id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Reparacion No Existente!....");
            }
                   

            //currentTipo.codigo = tipo.codigo;
            currentTipo.nombre = tipo.nombre;
            currentTipo.descripcion = tipo.descripcion;


            _unitOfWork.tipoReparacionesRepository.Update(currentTipo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoReparacion(int id)
        {
            var currentTipo = await _unitOfWork.tipoReparacionesRepository.GetByID(id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Reparacion No Existente!....");
            }

            await _unitOfWork.tipoReparacionesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
