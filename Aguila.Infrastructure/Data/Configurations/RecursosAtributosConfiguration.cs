using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
   public class RecursosAtributosConfiguration : IEntityTypeConfiguration<RecursosAtributos>
    {

        public void Configure(EntityTypeBuilder<RecursosAtributos> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo).HasColumnName("codigo");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(50);

            builder.Property(e => e.RecursoId).HasColumnName("recurso_ID");

            builder.HasOne(d => d.Recurso)
                .WithMany(p => p.RecursosAtributos)
                .HasForeignKey(d => d.RecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
