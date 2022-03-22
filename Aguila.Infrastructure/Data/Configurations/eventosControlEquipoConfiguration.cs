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
    public class eventosControlEquipoConfiguration : IEntityTypeConfiguration<eventosControlEquipo>
    {
        public void Configure(EntityTypeBuilder<eventosControlEquipo> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
               .IsRequired();

            builder.Property(e => e.idUsuarioCreacion)
                .HasColumnName("idUsuarioCreacion")
              .IsRequired();

            builder.Property(e => e.idEstacionTrabajo)
               .HasColumnName("idEstacionTrabajo")
             .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
              .IsRequired();

            builder.Property(e => e.descripcionEvento)
                .HasColumnName("descripcionEvento")
              .IsRequired();

            builder.Property(e => e.bitacoraObservaciones)
                .HasColumnName("bitacoraObservaciones")
              .IsRequired();

            builder.Property(e => e.fechaRevisado)
                .HasColumnName("fechaRevisado");

            builder.Property(e => e.idUsuarioRevisa)
                .HasColumnName("idUsuarioRevisa");


            builder.Property(e => e.fechaResuelto)
                .HasColumnName("fechaResuelto");

            builder.Property(e => e.idUsuarioResuelve)
               .HasColumnName("idUsuarioResuelve");

            builder.Property(e => e.fechaAnulado)
               .HasColumnName("fechaAnulado");

           

            builder.HasOne(f => f.activoOperacion)
               .WithMany()
               .HasForeignKey(f => f.idActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarioCreacion)
               .WithMany()
               .HasForeignKey(f => f.idUsuarioCreacion)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.estacionTrabajo)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarioRevisa)
              .WithMany()
              .HasForeignKey(f => f.idUsuarioRevisa)
              .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarioResuelve)
              .WithMany()
              .HasForeignKey(f => f.idUsuarioResuelve)
              .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarioAnula)
              .WithMany()
              .HasForeignKey(f => f.idUsuarioAnula)
              .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
