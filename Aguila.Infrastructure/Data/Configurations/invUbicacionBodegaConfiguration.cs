using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class invUbicacionBodegaConfiguration : IEntityTypeConfiguration<invUbicacionBodega>
    {
        public void Configure(EntityTypeBuilder<invUbicacionBodega> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idBodega)
                .HasColumnName("idBodega")
                .IsRequired();

            builder.Property(e => e.estante)
                .HasColumnName("estante")
                .IsRequired();

            builder.Property(e => e.pasillo)
                .HasColumnName("pasillo")
                .IsRequired();

            builder.Property(e => e.nivel)
                .HasColumnName("nivel")
                .IsRequired();

            builder.Property(e => e.lugar)
                .HasColumnName("lugar")
                .IsRequired();

            builder.Property(e => e.idProducto)
                .HasColumnName("idProducto")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.estacionTrabajo)
                .WithMany()
                .HasForeignKey(f => f.idBodega)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.producto)
                .WithMany()
                .HasForeignKey(f => f.idProducto)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
