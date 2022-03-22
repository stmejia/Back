using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class ModulosConfiguration : IEntityTypeConfiguration<Modulos>
    {
        public void Configure(EntityTypeBuilder<Modulos> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Activo)
                .HasColumnName("activo");

            builder.Property(e => e.ModuMinVersion)
                .HasColumnName("modu_MinVersion")
                .HasMaxLength(20);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(30);

            builder.Property(e => e.path)
                .HasColumnName("path")
                .HasMaxLength(200);
        }
    }
}
