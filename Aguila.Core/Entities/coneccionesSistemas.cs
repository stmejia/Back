using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class coneccionesSistemas
    {
        public int idEmpresa { get; set; }

        public int idEmpresaExterno { get; set; }

        public string modulo { get; set; }

        public string moduloExterno { get; set; }

        public string servidor { get; set; }

        public string baseDatos { get; set; }

    }
}
