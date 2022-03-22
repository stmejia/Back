using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoUbicacionesConfiguration : IEntityTypeConfiguration<activoUbicaciones>
    {
        public void Configure(EntityTypeBuilder<activoUbicaciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .IsRequired();

            builder.Property(e => e.idUbicacion)
                .HasColumnName("idUbicacion")
                .IsRequired();

            builder.Property(e => e.observaciones)
                .HasColumnName("observaciones")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.activoOperacion)
                .WithMany()
                .HasForeignKey(e => e.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.ubicacion)
                .WithMany()
                .HasForeignKey(e => e.idUbicacion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
