using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuariosService _usuariosService;
        private readonly IPasswordService _passwordService;

        public SecurityController(IConfiguration configuration, IUsuariosService securityService, IPasswordService passwordService)
        {
            _configuration = configuration;
            _usuariosService = securityService;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Autenticacion de Usuario, enviar login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authentication(UsuarioLogin login)
        {
            var usuario = await _usuariosService.GetUsuarioByUserName(login.Username);

            // No se encontro el usurio
            if (usuario == null)
                throw new AguilaException("Usuario o password incorrectos!....", StatusCodes.Status404NotFound);

            // return NotFound("Usuario no existe.!");

            var pass = login.Password;

            // Si tiene que cambiar la clave el usuario debe de enviar contraseña anterior y contraseña nueva
            if (usuario.cambiarClave)
            {              
                pass = login.PasswordAnterior ?? login.Password; 
            }                      

            var passwordValid = _passwordService.Check(usuario.Password, pass);

            // Password Invalido
            if (!passwordValid )
                throw new AguilaException("Usuario y password incorrectos", StatusCodes.Status406NotAcceptable);

            // Usuario Bloqueado
            if (!usuario.Activo)
                throw new AguilaException("Usuario Bloqueado!....", StatusCodes.Status423Locked);

            // El usuario debe de cambiar su clave, debe de enviar contraseña anterior y contraseña nueva
            if (usuario.cambiarClave)
            {
                // Se debe validar el password con el passwor anterior
                if (login.PasswordAnterior == null || string.IsNullOrEmpty(login.PasswordAnterior.Trim()))
                    throw new AguilaException("Por favor proporcione una nueva clave", StatusCodes.Status428PreconditionRequired);

                // Nuevo Password y confirmacion no coinciden
                if (string.IsNullOrEmpty(login.Password.Trim()) || !login.Password.Equals(login.PasswordConfirmacion))
                    throw new AguilaException("Password y confirmacion no coinciden", StatusCodes.Status406NotAcceptable);

                login.Password = _passwordService.Hash(login.Password);
                var cambioClave = await _usuariosService.CambiarClave(login, false);             
            }

            // se envia como parameto a la funcion , el usuario autenticado
            var token = GenerateToken(usuario);

            return Ok(new { token });
        }

        // **Tupla una funcion puede regresar 2 valores
        private async Task<(bool, Usuarios)> IsValidUser(UsuarioLogin login)
        {
            // De esta manera capturamos los 2 valores devueltos por la funcion
            // validation.item1 tendra el valor booleano devuelto por la funcion
            // validation.item2 tendra el usuario tambien devuelto por la funcion

            var usuario = await _usuariosService.GetUsuarioByUserName(login.Username);

            // Validamos que exista el usuario
            if (usuario == null)
                return (false, null);

            // El usuario no esta activo
            if (!usuario.Activo)
                return (false, usuario);

            //El usuario debe de cambiar su clave
            if (usuario.cambiarClave)
                return (false, usuario);

            // Validamos si el password es correcto si existe el usuario, notar que el primer parameto ya esta encriptado el 2do no
            var isValid = _passwordService.Check(usuario.Password, login.Password);

            // Aca retornamos los 2 valores de la funcion
            return (isValid, usuario);
        }

        private string GenerateToken(Usuarios usuario)
        {
            // Header   //OJO el secret key debe ser strong sino no funciona y da un error
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secretkey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // TODO : tomar los roles de la tabla UsuarioRoles

            // Claims   informacion adicional
            var claims = new[]
            {                
                new Claim("UsuarioId", usuario.Id.ToString().Trim()),
                new Claim("UserName", usuario.Username),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Payload   , utilizamos el constructor de la clase
            var payload = new JwtPayload
            (
              _configuration["Authentication:Issuer"],
              _configuration["Authentication:Audience"],
              claims,
              DateTime.Now,
              DateTime.UtcNow.AddHours(3)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
