using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class codigoPostalConfiguration: IEntityTypeConfiguration<codigoPostal>
    {
        public void Configure(EntityTypeBuilder<codigoPostal> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idMunicipio)
                .HasColumnName("idMunicipio")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired();

            builder.HasOne(f => f.municipio)
                .WithMany()
                .HasForeignKey(f => f.idMunicipio);

        }
    }
}
