using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Core.Entities
{
    public class generadores
    {
        [Key]
        public int idActivo { get; set; }
        public int idTipoGenerador { get; set; }
        public decimal capacidadGalones { get; set; }
        public int numeroCilindros { get; set; }
        public string marcaGenerador { get; set; }
        public string tipoInstalacion { get; set; }
        public string tipoEnfriamiento { get; set; }
        public string aptoParaCA { get; set; }
        public string codigoAnterior { get; set; }
        public string tipoMotor { get; set; }
        public string noMotor { get; set; }
        public string velocidad { get; set; }
        public string potenciaMotor { get; set; }
        public string modeloGenerador { get; set; }
        public string serieGenerador { get; set; }
        public string tipoGeneradorGen { get; set; }
        public string potenciaGenerador { get; set; }
        public string tensionGenerador { get; set; }
        public string tipoTanque { get; set; }
        public string tipoAceite { get; set; }

        public DateTime fechaCreacion { get; set; }

        public activoOperaciones activoOperacion { get; set; }
        public tipoGeneradores tipoGenerador { get; set; }
    }
}
