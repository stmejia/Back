using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class UsuariosRolesConfiguration : IEntityTypeConfiguration<UsuariosRoles>
    {
        public void Configure(EntityTypeBuilder<UsuariosRoles> builder)
        {
            builder.HasKey(e => new { e.usuario_id, e.rol_id });

            builder.Property(e => e.usuario_id).HasColumnName("usuario_Id");
            builder.Property(e => e.rol_id).HasColumnName("rol_Id");

            builder.HasOne(d => d.Usuario)
               .WithMany()
               .HasForeignKey(d => d.usuario_id)
               .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_UsuariosRoles_Usuarios");

            builder.HasOne(d => d.Rol)
               .WithMany()
               .HasForeignKey(d => d.rol_id)
               .OnDelete(DeleteBehavior.ClientSetNull);
               //.HasConstraintName("FK_UsuariosRoles_Roles");
        }
    }
}
