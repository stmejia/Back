using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class listasConfiguration : IEntityTypeConfiguration<listas>
    {
        public void Configure(EntityTypeBuilder<listas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.valor)
               .HasColumnName("valor")
               .HasMaxLength(25)
               .IsRequired();

            builder.Property(e => e.descripcion)
             .HasColumnName("descripcion")
             .HasMaxLength(75)
             .IsRequired();

            builder.Property(e => e.idEmpresa)
               .HasColumnName("idEmpresa")
               .IsRequired();

            builder.Property(e => e.idTipoLista)
               .HasColumnName("idTipoLista")
               .IsRequired();

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            builder.HasOne(d => d.empresa)
                .WithMany()
                .HasForeignKey(d => d.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.tipos)
                .WithMany()
                .HasForeignKey(d => d.idTipoLista)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
