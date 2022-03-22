using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoGeneralesConfiguration : IEntityTypeConfiguration<activoGenerales>
    {
        public void Configure(EntityTypeBuilder<activoGenerales> builder)
        {
            builder.HasKey(e => e.id);

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_activoGenerales_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
               .HasColumnName("codigo")
               .HasMaxLength(10)
               .IsRequired();

            builder.Property(e => e.fechaCompra)
              .HasColumnName("fechaCompra")
              .HasColumnType("datetime")
              .IsRequired();

            builder.Property(e => e.fechaBaja)
             .HasColumnName("fechaBaja")
             .HasColumnType("datetime");

            builder.Property(e => e.valorCompra)
                .HasColumnName("valorCompra")
                .HasColumnType("decimal(10,6)")
                .IsRequired();

            builder.Property(e => e.valorLibro)
               .HasColumnName("valorLibro")
               .HasColumnType("decimal(10,6)")
               .IsRequired();

            builder.Property(e => e.valorRescate)
                .HasColumnName("valorRescate")
                .HasColumnType("decimal(10,6)")
                .IsRequired();

            builder.Property(e => e.depreciacionAcumulada)
               .HasColumnName("depreciacionAcumulada")
               .HasColumnType("decimal(10,6)")
               .IsRequired();

            builder.Property(e => e.idDocumentoCompra)
                .HasColumnName("idDocumentoCompra")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(e => e.idTipoActivo)
                .HasColumnName("idTipoActivo")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.tituloPropiedad)
              .HasColumnName("tituloPropiedad")
              .HasMaxLength(25);

            builder.Property(e => e.polizaImportacion)
              .HasColumnName("polizaImportacion")
              .HasMaxLength(25);


            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            builder.HasOne(e => e.tipoActivo)
               .WithMany()
               .HasForeignKey(e => e.idTipoActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
