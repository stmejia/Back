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
    public class medidasService : ImedidasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public medidasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<medidas> GetMedidas(medidasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var medidas = _unitOfWork.medidasRepository.GetAll();

            if (filter.codigo != null)
            {
                medidas = medidas.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.nombre != null)
            {
                medidas = medidas.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            var pagedMedidas = PagedList<medidas>.create(medidas, filter.PageNumber, filter.PageSize);
            return pagedMedidas;
        }

        public async Task<medidas> GetMedida(int id)
        {
            return await _unitOfWork.medidasRepository.GetByID(id);
        }

        public async Task InsertMedida(medidas medida)
        {
            //Insertamos la fecha de ingreso del registro
            medida.id = 0;
            medida.fechaCreacion = DateTime.Now;

            await _unitOfWork.medidasRepository.Add(medida);
            await _unitOfWork.SaveChangeAsync();
        }

        public  async Task<bool> UpdateMedida(medidas medida)
        {
            var currentMedida = await _unitOfWork.medidasRepository.GetByID(medida.id);
            if (currentMedida == null)
            {
                throw new AguilaException("Medida no existente...");
            }

            currentMedida.nombre = medida.nombre;

            _unitOfWork.medidasRepository.Update(currentMedida);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteMedida(int id)
        {
            var currentMedida = await _unitOfWork.medidasRepository.GetByID(id);
            if (currentMedida == null)
            {
                throw new AguilaException("Medida no existente...");
            }

            await _unitOfWork.medidasRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
