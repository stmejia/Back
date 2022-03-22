using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class AsigUsuariosModulosConfiguration : IEntityTypeConfiguration<AsigUsuariosModulos>
    {
        public void Configure(EntityTypeBuilder<AsigUsuariosModulos> builder)
        {
            builder.HasKey(e => new { e.UsuarioId, e.ModuloId });

            builder.Property(e => e.ModuloId).HasColumnName("modulo_ID");

            builder.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            builder.HasOne(d => d.Modulo)
                .WithMany()
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}
