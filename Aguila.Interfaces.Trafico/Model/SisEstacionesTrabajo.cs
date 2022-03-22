using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class SisEstacionesTrabajo
    {
        [Key]
        public int Esta_ID { get; set; }

        public int Sucu_ID { get; set; }

        public byte StTp_ID { get; set; }

        public int Esta_codigo { get; set; }

        public string Esta_Nombre { get; set; }

        public string Esta_Abreviatura { get; set; }

        public string Esta_Aleas { get; set; }

        public bool Esta_activ { get; set; }

        public DateTime Esta_Fch_Ini { get; set; }

        public byte Empr_ID { get; set; }

        public byte Sucu_codigo { get; set; }

        public string Sucu_Nombre { get; set; }

        public string Sucu_Abreviatura { get; set; }

        public string Sucu_Aleas { get; set; }

        public string Sucu_Dir { get; set; }

        public bool Sucu_activ { get; set; }

        public DateTime Sucu_fch_ini { get; set; }

        public byte Empr_codigo { get; set; }

        public string Empr_Nombre { get; set; }

        public string Empr_Abreviatura { get; set; }

        public string Empr_aleas { get; set; }

        public bool Empr_Activ { get; set; }

        public DateTime Empr_fch_ini { get; set; }

        public string Empr_nit { get; set; }

        public string Empr_dir { get; set; }

        public string empr_tels { get; set; }

        public string empr_Mail { get; set; }

        public string empr_WebPage { get; set; }

        public byte[] empr_logo { get; set; }

        public string BaseDatos { get; set; }

        public string empr_Departamento { get; set; }

        public string empr_Municipio { get; set; }

        public byte? DiasAtc { get; set; }

        public bool UbicarEquipoFinCirculacion { get; set; }

        public bool UbicarDisponibleRemolque { get; set; }

        public bool UbicarDisponibleEquipo { get; set; }

        public bool UbicarDisponibleGenerador { get; set; }

        public bool UbicarDisponibleContenedor { get; set; }

    }
}
