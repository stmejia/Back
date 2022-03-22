using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class medidasConfiguration : IEntityTypeConfiguration<medidas>
    {
        public void Configure(EntityTypeBuilder<medidas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(45)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_medidas_Codigo_Unico");

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
