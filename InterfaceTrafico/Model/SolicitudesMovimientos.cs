using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InterfaceTrafico.Model
{
    public class SolicitudesMovimientos
    {
        [Key]
        public int SoliID { get; set; }

        public int EstaID { get; set; }

        public byte SoliAño { get; set; }

        public short TpDcID { get; set; }

        public int SoliCorrelativo { get; set; }

        public string SoliCodigo { get; set; }

        public DateTime SoliFch { get; set; }

        public int SoliOrigenID { get; set; }

        public int SoliDestinoID { get; set; }

        public string SoliDirOrigen { get; set; }

        public string SoliDirDestino { get; set; }

        public int ClieID { get; set; }

        public string SoliConsignatario { get; set; }

        public string SoliObser { get; set; }

        public int NavieraID { get; set; }

        public string SoliClieContNombre { get; set; }

        public string SoliClieContTelefono { get; set; }

        public string SoliClieContMail { get; set; }

        public short TpMvID { get; set; }

        public int? SoliFianzaNumero { get; set; }

        public DateTime? SoliFchMov { get; set; }

        public bool SoliAut { get; set; }

        public bool SoliAnul { get; set; }

        public string SoliBooking { get; set; }

        public string SoliBL { get; set; }

        public string SoliWorkOrder { get; set; }

        public string SoliBarcoNombre { get; set; }

        public string SoliBarcoViajeNo { get; set; }

        public bool SoliFin { get; set; }

        public string UsuaIDAutorizo { get; set; }

        public DateTime SoliSysFch { get; set; }

        public short UserID { get; set; }

    }
}
