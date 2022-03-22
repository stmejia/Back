using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.QueryFilters
{
    public class vehiculosQueryFilter
    {
        public int? idActivo { get; set; }
        public int? idTipoVehiculo { get; set; }
        public string motor { get; set; }
        public byte? ejes { get; set; }
        public string tarjetaCirculacion { get; set; }
        public string placa { get; set; }
        public int? tamanoMotor { get; set; }
        public int? llantas { get; set; }
        public string distancia { get; set; }
        public string potencia { get; set; }
        public string tornamesaGraduable { get; set; }
        public string capacidadCarga { get; set; }
        public string carroceria { get; set; }
        public string tipoCarga { get; set; }
        public string tipoVehiculo { get; set; }
        public string tipoMotor { get; set; }
        public string codigo { get; set; }
        public byte? idEmpresa { get; set; }
        public int? idEstado { get; set; }
        public string flota { get; set; }
        public bool? propio { get; set; }
        public bool? equipoActivo { get; set; }
        public bool? global { get; set; }
        public int? idEstacionTrabajo { get; set; }
        public string polizaSeguro { get; set; }

        public string tipoMovimiento { get; set; }
        public string dobleRemolque { get; set; }
        public string aptoParaCentroAmerica { get; set; }
        public string medidaFurgon { get; set; }

        public bool ignorarFechas { get; set; } = false;
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
