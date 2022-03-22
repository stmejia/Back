using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.DTOs
{
    public class condicionTecnicaGenSetDto
    {
        public long idCondicionActivo { get; set; }
        public string bateriaCodigo { get; set; }
        public bool bateriaNivelAcido { get; set; }
        public bool bateriaArnes { get; set; }
        public bool bateriaTerminales { get; set; }
        public bool bateriaGolpes { get; set; }
        public bool bateriaCarga { get; set; }
        public bool combustibleDiesel { get; set; }
        public bool combustibleAgua { get; set; }
        public bool combustibleAceite { get; set; }
        public bool combustibleFugas { get; set; }
        public bool filtroAceite { get; set; }
        public bool filtroDiesel { get; set; }
        public bool bombaAguaEstado { get; set; }
        public bool escapeAgujeros { get; set; }
        public bool escapeDañado { get; set; }
        public bool cojinetesEstado { get; set; }
        public bool arranqueFuncionamiento { get; set; }
        public bool fajaAlternador { get; set; }
        public bool enfriamientoAire { get; set; }
        public bool enfriamientoAgua { get; set; }
        public bool cantidadGeneradaVolts { get; set; }

        public condicionActivosDto condicionActivo { get; set; }
    }
}
