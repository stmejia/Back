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
    public class asesoresService : IasesoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public asesoresService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<asesores> GetAsesores(asesoresQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var asesores = _unitOfWork.asesoresRepository.GetAll();

            if (filter.codigo != null)
            {
                asesores = asesores.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                asesores = asesores.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.idEmpleado != null)
            {
                asesores = asesores.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            var pagedAsesores = PagedList<asesores>.create(asesores, filter.PageNumber, filter.PageSize);
            return pagedAsesores;
        }

        public async Task<asesores> GetAsesor(int id)
        {
            return await _unitOfWork.asesoresRepository.GetByID(id);
        }

        public async Task InsertAsesor(asesores asesor)
        {
            //Insertamos la fecha de ingreso del registro
            asesor.id = 0;
            asesor.fechaCreacion = DateTime.Now;

            await _unitOfWork.asesoresRepository.Add(asesor);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateAsesor(asesores asesor)
        {
            var currentAsesor = await _unitOfWork.asesoresRepository.GetByID(asesor.id);
            if (currentAsesor == null)
            {
                throw new AguilaException("Asesor no existente...");
            }

            currentAsesor.nombre = asesor.nombre;
            currentAsesor.idEmpleado = asesor.idEmpleado;

            _unitOfWork.asesoresRepository.Update(currentAsesor);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteAsesor(int id)
        {
            var currentAsesor = await _unitOfWork.asesoresRepository.GetByID(id);
            if (currentAsesor == null)
            {
                throw new AguilaException("Asesor no existente...");
            }

            await _unitOfWork.asesoresRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
