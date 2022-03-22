using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class SucursalesConfiguration : IEntityTypeConfiguration<Sucursales>
    {
        public void Configure(EntityTypeBuilder<Sucursales> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.Property(e => e.Activa).HasColumnName("activa");

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasColumnName("codigo")
                .HasMaxLength(10);

            builder.Property(e => e.Direccion)
                .HasColumnName("direccion")
                .HasMaxLength(100);

            builder.Property(e => e.EmpresaId).HasColumnName("empresa_ID");

            builder.Property(e => e.FchCreacion)
                .HasColumnName("fch_creacion")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(80);

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.EmpresaId);

                            
        }
    }
}
