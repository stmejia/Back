using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class paisesConfiguration: IEntityTypeConfiguration<paises>
    {
        public void Configure(EntityTypeBuilder<paises> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.CodMoneda)
                .HasColumnName("codMoneda")
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.CodAlfa2)
                .HasColumnName("codAlfa2")
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.CodAlfa3)
                .HasColumnName("CodAlfa3")
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.CodNumerico)
                .HasColumnName("codNumerico")
                .IsRequired();

            builder.Property(e => e.Idioma)
                .HasColumnName("idioma")
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(e => e.FechaCreacion)
                .HasColumnName("fechaCreacion")
                .IsRequired()
                .HasColumnType("datetime");

        }
    }
}
