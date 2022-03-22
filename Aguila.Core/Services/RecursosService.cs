using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Repositories;
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
    public class RecursosService : IRecursosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        
        public RecursosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Recursos> GetRecursos(RecursosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var recursos = _unitOfWork.RecursosRepository.GetAll().AsQueryable();

            if (filter.Nombre != null)
            {
                recursos = recursos.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if (filter.Tipo!=null)
            {
                recursos = recursos.Where(x => x.Tipo.ToLower().Contains(filter.Tipo.ToLower()));
            }

            if (filter.Activo!= null)
            {
                recursos = recursos.Where(x => x.Activo == filter.Activo);
            }

            if (filter.Controlador != null)
            {
                recursos = recursos.Where(x => x.Controlador.ToLower().Contains(filter.Controlador.ToLower()));
            }

            recursos = recursos.OrderByDescending(x=>x.fechaCreacion);

            var pagedRecursos = PagedList<Recursos>.create(recursos, filter.PageNumber, filter.PageSize);
            return pagedRecursos;
        }

        public  List<Recursos> GetRecursosGeneral(RecursosQueryFilter filter)
        {
            var recursos = _unitOfWork.RecursosRepository.GetAll().AsQueryable();

            if (filter.Nombre != null)
            {
                recursos = recursos.Where(x => x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if (filter.Tipo != null)
            {
                recursos = recursos.Where(x => x.Tipo.ToLower().Contains(filter.Tipo.ToLower()));
            }

            if (filter.Activo != null)
            {
                recursos = recursos.Where(x => x.Activo == filter.Activo);
            }

            if (filter.Controlador != null)
            {
                recursos = recursos.Where(x => x.Controlador.ToLower().Contains(filter.Controlador.ToLower()));
            }

            recursos = recursos.OrderByDescending(x => x.fechaCreacion);

            return recursos.ToList();
        }

        public async Task<Recursos> GetRecurso(int id)
        {
            return await _unitOfWork.RecursosRepository.GetByID(id);
        }

        public async Task InsertRecurso(Recursos recurso)
        {
            //Elimina las opciones repetidas, espacios en blanco y entradas vacias.
            string opciones = string.Join(",", recurso.opciones
                                              .Split(',')
                                              .Select(x => x.Trim())
                                              .Where(x => !string.IsNullOrWhiteSpace(x))
                                              .Distinct()
                                              .ToArray());

            recurso.opciones = opciones.ToString();
            recurso.Id = 0;//reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            recurso.fechaCreacion = DateTime.Now;

            await _unitOfWork.RecursosRepository.Add(recurso);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateRecurso(Recursos recurso)
        {
            //valida que el Recurso a actualizar exista
            var currentRecurso = await _unitOfWork.RecursosRepository.GetByID(recurso.Id);
            if (currentRecurso == null)
            {
                throw new AguilaException("Recurso No Existente!....");
            }

            currentRecurso.Nombre = recurso.Nombre;
            currentRecurso.Tipo = recurso.Tipo;
            currentRecurso.Activo = recurso.Activo;
            currentRecurso.Controlador = recurso.Controlador;

            //Elimina las opciones repetidas, espacios en blanco y entradas vacias.            
            string opciones = string.Join(",", recurso.opciones.Split(',')
                                               .Select(x => x.Trim())
                                               .Where(x => !string.IsNullOrWhiteSpace(x))
                                               .Distinct()
                                               .ToArray());

            currentRecurso.opciones = opciones;

            _unitOfWork.RecursosRepository.Update(currentRecurso);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteRecurso(int id)
        {
            //se valida que el recurso a eliminar exista
            var recurso = await _unitOfWork.RecursosRepository.GetByID(id);
            if (recurso == null)
            {
                throw new AguilaException("Recurso No Existente!....");
            }

            await _unitOfWork.RecursosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
