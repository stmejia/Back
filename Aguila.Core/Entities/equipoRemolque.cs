using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Core.Entities
{
    public class equipoRemolque
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
        public DateTime fechaCreacion { get; set; }
        public Guid? idImagenRecursoTarjetaCirculacion { get; set; }

        public  tipoEquipoRemolque tipoEquipoRemolque { get; set; }
        public  activoOperaciones activoOperacion { get; set; }
        public ImagenRecurso imagenTarjetaCirculacion { get; set; }

    }
}
