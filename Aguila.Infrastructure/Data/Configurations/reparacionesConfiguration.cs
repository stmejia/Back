using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
   public class reparacionesConfiguration : IEntityTypeConfiguration<reparaciones>
   {
        public void Configure(EntityTypeBuilder<reparaciones> builder) {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
               .HasColumnName("codigo")
               .HasMaxLength(5)
               .IsRequired();

            builder.Property(e => e.idEmpresa)
               .HasColumnName("idEmpresa")
               .IsRequired();

            builder.Property(e => e.nombre)
             .HasColumnName("nombre")
             .HasMaxLength(25)
             .IsRequired();

            builder.Property(e => e.descripcion)
              .HasColumnName("descripcion")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(e => e.horasHombre)
              .HasColumnName("horasHombre")
              .HasColumnType("decimal(10,2)");

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            builder.Property(e => e.idCategoria)
              .HasColumnName("idCategoria")
              .HasColumnType("int");

            builder.Property(e => e.idTipoReparacion)
              .HasColumnName("idTipoReparacion")
              .HasColumnType("int");

            builder.HasOne(e => e.tipo)
                .WithMany()
                .HasForeignKey(e => e.idTipoReparacion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.categoria)
                .WithMany()
                .HasForeignKey(e => e.idCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);



        }
   }
}
