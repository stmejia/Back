using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs
{
    public class controlGaritaDto
    {
        public string movimiento { get; set; }
        public string piloto { get; set; }

        public virtual ICollection<equipoGaritaDto> equipos { get; set; }

        public string marchamo { get; set; }
        public string origenDestino { get; set; }
        public bool atc { get; set; }
        public bool lleno { get; set; }
        public int idEstacionTrabajo { get; set; }
        public int idEmpresa { get; set; }
        public string empresa { get; set; }

    }
}
