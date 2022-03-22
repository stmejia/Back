using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class EmpresasConfiguration : IEntityTypeConfiguration<Empresas>
    {
        public void Configure(EntityTypeBuilder<Empresas> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                 .ValueGeneratedOnAdd();
           
            builder.Property(e => e.Abreviatura)
                .IsRequired()
                .HasColumnName("abreviatura")
                .HasMaxLength(20);

            builder.HasIndex(e => e.Codigo).IsUnique();
            builder.HasIndex(e => e.Abreviatura).IsUnique();

            builder.Property(e => e.Activ).HasColumnName("activ");

            builder.Property(e => e.Aleas)
                .IsRequired()
                .HasColumnName("aleas")
                .HasMaxLength(20);

            builder.Property(e => e.Codigo).HasColumnName("codigo");

            builder.Property(e => e.Departamento)
                .HasColumnName("departamento")
                .HasMaxLength(30);

            builder.Property(e => e.Direccion)
                .IsRequired()
                .HasColumnName("direccion")
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(80);

            builder.Property(e => e.FchCreacion)
                .HasColumnName("fchCreacion")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.esEmpleador)
               .HasColumnName("esEmpleador");

            builder.Property(e => e.Municipio)
                .HasColumnName("municipio")
                .HasMaxLength(30);

            builder.Property(e => e.Nit)
                .IsRequired()
                .HasColumnName("nit")
                .HasMaxLength(20);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Pais)
                .HasColumnName("pais")
                .HasMaxLength(20);

            builder.Property(e => e.Telefono)
                .HasMaxLength(50);

            builder.Property(e => e.WebPage)
                .HasColumnName("webPage")
                .HasMaxLength(80);

            builder.HasOne<ImagenRecurso>(e => e.ImagenLogo)
                .WithOne()
                .HasForeignKey<Empresas>(e => e.ImagenRecurso_IdLogo)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
