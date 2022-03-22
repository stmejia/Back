using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class vehiculosDto
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
        //public string capacidadMontacarga { get; set; }
        public string tipoMotor { get; set; }
        //public string tipoMaquinaria { get; set; }
        public int idEstacion { get; set; }
        public string polizaSeguro { get; set; }
        public Guid? idImagenRecursoTarjetaCirculacion { get; set; }

        public  activoOperacionesDto activoOperacion { get; set; }
        public tipoVehiculosDto tipoVehiculos { get; set; }
        public ImagenRecurso imagenTarjetaCirculacion { get; set; }
    }
}
