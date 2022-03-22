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
    public class tipoGeneradoresService : ItipoGeneradoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoGeneradoresService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoGeneradores> GetTipoGeneradores(tipoGeneradoresQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipoGeneradores = _unitOfWork.tipoGeneradoresRepository.GetAll();

            if (filter.idEmpresa != null)
            {
                tipoGeneradores = tipoGeneradores.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.prefijo != null)
            {
                tipoGeneradores = tipoGeneradores.Where(x => x.prefijo.ToLower().Equals(filter.prefijo.ToLower()));
            }

            if (filter.codigo != null)
            {
                tipoGeneradores = tipoGeneradores.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                tipoGeneradores = tipoGeneradores.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            var pagedTipoGeneradores = PagedList<tipoGeneradores>.create(tipoGeneradores, filter.PageNumber, filter.PageSize);
            return pagedTipoGeneradores;
        }

        public async Task<tipoGeneradores> GetTipoGenerador(int id)
        {
            return await _unitOfWork.tipoGeneradoresRepository.GetByID(id);
        }

        public async Task InsertTipoGenerador(tipoGeneradores tipoGenerador)
        {
            //Insertamos la fecha de ingreso del registro
            tipoGenerador.id = 0;
            tipoGenerador.fechaCreacion = DateTime.Now;

            switch (tipoGenerador.prefijo.ToUpper())
            {
                case "GS01":
                    tipoGenerador.estructuraCoc = "tipoInstalacion,marcaGenerador,0000,flota";
                    break;                                   
            }

            await _unitOfWork.tipoGeneradoresRepository.Add(tipoGenerador);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoGenerador(tipoGeneradores tipoGenerador)
        {
            var currentTipoGenerador = await _unitOfWork.tipoGeneradoresRepository.GetByID(tipoGenerador.id);

            if (currentTipoGenerador == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            currentTipoGenerador.descripcion = tipoGenerador.descripcion;
            currentTipoGenerador.prefijo = tipoGenerador.prefijo;

            switch (tipoGenerador.prefijo.ToUpper())
            {
                case "GS01":
                    tipoGenerador.estructuraCoc = "tipoInstalacion,marcaGenerador,0000,flota";
                    break;
            }

            currentTipoGenerador.estructuraCoc = tipoGenerador.estructuraCoc;
            
            _unitOfWork.tipoGeneradoresRepository.Update(currentTipoGenerador);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoGenerador(int id)
        {
            var currentTipoGenerador = await _unitOfWork.tipoGeneradoresRepository.GetByID(id);
            if (currentTipoGenerador == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.tipoGeneradoresRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
