using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class ModulosMnuConfiguration : IEntityTypeConfiguration<ModulosMnu>
    {
        public void Configure(EntityTypeBuilder<ModulosMnu> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.Property(e => e.Activo).HasColumnName("activo");

            builder.Property(e => e.Codigo).HasColumnName("codigo");

            builder.Property(e => e.Descrip)
                .IsRequired()
                .HasColumnName("descrip")
                .HasMaxLength(60);

            builder.Property(e => e.MenuIdPadre).HasColumnName("menu_id_padre");

            builder.Property(e => e.ModuloId).HasColumnName("modulo_id");

            builder.Property(e => e.RecursoId).HasColumnName("recurso_ID");

            builder.HasOne(d => d.MenuIdPadreNavigation)
                .WithMany(p => p.InverseMenuIdPadreNavigation)
                .HasForeignKey(d => d.MenuIdPadre)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Modulo)
                .WithMany(p => p.ModulosMnu)
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Recurso)
                .WithMany(p => p.ModulosMnu)
                .HasForeignKey(d => d.RecursoId);
        }
    }
}
