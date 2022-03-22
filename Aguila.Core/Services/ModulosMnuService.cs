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
    public class ModulosMnuService : IModulosMnuService
    {
       

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ModulosMnuService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //retorna el listado de ModulosMnu existentes
        public PagedList<ModulosMnu> GetModulosMnu(ModulosMnuQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var modulosMnu = _unitOfWork.ModulosMnuRepository.GetAll();

            if(filter.MenuIdPadre != null)
            {
                modulosMnu = modulosMnu.Where(x => x.MenuIdPadre==filter.MenuIdPadre);
            }

            if (filter.Descrip != null)
            {
                modulosMnu = modulosMnu.Where(x => x.Descrip.ToLower().Contains(filter.Descrip.ToLower()));
            }

            var pagedModulos = PagedList<ModulosMnu>.create(modulosMnu, filter.PageNumber, filter.PageSize);
            return pagedModulos;
            
        }

        //retorna un ModuloMnu por su id
        public async Task<ModulosMnu> GetModuloMnu(int id)
        {
            return await _unitOfWork.ModulosMnuRepository.GetByID(id);
        }

        //insesrta un ModuloMnu
        public async Task InsertModuloMnu(ModulosMnu modulosMnu)
        {
            var existeModulo = await _unitOfWork.ModulosRepository.GetByID(modulosMnu.ModuloId);

            if (existeModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            if (modulosMnu.RecursoId != null)
            {
                var existeRecurso = await _unitOfWork.RecursosRepository.GetByID((int)modulosMnu.RecursoId);
                if (existeRecurso == null)
                {
                    throw new AguilaException("Recurso No Existente!....");
                }
            }
            //reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            modulosMnu.Id = 0;

            await _unitOfWork.ModulosMnuRepository.Add(modulosMnu);
            await _unitOfWork.SaveChangeAsync();
        }

        //actualiza un ModuloMnu
        public async Task<bool> updateModuloMnu(ModulosMnu modulosMnu)
        {
            var currentModuloMnu = await GetModuloMnu(modulosMnu.Id);
            if (currentModuloMnu == null)
            {
                throw new AguilaException("Modulo Menu No Existente!....");
            }

            var existeModulo = await _unitOfWork.ModulosRepository.GetByID(modulosMnu.ModuloId);

            if (existeModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            if (modulosMnu.RecursoId != null)
            {
                var existeRecurso = await _unitOfWork.RecursosRepository.GetByID((int)modulosMnu.RecursoId);
                if (existeRecurso == null)
                {
                    throw new AguilaException("Recurso No Existente!....");
                }
            }

            currentModuloMnu.ModuloId = modulosMnu.ModuloId;
            currentModuloMnu.MenuIdPadre = modulosMnu.MenuIdPadre;
            currentModuloMnu.Codigo = modulosMnu.Codigo;
            currentModuloMnu.Descrip = modulosMnu.Descrip;
            currentModuloMnu.RecursoId = modulosMnu.RecursoId;
            currentModuloMnu.Activo = modulosMnu.Activo;

            _unitOfWork.ModulosMnuRepository.Update(currentModuloMnu);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        //elimina un ModuloMnu
        public async Task<bool> DeleteModuloMnu(int id)
        {
            //se valida que el modulo menu a eliminar exista
            var currentModuloMnu = await GetModuloMnu(id);
            if (currentModuloMnu == null)
            {
                throw new AguilaException("Modulo Menu No Existente!....");
            }

            await _unitOfWork.ModulosMnuRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
