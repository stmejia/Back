using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class UsuariosConfiguration : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.Username)
               .IsRequired()
               .HasColumnName("username")
               .HasMaxLength(80)
               .IsFixedLength();                       

            builder.Property(e => e.Activo).HasColumnName("activo");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(80);

            builder.Property(e => e.EstacionTrabajoId).HasColumnName("estacionTrabajo_id");

            builder.Property(e => e.FchCreacion)
                .HasColumnName("fchCreacion")
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder.Property(e => e.fchNacimiento )
                .HasColumnName("fchNacimiento")
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder.Property(e => e.FchPassword)
                .HasColumnName("fchPassword")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.ModuloId).HasColumnName("modulo_id");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(80);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(120);

            builder.Property(e => e.SucursalId).HasColumnName("sucursal_id");

            builder.Property(e => e.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasMaxLength(80);

            builder.HasOne<ImagenRecurso>(e => e.ImagenPerfil)
                .WithOne(f => f.Usuario)
                .HasForeignKey<Usuarios>(f => f.ImagenRecurso_IdPerfil);

            builder.HasMany<AsigUsuariosEstacionesTrabajo>(e => e.EstacionesTrabajoAsignadas)
                .WithOne()
                .HasForeignKey(e => e.UsuarioId);

        }
    }
}
