using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class UsuariosRecursosConfiguration : IEntityTypeConfiguration<UsuariosRecursos>
    {
        public void Configure(EntityTypeBuilder<UsuariosRecursos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.estacionTrabajo_id)
                .HasColumnName("estacionTrabajo")
                .IsRequired();

            builder.Property(e => e.recurso_id)
                .HasColumnName("recurso")
                .IsRequired();

            builder.Property(e => e.usuario_id)
                .HasColumnName("usuario")
                .IsRequired();

            builder.Property(e => e.opcionesAsignadas)
                .HasColumnName("opcionesAsignadas")
                .HasMaxLength(500);

            builder.HasOne(d => d.Estacion)
                .WithMany()
                .HasForeignKey(d => d.estacionTrabajo_id)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_UsuariosRecursos_EstacionesTrabajo");

            builder.HasOne(d => d.Recurso)
                .WithMany()
                .HasForeignKey(d => d.recurso_id)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_UsuariosRecursos_Recursos");

            builder.HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK_UsuariosRecursos_Usuarios");


        }
    }
}
