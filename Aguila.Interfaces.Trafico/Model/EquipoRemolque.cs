using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aguila.Interfaces.Trafico.Model
{
    public class EquipoRemolque
    {
        [Key]
        public int EqRmID { get; set; }

        public int TranID { get; set; }

        public short TpEqID { get; set; }

        public short EqRmCorrel { get; set; }

        public string EqRmCodigo { get; set; }

        public string EqRmCodigoNuevo { get; set; }

        public byte EmprID { get; set; }

        public byte EqRmEjes { get; set; }

        public bool EqRmCorredizo { get; set; }

        public bool EqRmExtendible { get; set; }

        public string EqRmCuello { get; set; }

        public bool EqRmGentSet { get; set; }

        public byte EqRmDolly { get; set; }

        public bool EqRmPechera { get; set; }

        public DateTime? EqRmFchAdquisicion { get; set; }

        public DateTime? EqRmFchIni { get; set; }

        public DateTime? EqRmFchBaja { get; set; }

        public string EqRmVin { get; set; }

        public string EqRmMarca { get; set; }

        public byte? EqRmToneladas { get; set; }

        public short? EqRmAño { get; set; }

        public string EqRmPlaca { get; set; }

        public string EqRmColor { get; set; }

        public string EqRmTarjetaCirculacion { get; set; }

        public string EqRmTituloPropiedad { get; set; }

        public string EqRmPolizaImportacion { get; set; }

        public string EqRmFactura { get; set; }

        public string EqRmProveedor { get; set; }

        public decimal? EqRmValor { get; set; }

        public decimal? EqRmDepreciacionAnual { get; set; }

        public decimal? EqRmDepreciacionAcum { get; set; }

        public string EqRmObservaciones { get; set; }

        public bool EqRmUnion { get; set; }

        //public byte[] ImgTarjetaCirculacion { get; set; }

        public int? NuevoCorrelativo { get; set; }

        public string CodigoAnterior { get; set; }

        [ForeignKey("TpEqID")]
        public TipoEquipoRemolque TipoEquipoRemolque { get; set; }

    }
}
