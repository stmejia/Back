using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionContenedorConfiguration : IEntityTypeConfiguration<condicionContenedor>
    {
        public void Configure(EntityTypeBuilder<condicionContenedor> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
                .HasColumnName("idCondicionActivo")
                .IsRequired();

            builder.Property(e => e.tipoContenedor)
                .HasColumnName("tipoContenedor")
                .IsRequired();

            builder.Property(e => e.exteriorMarcos)
                .HasColumnName("exteriorMarcos");

            builder.Property(e => e.exteriorMarcosObs)
                .HasColumnName("exteriorMarcosObs");

            builder.Property(e => e.puertasInteriorExterior)
                .HasColumnName("puertasInteriorExterior");

            builder.Property(e => e.puertasInteriorExteriorObs)
                .HasColumnName("puertasInteriorExteriorObs");

            builder.Property(e => e.pisoInterior)
                .HasColumnName("pisoInterior");

            builder.Property(e => e.pisoInteriorObs)
                .HasColumnName("pisoInteriorObs");

            builder.Property(e => e.techoCubierta)
                .HasColumnName("techoCubierta");

            builder.Property(e => e.techoCubiertaObs)
                .HasColumnName("techoCubiertaObs");

            builder.Property(e => e.ladosIzquierdoDerecho)
                .HasColumnName("ladosIzquierdoDerecho");

            builder.Property(e => e.ladosIzquierdoDerechoObs)
                .HasColumnName("ladosIzquierdoDerechoObs");

            builder.Property(e => e.paredFrontal)
                .HasColumnName("paredFrontal");

            builder.Property(e => e.paredFrontalObs)
                .HasColumnName("paredFrontalObs");

            builder.Property(e => e.areaCondensadorCompresor)
                .HasColumnName("areaCondensadorCompresor");

            builder.Property(e => e.areaCondensadorCompresorObs)
                .HasColumnName("areaCondensadorCompresorObs");

            builder.Property(e => e.areaEvaporador)
                .HasColumnName("areaEvaporador");

            builder.Property(e => e.areaEvaporadorObs)
                .HasColumnName("areaEvaporadorObs");

            builder.Property(e => e.areaBateria)
                .HasColumnName("areaBateria");

            builder.Property(e => e.areaBateriaObs)
                .HasColumnName("areaBateriaObs");

            builder.Property(e => e.cajaControlElectricoAutomatico)
                .HasColumnName("cajaControlElectricoAutomatico");

            builder.Property(e => e.cajaControlElectricoAutomaticoObs)
                .HasColumnName("cajaControlElectricoAutomaticoObs");

            builder.Property(e => e.cablesConexionElectrica)
                .HasColumnName("cablesConexionElectrica");

            builder.Property(e => e.cablesConexionElectricaObs)
                .HasColumnName("cablesConexionElectricaObs");

            builder.HasOne(f => f.condicionActivo)
               .WithMany()
               .HasForeignKey(f => f.idCondicionActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
