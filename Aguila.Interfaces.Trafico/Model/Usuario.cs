using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    [Table("_SysUsuarios")]
    public class Usuario
    {        
        [Key]
        public short usua_ID { get; set; }

        public string usua_nombre { get; set; }
    }
}
