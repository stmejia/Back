using Aguila.Core.Interfaces.Services;
using Aguila.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Aguila.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;

        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string Hash, string password)
        {
            // Este metodo no descifra una clave previamente grabada
            // Cifra de nuevo con la clave enviada y realizar la comparacion de las claves cifradas
            var parts = Hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Formato hash inesperado");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algoritm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations
               ))
            {
                var keyToCheck = algoritm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }

        }

        public string Hash(string password)
        {
            // PBKDF2 implementation
            // saltsize minimo de 8
            // iteraciones minimo 10000
            using (var algoritm = new Rfc2898DeriveBytes(
               password,
               _options.SaltSize,
               _options.Iterations
            ))
            {
                var key = Convert.ToBase64String(algoritm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algoritm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";
            }

        }
    }

}
