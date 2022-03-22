using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class clientes
    {
        public int id { get; set; }
        public int idTipoCliente { get; set; }
        public long idDireccion { get; set; }
        public long idEntidadComercial { get; set; }
        public string codigo { get; set; }
        //public int idCorporacion { get; set; }
        public byte idEmpresa { get; set; }
        public int diasCredito { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual tipoClientes tipoCliente { get; set; }
        public virtual direcciones direccion { get; set; }
        public virtual entidadComercial entidadComercial { get; set; }
        //public virtual corporaciones corporacion { get; set; }

    }
}
