using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
   public class tipoProveedoresConfiguration : IEntityTypeConfiguration<tipoProveedores>
    {
        public void Configure(EntityTypeBuilder<tipoProveedores> builder) {

            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
               .HasColumnName("codigo")
               .HasMaxLength(5)
               .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoProveedores_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
             .HasColumnName("descripcion")
             .HasMaxLength(45)
             .IsRequired();

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");
        }
    }
}
