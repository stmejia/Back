using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Entities
{
    public class vehiculos
    {
        public int idActivo { get; set; }
        public int idTipoVehiculo { get; set; }
        public string motor { get; set; }
        public byte? ejes { get; set; }
        public string tarjetaCirculacion { get; set; }
        public string placa { get; set; }
        public int? tamanoMotor { get; set; }        
        public int llantas { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string distancia { get; set; }
        public string potencia { get; set; }
        public string tornamesaGraduable { get; set; }
        public string tipoMovimiento { get; set; }
        public string dobleRemolque { get; set; }
        public string aptoParaCentroAmerica { get; set; }
        public string capacidadCarga { get; set; }
        public string carroceria { get; set; }
        public string tipoCarga { get; set; }
        public string medidaFurgon { get; set; }
        public string tipoVehiculo { get; set; }
        public Guid? idImagenRecursoTarjetaCirculacion { get; set; }
        //public string capacidadMontacarga { get; set; }

        public string tipoMotor { get; set; }
        public string polizaSeguro { get; set; }

        public  activoOperaciones activoOperacion { get; set; }
        public tipoVehiculos tipoVehiculos { get; set; }
        public ImagenRecurso imagenTarjetaCirculacion { get; set; }

    }
}
