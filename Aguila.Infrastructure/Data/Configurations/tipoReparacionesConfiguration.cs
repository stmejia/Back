using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoReparacionesConfiguration : IEntityTypeConfiguration<tipoReparaciones>
    {
        public void Configure(EntityTypeBuilder<tipoReparaciones> builder)
        {
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
              .HasMaxLength(30)
              .IsRequired();

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK_tipoReparaciones_Empresas");
        }
    }
}
