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
    public class llantaActualService : IllantaActualService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public llantaActualService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<llantaActual> GetLlantaActual(llantaActualQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var llantaActual = _unitOfWork.llantaActualRepository.GetAll();

            if (filter.idLlanta != null)
            {
                llantaActual = llantaActual.Where(e => e.idLlanta == filter.idLlanta);
            }

            if (filter.idLlantaTipo != null)
            {
                llantaActual = llantaActual.Where(e => e.idLlantaTipo == filter.idLlantaTipo);
            }

            if (filter.idLlanta != null)
            {
                llantaActual = llantaActual.Where(e => e.idLlanta == filter.idLlanta);
            }

            if (filter.idActivoOperaciones != null)
            {
                llantaActual = llantaActual.Where(e => e.idActivoOperaciones == filter.idActivoOperaciones);
            }

            if (filter.idEstado != null)
            {
                llantaActual = llantaActual.Where(e => e.idEstado == filter.idEstado);
            }

            if (filter.ubicacionId != null)
            {
                llantaActual = llantaActual.Where(e => e.ubicacionId == filter.ubicacionId);
            }

            if (filter.documentoEstado != null)
            {
                llantaActual = llantaActual.Where(e => e.documentoEstado.ToLower().Contains(filter.documentoEstado.ToLower()));
            }

            if (filter.documentoUbicacion != null)
            {
                llantaActual = llantaActual.Where(e => e.documentoUbicacion.ToLower().Contains(filter.documentoUbicacion.ToLower()));
            }

            if (filter.observacion != null)
            {
                llantaActual = llantaActual.Where(e => e.observacion.ToLower().Contains(filter.observacion.ToLower()));
            }

            if (filter.posicion != null)
            {
                llantaActual = llantaActual.Where(e => e.posicion == filter.posicion);
            }

            if (filter.profundidadIzquierda != null)
            {
                llantaActual = llantaActual.Where(e => e.profundidadIzquierda.ToLower().Contains(filter.profundidadIzquierda.ToLower()));
            }

            if (filter.profundidadCentro != null)
            {
                llantaActual = llantaActual.Where(e => e.profundidadCentro.ToLower().Contains(filter.profundidadCentro.ToLower()));
            }

            if (filter.profundidadDerecho != null)
            {
                llantaActual = llantaActual.Where(e => e.profundidadDerecho.ToLower().Contains(filter.profundidadDerecho.ToLower()));
            }

            if (filter.reencauche != null)
            {
                llantaActual = llantaActual.Where(e => e.reencauche.ToLower().Contains(filter.reencauche.ToLower()));
            }

            if (filter.precio != null)
            {
                llantaActual = llantaActual.Where(e => e.precio == filter.precio);
            }

            if (filter.proposito != null)
            {
                llantaActual = llantaActual.Where(e => e.proposito.ToLower().Contains(filter.proposito.ToLower()));
            }

            //if (filter.fechaEstado != null)
            //{
            //    llantaActual = llantaActual.Where(e => e.fechaEstado == filter.fechaEstado);
            //}

            //if (filter.fechaUbicacion != null)
            //{
            //    llantaActual = llantaActual.Where(e => e.fechaUbicacion == filter.fechaUbicacion);
            //}

            var pagedLlantaActual = PagedList<llantaActual>.create(llantaActual, filter.PageNumber, filter.PageSize);
            return pagedLlantaActual;
        }

        public async Task<llantaActual> GetLlantaActual(int id)
        {
            return await _unitOfWork.llantaActualRepository.GetByID(id);
        }

        public async Task InsertLlantaActual(llantaActual llantaActual)
        {
            //Insertamos la fecha de ingreso del registro
            llantaActual.fechaCreacion = DateTime.Now;

            await _unitOfWork.llantaActualRepository.Add(llantaActual);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateLlantaActual(llantaActual llantaActual)
        {
            var currentLlantaActual = await _unitOfWork.llantaActualRepository.GetByID(llantaActual.idLlanta);
            if (currentLlantaActual == null)
            {
                throw new AguilaException("Llanta no existente...");
            }

            currentLlantaActual.idLlanta = llantaActual.idLlanta;
            currentLlantaActual.idLlantaTipo = llantaActual.idLlantaTipo;
            currentLlantaActual.idActivoOperaciones = llantaActual.idActivoOperaciones;
            currentLlantaActual.idEstado = llantaActual.idEstado;
            currentLlantaActual.ubicacionId = llantaActual.ubicacionId;
            currentLlantaActual.documentoEstado = llantaActual.documentoEstado;
            currentLlantaActual.documentoUbicacion = llantaActual.documentoUbicacion;
            currentLlantaActual.observacion = llantaActual.observacion;
            currentLlantaActual.posicion = llantaActual.posicion;
            currentLlantaActual.profundidadIzquierda = llantaActual.profundidadIzquierda;
            currentLlantaActual.profundidadCentro = llantaActual.profundidadCentro;
            currentLlantaActual.profundidadDerecho = llantaActual.profundidadDerecho;
            currentLlantaActual.reencauche = llantaActual.reencauche;
            currentLlantaActual.precio = llantaActual.precio;
            currentLlantaActual.proposito = llantaActual.proposito;
            currentLlantaActual.fechaEstado = llantaActual.fechaEstado;
            currentLlantaActual.fechaUbicacion = llantaActual.fechaUbicacion;

            _unitOfWork.llantaActualRepository.Update(currentLlantaActual);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteLlantaActual(int id)
        {
            var currentLlantaActual = await _unitOfWork.llantaActualRepository.GetByID(id);
            if (currentLlantaActual == null)
            {
                throw new AguilaException("Llanta no existente...");
            }

            await _unitOfWork.llantaActualRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
