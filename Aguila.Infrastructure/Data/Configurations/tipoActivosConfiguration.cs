using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoActivosConfiguration : IEntityTypeConfiguration<tipoActivos>
    {
        public void Configure(EntityTypeBuilder<tipoActivos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
               .HasColumnName("codigo")
               .HasMaxLength(45)
               .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoActivos_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombre)
             .HasColumnName("nombre")
             .HasMaxLength(45)
             .IsRequired();

            builder.Property(e => e.area)
             .HasColumnName("area")
             .HasMaxLength(45)
             .IsRequired();

            builder.Property(e => e.operaciones)
            .HasColumnName("operaciones")
            .HasColumnType("bit");
            //.IsRequired();

            builder.Property(e => e.idCuenta)
            .HasColumnName("idCuenta")
            .HasColumnType("int");

            builder.Property(e => e.porcentajeDepreciacionAnual)
            .HasColumnName("porcentajeDepreciacionAnual")
            .HasColumnType("decimal(3,2)")
            .IsRequired();

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

        }

    }
}
