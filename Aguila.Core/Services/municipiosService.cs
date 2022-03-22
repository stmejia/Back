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
    public class municipiosService : ImunicipiosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public municipiosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<municipios> GetMunicipio(municipiosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var municipios = _unitOfWork.municipiosRepository.GetAll();

            if (filter.idDepartamento != null)
            {
                municipios = municipios.Where(e => e.idDepartamento == filter.idDepartamento);
            }

            if (filter.codMunicipio != null)
            {
                municipios = municipios.Where(e => e.codMunicipio.ToLower().Contains(filter.codMunicipio.ToLower()));
            }

            if (filter.nombreMunicipio != null)
            {
                municipios = municipios.Where(e => e.nombreMunicipio.ToLower().Contains(filter.nombreMunicipio.ToLower()));
            }

            municipios = municipios.OrderBy(e => e.nombreMunicipio);

            var PagedMunicipios = PagedList<municipios>.create(municipios, filter.PageNumber, filter.PageSize);
            return PagedMunicipios;
        }

        public async Task<municipios> GetMunicipio(int id)
        {
            return await _unitOfWork.municipiosRepository.GetByID(id);
        }

        public async Task InsertMunicipio(municipios municipio)
        {
            municipiosQueryFilter filter = new municipiosQueryFilter();
            filter.codMunicipio = municipio.codMunicipio;
            filter.idDepartamento = municipio.idDepartamento;

            var currentMunicipio= GetMunicipio(filter);
            if (currentMunicipio.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este código en el departamento indicado....", 406);
            }

            //Insertamos la fecha de ingreso del registro
            municipio.id = 0;
            municipio.fechaCreacion = DateTime.Now;

            await _unitOfWork.municipiosRepository.Add(municipio);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateMunicipio(municipios municipio)
        {
            var currentMunicipio = await _unitOfWork.municipiosRepository.GetByID(municipio.id);
            if (currentMunicipio == null)
            {
                throw new AguilaException("Municipio no existente...");
            }

            currentMunicipio.idDepartamento = municipio.id;
            currentMunicipio.codMunicipio = municipio.codMunicipio;
            currentMunicipio.nombreMunicipio = municipio.nombreMunicipio;

            _unitOfWork.municipiosRepository.Update(currentMunicipio);
            await _unitOfWork.SaveChangeAsync();
            
            return true;
        }

        public async Task<bool> DeleteMunicipio(int id)
        {
            var currentMunicipio = await _unitOfWork.municipiosRepository.GetByID(id);
            if (currentMunicipio == null)
            {
                throw new AguilaException("Municipio no existente...");
            }

            await _unitOfWork.municipiosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
