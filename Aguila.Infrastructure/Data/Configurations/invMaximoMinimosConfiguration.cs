using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class invMaximoMinimosConfiguration : IEntityTypeConfiguration<invProductoBodega>
    {
        public void Configure(EntityTypeBuilder<invProductoBodega> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idProducto)
                .HasColumnName("idProducto")
                .IsRequired();

            builder.Property(e => e.idBodega)
                .HasColumnName("idBodega")
                .IsRequired();

            builder.Property(e => e.maximo)
                .HasColumnName("maximo")
                .IsRequired();

            builder.Property(e => e.minimo)
                .HasColumnName("minimo")
                .IsRequired();

            builder.HasOne(f => f.estacionesTrabajo)
                .WithMany()
                .HasForeignKey(f => f.idBodega)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.producto)
                .WithMany()
                .HasForeignKey(f => f.idProducto)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
