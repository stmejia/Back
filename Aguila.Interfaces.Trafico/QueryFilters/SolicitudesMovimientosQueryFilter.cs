using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Interfaces.Trafico.QueryFilters
{
    public class SolicitudesMovimientosQueryFilter
    {
        public int estacionTrabajoId { get; set; }
        public DateTime fechaDel { get; set; }
        public DateTime fechaAl { get; set; }
        public int? solicitudDel { get; set; }
        public int? solicitudAl { get; set; }
        public string? contenedorPrefijo { get; set; }
        public string? contenedorNumero { get; set; }
        public int? clienteCodigo { get; set; }
        public string? ClienteNombre { get; set; }
        public string? booking { get; set; }
        public Documento? documento { get; set; }
        public int? solicituMovimientoIntegracionId { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public enum Documento
        {
            Envio,
            Traslado
        }

    }

}
