using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        public UsuariosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IImagenesRecursosService imagenesRecursosService )
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _imagenesRecursosService = imagenesRecursosService;
        }
        public async  Task<PagedList<Usuarios>> GetUsuarios(UsuarioQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            
            // Este get incluye ImagenRecurso de la propiedad perfil del usuario
            var usuarios = _unitOfWork.UsuariosRepository.GetAllIncludes();


            if (filter.nombre != null)
            {
                usuarios = usuarios.Where(x => x.Nombre.ToLower().Contains(filter.nombre.ToLower()));
            }
            if(filter.FchCreacion != null)
            {
                DateTime filtroFecha = (DateTime)filter.FchCreacion;
                //usuarios = usuarios.Where(x => x.FchCreacion.ToShortDateString() == filter.FchCreacion?.ToShortDateString());
                usuarios = usuarios.Where(x => x.FchCreacion.Date == filtroFecha.Date);
            }

            var pagedUsuarios = PagedList<Usuarios>.create(usuarios, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedUsuarios.Select(e => e.ImagenPerfil).ToList());            
            
            return pagedUsuarios;
        }

        public async Task<Usuarios> GetUsuario(long id)
        {
            //var usuario = await _unitOfWork.UsuariosRepository.GetByID(id);

            var usuario = await _unitOfWork.UsuariosRepository.GetByIdIncludes(id);

            //Manejo de imagenes
            if (usuario != null && usuario.ImagenRecurso_IdPerfil != null && usuario.ImagenRecurso_IdPerfil != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(usuario.ImagenRecurso_IdPerfil ?? Guid.Empty);
                usuario.ImagenPerfil = imgRecurso;
            }
            //Fin Imagenes

            //Enviar Logo de Empresa
            foreach(Empresas xEmpresa in usuario.EstacionesTrabajoAsignadas.Select(e => e.EstacionTrabajo.Sucursal.Empresa ).ToList())
            {
                if (xEmpresa.ImagenRecurso_IdLogo != null) {                    
                    var xEmpresaImagenLogo = usuario.EstacionesTrabajoAsignadas.Select(x=>x.EstacionTrabajo.Sucursal.Empresa)
                        .Where(x=>x.Id == xEmpresa.Id).Select(x=>x.ImagenLogo).FirstOrDefault();

                    if (xEmpresaImagenLogo == null) { 
                        var imgEmpresa = await _imagenesRecursosService.GetByID(xEmpresa.ImagenRecurso_IdLogo ?? Guid.Empty);                        
                        xEmpresa.ImagenLogo = imgEmpresa;
                    }
                    else 
                    {                        
                        xEmpresa.ImagenLogo = xEmpresaImagenLogo;
                    }           
                    
                }
                
            }
            return usuario;
        }

        public async Task InsertUsuario(Usuarios usuario)
        {
            //validacion de modulo de Usuario
            var existeModulo = await _unitOfWork.ModulosRepository.GetByID(usuario.ModuloId);

            if (existeModulo == null)
                throw new AguilaException("Modulo No Existente!....");

            //valida si existe estacion de trabajo
            var existeEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuario.EstacionTrabajoId);

            if (existeEstacion == null)
                throw new AguilaException("Estacion de Trabajo No Existente!....");

            //valida si existe la sucursal
            var existeSucursal = await _unitOfWork.SucursalRepository.GetByID(usuario.SucursalId);
            if (existeSucursal == null)
                throw new AguilaException("Sucursal No Existente!....");

            usuario.Id = 0;
            usuario.FchCreacion = DateTime.Now;
            usuario.fchBloqueado = null;
            usuario.Activo = true;
            usuario.FchPassword = DateTime.Now;
            usuario.cambiarClave = true;

            // Guardamos el Recurso de Imagen
            if (usuario.ImagenPerfil != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(usuario.ImagenPerfil,"Usuarios",nameof(usuario.ImagenPerfil));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    usuario.ImagenRecurso_IdPerfil = imgRecurso.Id;

                usuario.ImagenPerfil = null;
            }
            //  Fin de recurso de Imagen          

            await _unitOfWork.UsuariosRepository.Add(usuario);            
            await _unitOfWork.SaveChangeAsync();
            estacionModuloDefault(usuario);
            await _unitOfWork.SaveChangeAsync();

        }

        public async Task<bool> UpdateUsuario(Usuarios usuario)
        {
            //valida que exista el usuarios a actualizar

            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(usuario.Id);

            if (currentUsuario == null)            
                throw new AguilaException("Usuario No Existente!....", 404);            

            //validacion de modulo de Usuario

            var existeModulo = await _unitOfWork.ModulosRepository.GetByID(usuario.ModuloId);
            if (existeModulo == null)
                throw new AguilaException("Modulo No Existente!....");


            //valida si existe estacion de trabajo
            var existeEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(usuario.EstacionTrabajoId);

            if (existeEstacion == null)
                throw new AguilaException("Estacion de Trabajo No Existente!....");

            //valida si existe la sucursal
            var existeSucursal = await _unitOfWork.SucursalRepository.GetByID(usuario.SucursalId);
            if (existeSucursal == null)
                throw new AguilaException("Sucursal No Existente!....");

            currentUsuario.Nombre = usuario.Nombre;
            currentUsuario.Email = usuario.Email;
            currentUsuario.fchNacimiento = usuario.fchNacimiento;
            currentUsuario.ModuloId = usuario.ModuloId;
            currentUsuario.EstacionTrabajoId = usuario.EstacionTrabajoId;
            currentUsuario.SucursalId = usuario.SucursalId;

            // Guardamos el Recurso de Imagen
            if(usuario.ImagenPerfil != null)
            {
                usuario.ImagenPerfil.Id = currentUsuario.ImagenRecurso_IdPerfil ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(usuario.ImagenPerfil,"Usuarios",nameof(usuario.ImagenPerfil));
                
                if (currentUsuario.ImagenRecurso_IdPerfil == null || currentUsuario.ImagenRecurso_IdPerfil == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentUsuario.ImagenRecurso_IdPerfil = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen

            estacionModuloDefault(currentUsuario);
         
            _unitOfWork.UsuariosRepository.Update(currentUsuario);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        //Este metodo inserta el modulo y la sucursal seleccionados por default a la lista de estaciones de trabajo y modulos del usuario 
        private  void estacionModuloDefault(Usuarios usuario)
        {
            if(usuario.EstacionTrabajoId != null && usuario.EstacionTrabajoId > 0)
            {
                var xEstaciones = _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.GetAll()
                    .Where(e => e.UsuarioId == usuario.Id && e.EstacionTrabajoId == usuario.EstacionTrabajoId).FirstOrDefault();
                if (xEstaciones == null)
                {
                    AsigUsuariosEstacionesTrabajo asigEstacion = new AsigUsuariosEstacionesTrabajo
                    {
                        UsuarioId = usuario.Id,
                        EstacionTrabajoId = (int)usuario.EstacionTrabajoId
                    };
                    _unitOfWork.AsigUsuariosEstacionesTrabajoRepository.Add(asigEstacion);
                }
            }

            if(usuario.ModuloId != null && usuario.ModuloId > 0)
            {
                var xModulos = _unitOfWork.AsigUsuariosModulosRepository.GetAll()
                    .Where(e => e.UsuarioId == usuario.Id && e.ModuloId == usuario.ModuloId).FirstOrDefault();

                if (xModulos == null)
                {
                    AsigUsuariosModulos asigModulo = new AsigUsuariosModulos
                    {
                        UsuarioId = usuario.Id,
                        ModuloId = (byte)usuario.ModuloId,
                    };
                    _unitOfWork.AsigUsuariosModulosRepository.Add(asigModulo);
                }
            }
        }


        public async Task<bool> DeleteUsuario(long id)
        {
            //valida que exista el usuarios a eliminar
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(id);

            // Eliminamos el recurso de imagen
            if (usuario.ImagenRecurso_IdPerfil != null && usuario.ImagenRecurso_IdPerfil != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(usuario.ImagenRecurso_IdPerfil ?? Guid.Empty);
            // fin recurso imagen

            await _unitOfWork.UsuariosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<Usuarios> GetUsuarioByUserName(string username)
        {
            return await _unitOfWork.UsuariosRepository.GetUsuarioByUserName(username);
        }

        public async Task<bool> Bloquear(long id)
        {
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetByID(id);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");
            }

            if (currentUsuario.Activo == false)
            {
                throw new AguilaException("Este usuario ya se encuentra bloqueado.!");
            }
                
            currentUsuario.Activo = false;
            currentUsuario.fchBloqueado = DateTime.Now;

            _unitOfWork.UsuariosRepository.Update(currentUsuario);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public async Task<bool> CambiarClave(UsuarioLogin usuarioLogin, bool restablecer)
        {
            var currentUsuario = await _unitOfWork.UsuariosRepository.GetUsuarioByUserName(usuarioLogin.Username);
            if (currentUsuario == null)
            {
                throw new AguilaException("Usuario No Existente!....");            
            }

            currentUsuario.Password = usuarioLogin.Password;
            currentUsuario.FchPassword = DateTime.Now;
            currentUsuario.cambiarClave = restablecer;
            currentUsuario.Activo = true;
            currentUsuario.fchBloqueado = null;

            _unitOfWork.UsuariosRepository.Update(currentUsuario);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }



    }
}
