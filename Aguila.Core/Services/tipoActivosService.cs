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
    public class tipoActivosService : ItipoActivosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoActivosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoActivos> GetTipoActivos(tipoActivosQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipos = _unitOfWork.tipoActivosRepository.GetAll();

            if (filter.codigo != null)
            {
                tipos = tipos.Where(x => x.codigo.ToLower().Contains(filter.codigo.ToLower()));

            }

            if (filter.nombre != null)
            {
                tipos = tipos.Where(x => x.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.area != null)
            {
                tipos = tipos.Where(x => x.area.ToLower().Contains(filter.area.ToLower()));
            }


            if (filter.operaciones != null)
            {
                tipos = tipos.Where(x => x.operaciones == filter.operaciones);
            }

            if (filter.idCuenta != null)
            {
                tipos = tipos.Where(x => x.idCuenta == filter.idCuenta);
            }

            var pagedTipos = PagedList<tipoActivos>.create(tipos, filter.PageNumber, filter.PageSize);

            return pagedTipos;
        }

        public async Task<tipoActivos> GetTipoActivo(int id)
        {
            return await _unitOfWork.tipoActivosRepository.GetByID(id);
        }

        public async Task InsertTipoActivo(tipoActivos tipo)
        {

            //reparacionesQueryFilter filter = new reparacionesQueryFilter();
            //filter.codigo = reparacion.codigo;
            //filter.idEmpresa = reparacion.idEmpresa;


            //var currentReparacion = GetReparaciones(filter);
            //if (currentReparacion.LongCount() > 0)
            //{
            //    throw new AguilaException("Valor Duplicado! ya existe este codigo en la empresa indicada....", 406);
            //}
                       

            tipo.id = 0;
            tipo.fechaCreacion = DateTime.Now;
            await _unitOfWork.tipoActivosRepository.Add(tipo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoActivo(tipoActivos tipo)
        {
            var currentTipo = await _unitOfWork.tipoActivosRepository.GetByID(tipo.id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Activo No Existente!....");
            }

            //currentTipo.codigo = tipo.codigo;
            currentTipo.nombre = tipo.nombre;
            currentTipo.area = tipo.area;
            currentTipo.operaciones = tipo.operaciones;
            currentTipo.idCuenta = tipo.idCuenta;
            currentTipo.porcentajeDepreciacionAnual = tipo.porcentajeDepreciacionAnual;


            _unitOfWork.tipoActivosRepository.Update(currentTipo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoActivo(int id)
        {
            var currentTipo = await _unitOfWork.tipoActivosRepository.GetByID(id);
            if (currentTipo == null)
            {
                throw new AguilaException("Tipo Activo No Existente!....");
            }

            await _unitOfWork.tipoActivosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
