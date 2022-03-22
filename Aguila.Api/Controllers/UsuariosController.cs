using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        private readonly IRolesService _rolesService;
        private readonly IAsigUsuariosModulosService _asigUsuariosModulosService;
        private readonly IModulosService _modulosService;
        private readonly IUsuariosRolesService _usuariosRolesService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public UsuariosController(IUsuariosService usuariosService, IMapper mapper, IPasswordService passwordService,
                                   IRolesService rolesService,
                                   IUsuariosRolesService usuariosRolesService,
                                   IAsigUsuariosModulosService asigUsuariosModulosService,
                                   IModulosService modulosService ,
                                   IImagenesRecursosService imagenesRecursosService)
        {
            _usuariosService = usuariosService;
            _mapper = mapper;
            _passwordService = passwordService;
            _rolesService = rolesService;
            _usuariosRolesService = usuariosRolesService;
            _asigUsuariosModulosService = asigUsuariosModulosService;
            _modulosService = modulosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        /// <summary>
        /// Extraccion total de usuarios, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Lista de uruarios</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<UsuariosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsuarios([FromQuery] UsuarioQueryFilter filter)
        {
            var usuarios = await _usuariosService.GetUsuarios(filter);
            var usuariosDto = _mapper.Map<IEnumerable<UsuariosDto>>(usuarios);

            // TODO hacerlo com mapper
            var metadata = new Metadata
            {
                TotalCount = usuarios.TotalCount,
                PageSize = usuarios.PageSize,
                CurrentPage = usuarios.CurrentPage,
                TotalPages = usuarios.TotalPages,
                HasNextPage = usuarios.HasNextPage,
                HasPreviousPage = usuarios.HasPreviousPage,
                //HATEOS
                // NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPostsPageable))).ToString()
                //NextPageUrl = _uriService.GetPostPaginationUri(filters, "/api/PostPageable").ToString(),
                //PreviousPageUrl = _uriService.GetPostPaginationUri(filters, "/api/PostPageable").ToString()
            };

            var response = new AguilaResponse<IEnumerable<UsuariosDto>>(usuariosDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// <summary>
        /// Consulta de Usuario, enviar id de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<UsuariosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsuario(long id)
        {
            var usuario = await _usuariosService.GetUsuario(id);
            if (usuario == null)
            {
                throw new AguilaException("Usuario No Existente", 404);
            }

            var usuarioDto = _mapper.Map<UsuariosDto>(usuario);

            //captura los roles asginados al usuario
            UsuariosRolesQueryFilter filterRoles = new UsuariosRolesQueryFilter
            {
                usuario_id = id
            };

            var usuarioRoles = _usuariosRolesService.GetUsuariosRoles(filterRoles);
            foreach (var rol in usuarioRoles)
            {
                var currentRol = await _rolesService.GetRol(rol.rol_id);
                var currentRolDto = _mapper.Map<RolesDto>(currentRol);
                usuarioDto.Roles.Add(currentRolDto);
            }

            AsigUsuariosModulosQueryFilter filterModulos = new AsigUsuariosModulosQueryFilter
            {
                UsuarioId = id
            };

            var usuarioModulos = _asigUsuariosModulosService.GetAsigUsuariosModulos(filterModulos);
            foreach(var modulo in usuarioModulos)
            {
                var currentMoudulo = await _modulosService.GetModulo(modulo.ModuloId);
                var currentModuloDto = _mapper.Map<ModulosDto>(currentMoudulo);
                usuarioDto.Modulos.Add(currentModuloDto);
            }            

            var response = new AguilaResponse<UsuariosDto>(usuarioDto);
            return Ok(response);
        }


        /// <summary>
        /// Devuelve la configuracion de imagen para una propiedad del recurso
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpGet("/api/Usuarios/ImagenConfiguracion/{propiedad}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<ImagenRecursoConfiguracion>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImagenConfiguracion(string propiedad)
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;
            var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);

            var response = new AguilaResponse<ImagenRecursoConfiguracion>(imgRecConf);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/Usuarios/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _usuariosService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }

        /// <summary>
        /// Crear Usuario Nuevo
        /// </summary>      
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<UsuariosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(UsuariosDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuarios>(usuarioDto);

            usuario.Password = _passwordService.Hash(usuario.Password);

            await _usuariosService.InsertUsuario(usuario);

            /*Asignacion de Roles*/
            foreach(var rol in usuarioDto.Roles)
            {
                UsuariosRoles asignacion = new UsuariosRoles 
                { 
                    usuario_id = usuario.Id, 
                    rol_id = rol.id 
                };
                await _usuariosRolesService.InsertUsuarioRol(asignacion);
            }

            /*Asignacion de Modulos*/
            foreach(var modulo in usuarioDto.Modulos)
            {
                AsigUsuariosModulos asignacion = new AsigUsuariosModulos
                {
                    UsuarioId = usuario.Id,
                    ModuloId = modulo.Id
                };
                await _asigUsuariosModulosService.InsertAsigUsuarioModulo(asignacion);
            }                   

            usuarioDto = _mapper.Map<UsuariosDto>(usuario);

            var response = new AguilaResponse<UsuariosDto>(usuarioDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Usuario, enviar id de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        /// 

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<UsuariosDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, UsuariosDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuarios>(usuarioDto);
            usuario.Id = id;

            /*Se elimina los roles que el usuarios tenia asginados anteriormente*/
            await _usuariosRolesService.DeleteAll(id);

            /*Asignacion de Nuevos Roles*/
            foreach (var rol in usuarioDto.Roles)
            {
                UsuariosRoles asignacion = new UsuariosRoles
                {
                    usuario_id = usuario.Id,
                    rol_id = rol.id
                };
                await _usuariosRolesService.InsertUsuarioRol(asignacion);
            }

            /*Se elimina los modulos que el usuario tenia asignados anteriormente*/
            await _asigUsuariosModulosService.DeleteAll(id);

            /*Asignacion de nuevos Modulos*/
            foreach (var modulo in usuarioDto.Modulos)
            {
                AsigUsuariosModulos asignacion = new AsigUsuariosModulos
                {
                    UsuarioId = usuario.Id,
                    ModuloId = modulo.Id
                };
                await _asigUsuariosModulosService.InsertAsigUsuarioModulo(asignacion);
            }

            // manejo de imagenes
            //if (usuario.ImagenPerfil != null)
            //{
            //    // Solo se debe de buscar la configuracion y dejar que el service se encargue
            //    var controlador = ControllerContext.ActionDescriptor.ControllerName;
            //    var propiedad = nameof(usuarioDto.ImagenPerfil);
            //    var imgRecConf = await _imagenesRecursosService.GetConfiguracion(controlador, propiedad);
            //    usuario.ImagenPerfil.ImagenRecursoConfiguracion = imgRecConf;
            //}
            // Fin de manejo de imagenes

            var result = await _usuariosService.UpdateUsuario(usuario);
            var response = new AguilaResponse<Usuarios>(usuario);
            return Ok(response);
        }

        /// <summary>
        /// Bloquear o desactivar un usuario, el usuario no podra hacer login
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("/api/Usuarios/bloquear/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Bloquear(int id)
        {
            var result = await _usuariosService.Bloquear(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Restablece la contraseña del usuario y lo activa tambien, el usuario debera cambiar su contraseña al hacer login
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <returns></returns>
        [HttpPut("/api/Usuarios/restablecerpassword")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> restablecerPassword(UsuarioLogin usuarioLogin)
        {
            usuarioLogin.Password = _passwordService.Hash(usuarioLogin.Password);
            var result = await _usuariosService.CambiarClave(usuarioLogin,true);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }
       
        /// <summary>
        /// Eliminar Usuario, enviar id de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuariosService.DeleteUsuario(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }
    }
}
