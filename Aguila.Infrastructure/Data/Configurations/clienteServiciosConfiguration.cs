using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class clienteServiciosConfiguration : IEntityTypeConfiguration<clienteServicios>
    {
        public void Configure(EntityTypeBuilder<clienteServicios> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idCliente)
                .HasColumnName("idCliente")
                .IsRequired();

            builder.Property(e => e.idServicio)
                .HasColumnName("idServicio")
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.vigenciaHasta)
                .HasColumnName("vigenciaHasta")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.cliente)
                .WithMany()
                .HasForeignKey(f => f.idCliente)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.servicio)
                .WithMany()
                .HasForeignKey(f => f.idServicio)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
