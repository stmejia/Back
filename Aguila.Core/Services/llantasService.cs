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
    public class llantasService : IllantasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public llantasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<llantas> GetLlantas(llantasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var llantas = _unitOfWork.llantasRepository.GetAll();

            if (filter.idEstadoIngreso != null)
            {
                llantas = llantas.Where(e => e.idEstadoIngreso == filter.idEstadoIngreso);
            }

            if (filter.proveedorId != null)
            {
                llantas = llantas.Where(e => e.proveedorId == filter.proveedorId);
            }

            if (filter.idLlantaTipo != null)
            {
                llantas = llantas.Where(e => e.idLlantaTipo == filter.idLlantaTipo);
            }

            if (filter.codigo != null)
            {
                llantas = llantas.Where(e => e.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.marca != null)
            {
                llantas = llantas.Where(e => e.marca.ToLower().Contains(filter.marca.ToLower()));
            }

            if (filter.serie != null)
            {
                llantas = llantas.Where(e => e.serie.ToLower().Contains(filter.serie.ToLower()));
            }

            if (filter.reencaucheIngreso != null)
            {
                llantas = llantas.Where(e => e.reencaucheIngreso.ToLower().Contains(filter.reencaucheIngreso.ToLower()));
            }

            if (filter.medidas != null)
            {
                llantas = llantas.Where(e => e.medidas.ToLower().Contains(filter.medidas.ToLower()));
            }

            if (filter.precio != null)
            {
                llantas = llantas.Where(e => e.precio == filter.precio);
            }

            if (filter.llantaDoble != null)
            {
                llantas = llantas.Where(e => e.llantaDoble == filter.llantaDoble);
            }

            //if (filter.fechaIngreso != null)
            //{
            //    llantas = llantas.Where(e => e.fechaIngreso == filter.fechaIngreso);
            //}

            if (filter.fechaBaja != null)
            {
                llantas = llantas.Where(e => e.fechaBaja == filter.fechaBaja);
            }

            if (filter.propositoIngreso != null)
            {
                llantas = llantas.Where(e => e.propositoIngreso == filter.propositoIngreso);
            }

            var pagedLlantas = PagedList<llantas>.create(llantas, filter.PageNumber, filter.PageSize);
            return pagedLlantas;
        }

        public async Task<llantas> GetLlanta(int id)
        {
            return await _unitOfWork.llantasRepository.GetByID(id);
        }

        public async Task InsertLlanta(llantas llanta)
        {
            //Insertamos la fecha de ingreso del registro
            llanta.id = 0;
            llanta.fechaCreacion = DateTime.Now;

            await _unitOfWork.llantasRepository.Add(llanta);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateLlanta(llantas llanta)
        {
            var currentLlanta = await _unitOfWork.llantasRepository.GetByID(llanta.id);
            if (currentLlanta == null)
            {
                throw new AguilaException("Llanta no existente...");
            }

            currentLlanta.idEstadoIngreso = llanta.idEstadoIngreso;
            currentLlanta.proveedorId = llanta.proveedorId;
            currentLlanta.idLlantaTipo = llanta.idLlantaTipo;
            currentLlanta.marca = llanta.marca;
            currentLlanta.serie = llanta.serie;
            currentLlanta.reencaucheIngreso = llanta.reencaucheIngreso;
            currentLlanta.medidas = llanta.medidas;
            currentLlanta.precio = llanta.precio;
            currentLlanta.llantaDoble = llanta.llantaDoble;
            currentLlanta.fechaIngreso = llanta.fechaIngreso;
            currentLlanta.fechaBaja = llanta.fechaBaja;
            currentLlanta.propositoIngreso = llanta.propositoIngreso;

            _unitOfWork.llantasRepository.Update(currentLlanta);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteLlanta(int id)
        {
            var currentLlanta = await _unitOfWork.llantasRepository.GetByID(id);
            if (currentLlanta == null)
            {
                throw new AguilaException("Llanta no existente...");
            }

            await _unitOfWork.llantasRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
