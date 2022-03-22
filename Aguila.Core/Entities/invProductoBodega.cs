using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class invProductoBodega
    {
        public int id { get; set; }
        public int idBodega { get; set; }
        public int idProducto { get; set; }
        public int estante { get; set; }
        public int pasillo { get; set; }
        public int nivel { get; set; }
        public int lugar { get; set; }
        public int? maximo { get; set; }
        public int? minimo { get; set; }
        public DateTime fechaCreacion { get; set; }

        public virtual productos producto { get; set; }
        public virtual EstacionesTrabajo estacionesTrabajo  { get; set; }
    }
}
