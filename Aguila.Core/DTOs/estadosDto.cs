using Aguila.Core.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class estadosDto
    {
        public int id { get; set; }
        public byte idEmpresa { get; set; }
        public string codigo { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public int numeroOrden { get; set; }
        public bool disponible { get; set; }
        public string evento { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public virtual bool circulacion { get {

                var eventoCirculacion = new List<string>()
                {
                    ControlActivosEventos.Egresado.ToString().ToUpper().Trim(),
                    ControlActivosEventos.Bodega.ToString().ToUpper().Trim(),
                    ControlActivosEventos.RentaInterna.ToString().ToUpper().Trim(),
                    ControlActivosEventos.RentaExterna.ToString().ToUpper().Trim(),
                };

                var eventos = evento.Split(",");

                foreach (var evento in eventos)
                {
                    if (eventoCirculacion.Contains(evento.ToUpper().Trim()))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
