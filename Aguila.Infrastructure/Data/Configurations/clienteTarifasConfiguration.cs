using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class clienteTarifasConfiguration : IEntityTypeConfiguration<clienteTarifas>
    {
        public void Configure(EntityTypeBuilder<clienteTarifas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idCliente)
                .HasColumnName("idCliente")
                .IsRequired();

            builder.Property(e => e.idTarifa)
                .HasColumnName("idTarifa")
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.vigenciaHasta)
                .HasColumnName("vigenciaHasta")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.activa)
                .HasColumnName("activa")
                .HasColumnType("bool")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.cliente)
                .WithMany()
                .HasForeignKey(e => e.idCliente)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.tarifa)
                .WithMany()
                .HasForeignKey(e => e.idTarifa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
