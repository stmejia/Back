using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class Movimientos
    {
        [Key]
        public int MoviID { get; set; }

        public int EstaID { get; set; }

        public short TpDcID { get; set; }

        public byte? MoviAño { get; set; }

        public int MoviCorrelativo { get; set; }

        public string MoviCodigo { get; set; }

        public DateTime? MoviFch { get; set; }

        public int SlMIID { get; set; }

        public int PiloID { get; set; }

        public int TranID { get; set; }

        public int RemoID { get; set; }

        public int EqRmID { get; set; }

        public int? GeneID { get; set; }

        public int? ContID { get; set; }

        public bool MoviLleno { get; set; }

        public DateTime? MoviFchFin { get; set; }

        public int? MoviKmsRecorridos { get; set; }

        public decimal? MoviGlsConsumidos { get; set; }

        public string MoviGlsObserv { get; set; }

        public string MoviTicket { get; set; }

        public bool MoviPendiente { get; set; }

        public bool EqRmPendiente { get; set; }

        public bool GenePendiente { get; set; }

        public bool ContPendiente { get; set; }

        public bool MoviCamino { get; set; }

        public string MoviPiloSecundario { get; set; }

        public string MoviPiloLicenciaSecu { get; set; }

        public string MoviPlacaCabezalSecu { get; set; }

        public bool PagarPiloto { get; set; }

        public bool MoviCobrar { get; set; }

        public string MoviObser { get; set; }

        public DateTime? MoviSysFch { get; set; }

        public short UserID { get; set; }

        public bool MoviAnul { get; set; }

        public short? UserAnulID { get; set; }

        public string MoviMotivoAnul { get; set; }

        public decimal? Flete { get; set; }

        public bool TarifaAplicada { get; set; }

        public int SectIDOrigen { get; set; }

        public int SectIDDestino { get; set; }

        public string MoviDirOrigen { get; set; }

        public string MoviDirDestino { get; set; }

        public string MoviFianzaRegimen { get; set; }

        public string MoviFianzaPoliza { get; set; }

        public long? FactID2 { get; set; }

        [ForeignKey("SlMIID")]
        public SolicitudesMovimientosIntegracion solicitudesMovimientosIntegracion { get; set; }

        [ForeignKey("EstaID")]
        public SisEstacionesTrabajo EstacionTrabajo { get; set; }

        [ForeignKey("TpDcID")]
        public TiposDocumentos TipoDocumento { get; set; }

        [ForeignKey("EqRmID")]
        public EquipoRemolque EquipoRemolque { get; set; }


    }
}
