using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class equipoRemolqueDto
    {
        public int idActivo { get; set; }
        public int idTipoEquipoRemolque { get; set; }
        public int noEjes { get; set; }
        public string tandemCorredizo { get; set; }
        public string chasisExtensible { get; set; }
        public string tipoCuello { get; set; }
        public string acopleGenset { get; set; }
        public string acopleDolly { get; set; }
        public string capacidadCargaLB { get; set; }
        public string medidaLB { get; set; }
        public string medidaPlataforma { get; set; }
        public string tarjetaCirculacion { get; set; }
        public string placa { get; set; }
        public string pechera { get; set; }
        public string alturaContenedor { get; set; }
        public string tipoContenedor { get; set; }
        public string marcaUR { get; set; }
        public string largoFurgon { get; set; }
        //public string rielesHorizontales { get; set; }
        //public string rielesVerticales { get; set; }
        public string suspension { get; set; }
        public string rieles { get; set; }
        public int idEstacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public Guid? idImagenRecursoTarjetaCirculacion { get; set; }

        public activoOperacionesDto activoOperacion { get; set; }
        public tipoEquipoRemolqueDto tipoEquipoRemolque { get; set; }
        public ImagenRecurso imagenTarjetaCirculacion { get; set; }
    }
}
