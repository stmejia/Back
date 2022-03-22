using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Entities
{
    public class CondicionTaller : Documento
    {
        //public long id { get; set; }
        public int idActivo { get; set; }
        public int idEmpleado { get; set; }
        public string observaciones { get; set; }
    }

}
