using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoEstadosConfiguration : IEntityTypeConfiguration<activoEstados>
    {
        public void Configure(EntityTypeBuilder<activoEstados> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .IsRequired();

            builder.Property(e => e.idEstado)
                .HasColumnName("idEstado")
                .IsRequired();

            builder.Property(e => e.observacion)
                .HasColumnName("observacion")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.activoOperacion)
                .WithMany()
                .HasForeignKey(e => e.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.estado)
                .WithMany()
                .HasForeignKey(e => e.idEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
