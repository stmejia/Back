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
    public class rutasService : IrutasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public rutasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<rutas> GetRutas(rutasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var rutas = _unitOfWork.rutasRepository.GetAll();

            if (filter.codigo != null)
            {
                rutas = rutas.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa is null)
            {
                throw new AguilaException("Debe ingresar un id de Empresa");
            }
            else
            {
                rutas = rutas.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.idUbicacionOrigen != null)
            {
                rutas = rutas.Where(e => e.idUbicacionOrigen == filter.idUbicacionOrigen);
            }

            if (filter.idUbicacionDestino != null)
            {
                rutas = rutas.Where(e => e.idUbicacionDestino == filter.idUbicacionDestino);
            }

            if (filter.nombre != null)
            {
                rutas = rutas.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.existeRutaAlterna != null)
            {
                rutas = rutas.Where(e => e.existeRutaAlterna == filter.existeRutaAlterna);
            }

            if (filter.distanciaKms != null)
            {
                rutas = rutas.Where(e => e.distanciaKms == filter.distanciaKms);
            }

            if (filter.gradoPeligrosidad != null)
            {
                rutas = rutas.Where(e => e.gradoPeligrosidad.ToLower().Contains(filter.gradoPeligrosidad.ToLower()));
            }

            if (filter.estadoCarretera != null)
            {
                rutas = rutas.Where(e => e.estadoCarretera.ToLower().Contains(filter.estadoCarretera.ToLower()));
            }

            var pagedRutas = PagedList<rutas>.create(rutas, filter.PageNumber, filter.PageSize);
            return pagedRutas;
        }

        public async Task<rutas> GetRuta(int id)
        {
            return await _unitOfWork.rutasRepository.GetByID(id);
        }

        public async Task InsertRuta(rutas ruta)
        {
            rutasQueryFilter filter = new rutasQueryFilter();
            filter.codigo = ruta.codigo;
            filter.idEmpresa = ruta.idEmpresa;

            var currentRuta = GetRutas(filter);
            if (currentRuta.LongCount() > 0)
            {
                throw new AguilaException("Valor Duplicado! ya existe este código en la empresa indicada....", 406);
            }

            //Insertamos la fecha de ingreso del registro
            ruta.id = 0;
            ruta.fechaCreacion = DateTime.Now;

            await _unitOfWork.rutasRepository.Add(ruta);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateRuta(rutas ruta)
        {
            var currentRuta = await _unitOfWork.rutasRepository.GetByID(ruta.id);
            if (currentRuta == null)
            {
                throw new AguilaException("Ruta no existente...");
            }

            currentRuta.idEmpresa = ruta.idEmpresa;
            currentRuta.idUbicacionOrigen = ruta.idUbicacionOrigen;
            currentRuta.idUbicacionDestino = ruta.idUbicacionDestino;
            currentRuta.nombre = ruta.nombre;
            currentRuta.existeRutaAlterna = ruta.existeRutaAlterna;
            currentRuta.distanciaKms = ruta.distanciaKms;
            currentRuta.gradoPeligrosidad = ruta.gradoPeligrosidad;
            currentRuta.estadoCarretera = ruta.estadoCarretera;

            _unitOfWork.rutasRepository.Update(currentRuta);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteRuta(int id)
        {
            var currentRuta = await _unitOfWork.rutasRepository.GetByID(id);
            if (currentRuta == null)
            {
                throw new AguilaException("Ruta no existente...");
            }

            await _unitOfWork.rutasRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
