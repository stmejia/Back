using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class AsigUsuariosEstacionesTrabajoConfiguration : IEntityTypeConfiguration<AsigUsuariosEstacionesTrabajo>
    {
        public void Configure(EntityTypeBuilder<AsigUsuariosEstacionesTrabajo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EstacionTrabajoId).HasColumnName("estacionTrabajo_ID");
                        
            builder.Property(e => e.UsuarioId).HasColumnName("usuario_ID");

            builder.HasOne(d => d.EstacionTrabajo)
                .WithMany()
                .HasForeignKey(d => d.EstacionTrabajoId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            //builder.HasOne(d => d.Usuario)
            //    .WithMany()
            //    .HasForeignKey(d => d.UsuarioId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
