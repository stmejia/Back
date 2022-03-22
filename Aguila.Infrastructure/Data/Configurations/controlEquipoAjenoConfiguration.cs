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
    public class controlEquipoAjenoConfiguration : IEntityTypeConfiguration<controlEquipoAjeno>
    {
        public void Configure(EntityTypeBuilder<controlEquipoAjeno> builder) {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombrePiloto)
             .HasColumnName("nombrePiloto")
             .IsRequired();

            builder.Property(e => e.placaCabezal)
             .HasColumnName("placaCabezal");

            builder.Property(e => e.codigoEquipo)
             .HasColumnName("codigoEquipo");

            builder.Property(e => e.tipoEquipo)
             .HasColumnName("tipoEquipo");

            builder.Property(e => e.codigoChasis)
             .HasColumnName("codigoChasis");

            builder.Property(e => e.codigoGenerador)
             .HasColumnName("codigoGenerador");

            builder.Property(e => e.ingreso)
             .HasColumnName("ingreso");

            builder.Property(e => e.salida)
             .HasColumnName("salida");

            builder.Property(e => e.cargado)
             .HasColumnName("cargado");

            builder.Property(e => e.origen)
             .HasColumnName("origen");

            builder.Property(e => e.destino)
             .HasColumnName("destino");

            builder.Property(e => e.marchamo)
             .HasColumnName("marchamo");

            builder.Property(e => e.atc)
             .HasColumnName("atc");

            builder.Property(e => e.idUsuario)
             .HasColumnName("idUsuario");

            builder.Property(e => e.idUsuario)
              .HasColumnName("idUsuario");

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion");

            builder.Property(e => e.idEstacionTrabajo)
              .HasColumnName("idEstacionTrabajo")
              .IsRequired();

            builder.Property(e => e.empresa)
              .HasColumnName("empresa");

            builder.HasOne(f => f.usuario)
               .WithMany()
               .HasForeignKey(f => f.idUsuario);

            builder.HasOne(f => f.estacion)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo);

        }
    }
}
