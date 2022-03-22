using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aguila.Interfaces.Trafico.Model
{
    public class SolicitudesMovimientosIntegracion
    {
        [Key]
        public int SlMIID { get; set; }

        public int SoliID { get; set; }

        public int SlMINumero { get; set; }

        public short? TpCnID { get; set; }

        public string SlMIContPref { get; set; }

        public string SlMIContNumero { get; set; }

        public string SlMIPeso { get; set; }

        public string SlMIMercaderia { get; set; }

        public string SlMISeguridad { get; set; }

        public string SlMIEmpresaSeguridad { get; set; }

        public bool SlMIAut { get; set; }

        public string SlMIMarchamo { get; set; }

        public string SlMIATCNumero { get; set; }

        public DateTime? SlMIATCFchEmision { get; set; }

        public string SlMIDepositoContenedor { get; set; }

        public DateTime? SlMIFchEntrega { get; set; }

        public string SlMIAnotacion { get; set; }

        public bool? SlMIFianza { get; set; }

        public int ServID { get; set; }

        public byte Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public bool TarifaAplicada { get; set; }

        public bool Cobrar { get; set; }

        public DateTime? SlMISysFch { get; set; }

        public string SlMIBooking { get; set; }

        [ForeignKey("SoliID")]
        public SolicitudesMovimientos solicitudMovimiento { get; set; }

        [ForeignKey("TpCnID")]
        public TipoContenedor tipoContenedor { get; set; }

        public ICollection<Movimientos> Movimientos { get; set; }

        [ForeignKey("ServID")]
        public Servicios Servicio { get; set; }

        public EquiposEstatus EquiposEstatus { get; set; }
    }

}
