using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class LocSectores
    {
        [Key]
        public int SectID { get; set; }

        public int MuniID { get; set; }

        public string SectNombre { get; set; }

        public string SectIsoCode { get; set; }

    }
}
