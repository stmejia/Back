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

namespace Aguila.Core.Services
{
    public class departamentosService : IdepartamentosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public departamentosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<departamentos> GetDepartamento(departamentosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var departamentos = _unitOfWork.departamentosRepository.GetAll();

            if (filter.idPais != null)
            {
                departamentos = departamentos.Where(e => e.idPais == filter.idPais);
            }

            if (filter.codigo != null)
            {
                departamentos = departamentos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                departamentos = departamentos.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            departamentos = departamentos.OrderBy(e => e.nombre);

            var pagedDepartamentos = PagedList<departamentos>.create(departamentos, filter.PageNumber, filter.PageSize);

            return pagedDepartamentos;
        }

        public async Task<departamentos> GetDepartamento(int id)
        {
            return await _unitOfWork.departamentosRepository.GetByID(id);
        }

        public async Task InsertDepartamento(departamentos dptos)
        {
            departamentosQueryFilter filter = new departamentosQueryFilter();
            filter.codigo = dptos.codigo;
            filter.idPais = dptos.idPais;

            var currentDpto = GetDepartamento(filter);
            if (currentDpto.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este código en el país indicado....", 406);
            }

            //Insertamos la fecha de ingreso del registro
            dptos.id = 0;
            dptos.fechaCreacion = DateTime.Now;

            await _unitOfWork.departamentosRepository.Add(dptos);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateDepartamento(departamentos dpto)
        {
            var currentDpto = await _unitOfWork.departamentosRepository.GetByID(dpto.id);
            if (currentDpto == null)
            {
                throw new AguilaException("Departamento no existente...");
            }

            currentDpto.idPais = dpto.idPais;
            currentDpto.codigo = dpto.codigo;
            currentDpto.nombre = dpto.nombre;

            _unitOfWork.departamentosRepository.Update(currentDpto);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteDepartamento(int id)
        {
            var currentDpto = await _unitOfWork.departamentosRepository.GetByID(id);
            if (currentDpto == null)
            {
                throw new AguilaException("Departamento no existente...");
            }

            await _unitOfWork.departamentosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
