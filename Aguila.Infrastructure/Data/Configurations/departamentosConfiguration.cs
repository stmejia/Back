using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class departamentosConfiguration : IEntityTypeConfiguration<departamentos>
    {
        public void Configure(EntityTypeBuilder<departamentos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idPais)
                .HasColumnName("idPais")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.pais)
                .WithMany()
                .HasForeignKey(f => f.idPais)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
