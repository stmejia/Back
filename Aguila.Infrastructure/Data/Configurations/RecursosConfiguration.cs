using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class RecursosConfiguration : IEntityTypeConfiguration<Recursos>
    {
        public void Configure(EntityTypeBuilder<Recursos> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Activo)
                .HasColumnName("activo");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(50);

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasColumnName("tipo")
                .HasMaxLength(20);

            builder.Property(e => e.opciones)
                .IsRequired()
                .HasColumnName("opciones")
                .HasMaxLength(500);

            builder.Property(e => e.Controlador)
                .HasColumnName("Controlador")
                .HasMaxLength(50);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion");
        }
    }
}
