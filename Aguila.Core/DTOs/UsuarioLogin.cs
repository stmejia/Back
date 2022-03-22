using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class UsuarioLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmacion { get; set; }
        public string PasswordAnterior { get; set; }
    }
}
