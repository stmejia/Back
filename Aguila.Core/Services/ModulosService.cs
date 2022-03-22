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
    public class ModulosService : IModulosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        
        public ModulosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //Lista los Modulos existentes
        public PagedList<Modulos> GetModulos(ModuloQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var modulos = _unitOfWork.ModulosRepository.GetAll();

            if(filter.nombre != null)
            {
                modulos = modulos.Where(x=>x.Nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if(filter.activo !=null )
            {
                modulos = modulos.Where(x=> x.Activo ==filter.activo);
            }

            var pagedModulos = PagedList<Modulos>.create(modulos, filter.PageNumber, filter.PageSize);
            return pagedModulos;
        }

        //Lista un Modulo por su Id
        public async Task<Modulos> GetModulo(byte id)
        {
            return await _unitOfWork.ModulosRepository.GetByID(id);
        }

        public async Task InsertModulo(Modulos modulo)
        {
            //reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            modulo.Id = 0;

            await _unitOfWork.ModulosRepository.Add(modulo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> updateModulo(Modulos modulo)
        {
            var currentModulo = await _unitOfWork.ModulosRepository.GetByID(modulo.Id);
            if (currentModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            currentModulo.Nombre = modulo.Nombre;
            currentModulo.Activo = modulo.Activo;
            currentModulo.path = modulo.path;
            currentModulo.ModuMinVersion = modulo.ModuMinVersion;

            _unitOfWork.ModulosRepository.Update(currentModulo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteModulo(byte id)
        {
            var currentModulo = await _unitOfWork.ModulosRepository.GetByID(id);
            if (currentModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            await _unitOfWork.ModulosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
