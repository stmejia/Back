using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.DTOs
{
    public class condicionLlantaDto
    {
        private readonly string separador = ";";
        public string  codigo { get; set; }
        public string estado { get; set; }
        public int id { get; set; }
        public string marca { get; set; }
        public string observaciones { get; set; }
        public string profundidadCto { get; set; }
        public string profundidadDer { get; set; }
        public string profundidadIzq { get; set; }
        public string psi  { get; set; }

        public condicionLlantaDto() { }
        public condicionLlantaDto(string llanta) {
            llantaString = llanta;
        }

        public string llantaString { set {
                if (string.IsNullOrEmpty(value))
                    return; 
                

                var xPropiedades = value.Trim().Split(separador);
                codigo = xPropiedades[0];
                marca = xPropiedades[1];
                profundidadIzq = xPropiedades[2];
                profundidadCto = xPropiedades[3];
                profundidadDer = xPropiedades[4];
                psi = xPropiedades[5];
                estado = xPropiedades[6];
                observaciones = xPropiedades[7];
            } 
        }

        public override string ToString()
        {
            string xString = "";

            xString += codigo.Trim() + separador;
            xString += marca.Trim() + separador;
            xString += profundidadIzq.Trim() + separador;
            xString += profundidadCto.Trim() + separador;
            xString += profundidadDer.Trim() + separador;
            xString += psi.Trim() + separador;
            xString += estado.Trim() + separador;
            xString += observaciones == null ? "" : observaciones.ToString().Trim() + separador;

            return xString;
        }
    }
}
