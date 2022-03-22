using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs.DTOsRespuestas
{
    public class vehiculosDto2
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
        public string capacidadCarga { get; set; }
        public string carroceria { get; set; }
        public string tipoCarga { get; set; }
        public string tipoVehiculo { get; set; }
        public string tipoMotor { get; set; }
        public tipoVehiculosDto tipoVehiculos { get; set; }

    }
}
