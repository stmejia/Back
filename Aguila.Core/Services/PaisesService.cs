using Aguila.Core.Interfaces.Services;
using Aguila.Infrastructure.Repositories;
using Aguila.Core.CustomEntities;
using Microsoft.Extensions.Options;
using Aguila.Core.Entities;
using System.Threading.Tasks;
using Aguila.Core.QueryFilters;
using System;
using System.Linq;
using Aguila.Core.Exceptions;

namespace Aguila.Core.Services
{
    public class paisesService : IpaisesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public paisesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<paises> GetPaises(paisesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var paises = _unitOfWork.paisesRepository.GetAll();

            if (filter.Nombre != null)
            {
                paises = paises.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if (filter.CodMoneda != null)
            {
                paises = paises.Where(x => x.CodMoneda.ToLower().Contains(filter.CodMoneda.ToLower()));
            }

            if (filter.CodAlfa2 != null)
            {
                paises = paises.Where(x => x.CodAlfa2.ToLower().Contains(filter.CodAlfa2.ToLower()));
            }

            if (filter.CodAlfa3 != null)
            {
                paises = paises.Where(x => x.CodAlfa3.ToLower().Contains(filter.CodAlfa3.ToLower()));
            }

            if (filter.CodNumerico != null)
            {
                paises = paises.Where(x => x.CodNumerico == filter.CodNumerico);
            }

            if (filter.Idioma != null)
            {
                paises = paises.Where(x => x.Idioma.ToString().Contains(filter.Idioma.ToString()));
            }

            paises = paises.OrderBy(e => e.Nombre);

            var pagedPaises = PagedList<paises>.create(paises, filter.PageNumber, filter.PageSize);

            return pagedPaises;
        }

        public async Task<paises> GetPais(int id)
        {
            return await _unitOfWork.paisesRepository.GetByID(id);
        }

        public async Task InsertPais(paises pais)
        {
            //Insertamos la fecha de ingreso del registro
            pais.Id = 0;
            pais.FechaCreacion = DateTime.Now;

            await _unitOfWork.paisesRepository.Add(pais);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePais(paises pais)
        {
            var currentPais = await _unitOfWork.paisesRepository.GetByID(pais.Id);
            if (currentPais == null)
            {
                throw new AguilaException("Pais No Existente!....");
            }

            currentPais.Nombre = pais.Nombre;
            currentPais.CodMoneda = pais.CodMoneda;
            currentPais.CodAlfa2 = pais.CodAlfa2;
            currentPais.CodAlfa3 = pais.CodAlfa3;
            currentPais.CodNumerico = pais.CodNumerico;
            currentPais.Idioma = pais.Idioma;

            _unitOfWork.paisesRepository.Update(currentPais);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeletePais(int id)
        {
            var currentPais = await _unitOfWork.paisesRepository.GetByID(id);
            if (currentPais == null)
            {
                throw new AguilaException("Pais No Existente!....");
            }

            await _unitOfWork.paisesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
