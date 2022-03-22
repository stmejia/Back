using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class AsigUsuariosModulosService : IAsigUsuariosModulosService
    {
        //private readonly IAsigUsuariosModulosRepository _asigUsuariosModulosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public AsigUsuariosModulosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            // IAsigUsuariosModulosRepository asigUsuariosModulosRepository,
            //_asigUsuariosModulosRepository = asigUsuariosModulosRepository;
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //lista todos los usuarios y modulos asigandos de la tabla AsigUsuariosModulos
        public PagedList<AsigUsuariosModulos> GetAsigUsuariosModulos(AsigUsuariosModulosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var asigUsuariosModulos = _unitOfWork.AsigUsuariosModulosRepository.GetAll();

            if (filter.UsuarioId != null)
            {
                asigUsuariosModulos = asigUsuariosModulos.Where(x => x.UsuarioId == filter.UsuarioId);
            }

            if(filter.ModuloId != null)
            {
                asigUsuariosModulos = asigUsuariosModulos.Where(x => x.ModuloId == filter.ModuloId);
            }

            var pagedAsigUsuariosModulos = PagedList<AsigUsuariosModulos>.create(asigUsuariosModulos, filter.PageNumber, filter.PageSize);

            return pagedAsigUsuariosModulos;
        }

       // lista los mudulos asignados de un usuario especifico por su ID
        public IQueryable<AsigUsuariosModulos> GetAsigUsuarioModulos(long id)
        {
            return  _unitOfWork.AsigUsuariosModulosRepository.getAsigModulosUsuarioIncludes(id);
        }

        //inserta un nuevo modulo a un usuario en la tabla AsigUsuariosModulos
        public async Task InsertAsigUsuarioModulo(AsigUsuariosModulos usuarioModulo)
        {
            

            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioModulo.UsuarioId);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que el modulo exista
            var currentModulo = await _unitOfWork.ModulosRepository.GetByID(usuarioModulo.ModuloId);
            if (currentModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            //valida que el modulo ya este asignado al usuario

            var currentUsuarioModulo = await _unitOfWork.AsigUsuariosModulosRepository.getAsigModuloUsuario(usuarioModulo.UsuarioId, usuarioModulo.ModuloId);
            if (currentUsuarioModulo != null)
            {
                throw new AguilaException("Modulo ya asignado a Usuario!....");
            }

            await _unitOfWork.AsigUsuariosModulosRepository.Add(usuarioModulo);
            await _unitOfWork.SaveChangeAsync();
        }

        //elimina una asginacion de modulo a un usuario en la tabla AsigUsuariosModulos
        public async Task<bool> DeleteAsigUsuarioModulo(long usuarioId, byte moduloId)
        {
            //valida que el usuario exista
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuarioId);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            //valida que el modulo exista
            var currentModulo = await _unitOfWork.ModulosRepository.GetByID(moduloId);
            if (currentModulo == null)
            {
                throw new AguilaException("Modulo No Existente!....");
            }

            //valida que exista la asigancion del modulo al usuario para poder eliminarlo
            var currentUsuarioModulo = await _unitOfWork.AsigUsuariosModulosRepository.getAsigModuloUsuario(usuarioId, moduloId);
            if (currentUsuarioModulo == null)
            {
                throw new AguilaException("Asignacion de Modulo NO Existente!....");
            }

             await _unitOfWork.AsigUsuariosModulosRepository.DeleteAsigUsuarioModulo(usuarioId, moduloId);
             await  _unitOfWork.SaveChangeAsync();

            return true;
        }

        //elimina todos los modulos asignados a un usuario
        public async Task<bool> DeleteAll(long userId)
        {
            AsigUsuariosModulosQueryFilter filter = new AsigUsuariosModulosQueryFilter { UsuarioId = userId };
            var asignaciones = GetAsigUsuariosModulos(filter);

            foreach(var asignacion in asignaciones)
            {
                await _unitOfWork.AsigUsuariosModulosRepository.DeleteAsigUsuarioModulo(asignacion.UsuarioId, asignacion.ModuloId);
            }
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
