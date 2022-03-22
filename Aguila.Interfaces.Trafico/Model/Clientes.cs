using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key]
        public int ClieID { get; set; }

        public int ClieCodigo { get; set; }

        public byte EmprID { get; set; }

        public byte ClClID { get; set; }

        public DateTime? ClieFchIni { get; set; }

        public DateTime? ClieFchBaja { get; set; }

        public string ClieNombre { get; set; }

        public string ClieNombreRegistrado { get; set; }

        //public int? ContactoID { get; set; }

        public string ClieDireccion { get; set; }

        public string ClieTelefonos { get; set; }

        public string ClieFax { get; set; }

        public string ClieNit { get; set; }

        public string ClieMail { get; set; }

        public string ClieAnotaciones { get; set; }

        public bool ClieNaviera { get; set; }

    }
}
