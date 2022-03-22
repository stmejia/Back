using System;
using System.Collections.Generic;
using System.Text;
using Aguila.Core.Entities;

namespace Aguila.Core.DTOs
{
    public class ingresoDto
    {
        public int idActivo { get; set; }
        public byte idEmpresa { get; set; }
        public int idEstacionTrabajo { get; set; }
        public string guardiaNombre { get; set; }
        public string placa { get; set; }
        public string flota { get; set; }
        public string transporte { get; set; }
        public string tipoEquipo { get; set; }
        public string piloto { get; set; }
        public int idPiloto { get; set; }
        public long condicion { get; set; }
        public string observaciones { get; set; }
        public bool tipoMovimiento { get; set; }//True ingreso, False salida
        public bool cargado { get; set; }//True cargado, False no cargado
        public TipoEvento evento { get; set; }

        public enum TipoEvento
        {
            Ingreso,
            Salida,
            Reserva,
            Bodega,
            QuitarReserva
        }

    }
}
