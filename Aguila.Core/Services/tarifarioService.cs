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
    public class tarifarioService : ItarifarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tarifarioService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tarifario> GetTarifario(tarifarioQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tarifario = _unitOfWork.tarifarioRepository.GetAll();

            if (filter.codigo != null)
            {
                tarifario = tarifario.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.tipoCarga != null)
            {
                tarifario = tarifario.Where(e => e.tipoCarga.ToLower().Contains(filter.tipoCarga.ToLower()));
            }

            if (filter.tipoMovimiento != null)
            {
                tarifario = tarifario.Where(e => e.tipoMovimiento.ToLower().Contains(filter.tipoMovimiento.ToLower()));
            }

            if (filter.segmento != null)
            {
                tarifario = tarifario.Where(e => e.segmento.ToLower().Contains(filter.segmento.ToLower()));
            }

            if (filter.idUbicacionOrigen != null)
            {
                tarifario = tarifario.Where(e => e.idUbicacionOrigen == filter.idUbicacionOrigen);
            }

            if (filter.idUbicacionDestino != null)
            {
                tarifario = tarifario.Where(e => e.idUbicacionDestino == filter.idUbicacionDestino);
            }

            if (filter.idRuta != null)
            {
                tarifario = tarifario.Where(e => e.idRuta == filter.idRuta);
            }

            if (filter.idServicio != null)
            {
                tarifario = tarifario.Where(e => e.idServicio == filter.idServicio);
            }

            if (filter.idEmpresa != null)
            {
                tarifario = tarifario.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.combustibleGls != null)
            {
                tarifario = tarifario.Where(e => e.combustibleGls == filter.combustibleGls);
            }

            if (filter.precio != null)
            {
                tarifario = tarifario.Where(e => e.precio == filter.precio);
            }

            if (filter.kmRecorridosCargado != null)
            {
                tarifario = tarifario.Where(e => e.kmRecorridosCargado == filter.kmRecorridosCargado);
            }

            if (filter.kmRecorridosVacio != null)
            {
                tarifario = tarifario.Where(e => e.kmRecorridosVacio == filter.kmRecorridosVacio);
            }

            if (filter.esEspecializado != null)
            {
                tarifario = tarifario.Where(e => e.esEspecializado == filter.esEspecializado);
            }

            if (filter.tipoViaje != null)
            {
                tarifario = tarifario.Where(e => e.tipoViaje.ToLower().Contains(filter.tipoViaje.ToLower()));
            }

            //if (filter.fechaVigencia != null)
            //{
            //    tarifario = tarifario.Where(e => e.fechaVigencia == filter.fechaVigencia);
            //}

            var pagedTarifario = PagedList<tarifario>.create(tarifario, filter.PageNumber, filter.PageSize);
            return pagedTarifario;
        }

        public async Task<tarifario> GetTarifario(int id)
        {
            return await _unitOfWork.tarifarioRepository.GetByID(id);
        }

        public async Task InsertTarifario(tarifario tarifario)
        {
            //Insertamos la fecha de ingreso del registro
            tarifario.id = 0;
            tarifario.fechaCreacion = DateTime.Now;

            await _unitOfWork.tarifarioRepository.Add(tarifario);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTarifario(tarifario tarifario)
        {
            var currentTarifario = await _unitOfWork.tarifarioRepository.GetByID(tarifario.id);
            if (currentTarifario == null)
            {
                throw new AguilaException("Tarifa no existente...");
            }

            currentTarifario.tipoCarga = tarifario.tipoCarga;
            currentTarifario.tipoMovimiento = tarifario.tipoMovimiento;
            currentTarifario.segmento = tarifario.segmento;
            currentTarifario.idUbicacionOrigen = tarifario.idUbicacionOrigen;
            currentTarifario.idUbicacionDestino = tarifario.idUbicacionDestino;
            currentTarifario.idRuta = tarifario.idRuta;
            currentTarifario.idServicio = tarifario.idServicio;
            currentTarifario.idEmpresa = tarifario.idEmpresa;
            currentTarifario.combustibleGls = tarifario.combustibleGls;
            currentTarifario.precio = tarifario.precio;
            currentTarifario.kmRecorridosCargado = tarifario.kmRecorridosCargado;
            currentTarifario.kmRecorridosVacio = tarifario.kmRecorridosVacio;
            currentTarifario.esEspecializado = tarifario.esEspecializado;
            currentTarifario.tipoViaje = tarifario.tipoViaje;
            currentTarifario.fechaVigencia = tarifario.fechaVigencia;

            _unitOfWork.tarifarioRepository.Update(currentTarifario);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTarifario(int id)
        {
            var currentTarifario = await _unitOfWork.tarifarioRepository.GetByID(id);
            if (currentTarifario == null)
            {
                throw new AguilaException("Tarifa no existente...");
            }

            await _unitOfWork.tarifarioRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
