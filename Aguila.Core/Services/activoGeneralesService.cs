using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class activoGeneralesService : IactivoGeneralesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public activoGeneralesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<activoGenerales> GetActivosGenerales(activoGeneralesQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activos = _unitOfWork.activoGeneralesRepository.GetAll();
            Boolean tieneFilter = false;

            if (filter.codigo != null)
            {
                activos = activos.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));
                tieneFilter = true;

            }

            if (filter.fechaCompra != null)
            {
                activos = activos.Where(x => x.fechaCreacion == filter.fechaCompra);
                tieneFilter = true;
            }

            if (filter.fechaBaja != null)
            {
                activos = activos.Where(x => x.fechaBaja == filter.fechaBaja);
                tieneFilter = true;
            }


            if (filter.idDocumentoCompra != null)
            {
                activos = activos.Where(x => x.idDocumentoCompra == filter.idDocumentoCompra);
                tieneFilter = true;
            }

            if (filter.idTipoActivo != null)
            {
                activos = activos.Where(x => x.idTipoActivo == filter.idTipoActivo);
                tieneFilter = true;
            }

            if (filter.polizaImportacion != null)
            {
                activos = activos.Where(x => x.polizaImportacion.ToLower().Contains(filter.polizaImportacion.ToLower()));
                tieneFilter = true;

            }

            if (tieneFilter == false)
            {
                throw new AguilaException("Debe enviar al menos un criterio de Filtro!....",406);
            }

            var pagedActivos = PagedList<activoGenerales>.create(activos, filter.PageNumber, filter.PageSize);

            return pagedActivos;
        }

        public async Task<activoGenerales> GetActivoGeneral(int id)
        {
            return await _unitOfWork.activoGeneralesRepository.GetByID(id);
        }

        public async Task InsertActivoGeneral(activoGenerales activo)
        {

            //reparacionesQueryFilter filter = new reparacionesQueryFilter();
            //filter.codigo = reparacion.codigo;
            //filter.idEmpresa = reparacion.idEmpresa;


            //var currentReparacion = GetReparaciones(filter);
            //if (currentReparacion.LongCount() > 0)
            //{
            //    throw new AguilaException("Valor Duplicado! ya existe este codigo en la empresa indicada....", 406);
            //}

            activo.id = 0;
            activo.fechaCreacion = DateTime.Now;
            await _unitOfWork.activoGeneralesRepository.Add(activo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateActivoGeneral(activoGenerales activo)
        {
            var currentActivo = await _unitOfWork.activoGeneralesRepository.GetByID(activo.id);
            if (currentActivo == null)
            {
                throw new AguilaException("Activo No Existente!....");
            }

            currentActivo.codigo = activo.codigo;
            currentActivo.descripcion = activo.descripcion;
            currentActivo.fechaCompra = activo.fechaCompra;
            currentActivo.valorCompra = activo.valorCompra;
            currentActivo.valorLibro = activo.valorLibro;
            currentActivo.valorRescate = activo.valorRescate;
            currentActivo.fechaBaja = activo.fechaBaja;
            currentActivo.depreciacionAcumulada = activo.depreciacionAcumulada;
            currentActivo.idDocumentoCompra = activo.idDocumentoCompra;
            currentActivo.idTipoActivo = activo.idTipoActivo;
            currentActivo.tituloPropiedad = activo.tituloPropiedad;
            currentActivo.polizaImportacion = activo.polizaImportacion;

            _unitOfWork.activoGeneralesRepository.Update(currentActivo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteActivoGeneral(int id)
        {
            var currentActivo = await _unitOfWork.activoGeneralesRepository.GetByID(id);
            if (currentActivo == null)
            {
                throw new AguilaException("Activo No Existente!....");
            }

            await _unitOfWork.activoGeneralesRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
