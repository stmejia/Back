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
    public class serviciosService : IserviciosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public serviciosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<servicios> GetServicios(serviciosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var servicios = _unitOfWork.serviciosRepository.GetAll();

            if (filter.codigo != null)
            {
                servicios = servicios.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                servicios = servicios.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.nombre != null)
            {
                servicios = servicios.Where(e => e.nombre == filter.nombre);
            }

            if (filter.precio != null)
            {
                servicios = servicios.Where(e => e.precio == filter.precio);
            }

            if (filter.ruta != null)
            {
                servicios = servicios.Where(e => e.ruta == filter.ruta);
            }

            var pagedServicios = PagedList<servicios>.create(servicios, filter.PageNumber, filter.PageSize);
            return pagedServicios;
        }

        public async Task<servicios> GetServicio(int id)
        {
            return await _unitOfWork.serviciosRepository.GetByID(id);
        }

        public async Task InsertServicio(servicios servicio)
        {
            serviciosQueryFilter filter = new serviciosQueryFilter();
            filter.codigo = servicio.codigo;
            filter.idEmpresa = servicio.idEmpresa;

            var currentServicios = GetServicios(filter);
            if (currentServicios.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este código en la empresa indicada....", 406);
            }

            //Insertamos la fecha de ingreso del registro
            servicio.id = 0;
            servicio.fechaCreacion = DateTime.Now;

            await _unitOfWork.serviciosRepository.Add(servicio);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateServicio(servicios servicio)
        {
            var currentServicio = await _unitOfWork.serviciosRepository.GetByID(servicio.id);
            if (currentServicio == null)
            {
                throw new AguilaException("Servicio no existente...");
            }

            currentServicio.idEmpresa = servicio.idEmpresa;
            currentServicio.nombre = servicio.nombre;
            currentServicio.precio = servicio.precio;
            currentServicio.ruta = servicio.ruta;

            _unitOfWork.serviciosRepository.Update(currentServicio);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteServicio(int id)
        {
            var currentServicio = await _unitOfWork.serviciosRepository.GetByID(id);
            if (currentServicio == null)
            {
                throw new AguilaException("Servicio no existente...");
            }

            await _unitOfWork.serviciosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
