using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class AsigUsuariosRecursosAtributosConfiguration : IEntityTypeConfiguration<AsigUsuariosRecursosAtributos>
    {
        public void Configure(EntityTypeBuilder<AsigUsuariosRecursosAtributos> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EstacionTrabajoId)
                .HasColumnName("estacionTrabajo_id");
                        
            builder.Property(e => e.ModuloId)
                .HasColumnName("modulo_id");

            builder.Property(e => e.RecursoAtributosId)
                .HasColumnName("recursoAtributos_id");

            builder.Property(e => e.UsuarioId)
                .HasColumnName("usuario_id");

            builder.HasOne(d => d.EstacionTrabajo)
                .WithMany()
                .HasForeignKey(d => d.EstacionTrabajoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Modulo)
                .WithMany()
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.RecursoAtributos)
                .WithMany()
                .HasForeignKey(d => d.RecursoAtributosId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
