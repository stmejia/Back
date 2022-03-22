using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tarifarioConfiguration : IEntityTypeConfiguration<tarifario>
    {
        public void Configure(EntityTypeBuilder<tarifario> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.tipoCarga)
                .HasColumnName("tipoCarga")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.tipoMovimiento)
                .HasColumnName("tipoMovimiento")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.segmento)
                .HasColumnName("segmento")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.idUbicacionOrigen)
                .HasColumnName("idUbicacionOrigen");

            builder.Property(e => e.idUbicacionDestino)
                .HasColumnName("idUbicacionDestino");

            builder.Property(e => e.idRuta)
                .HasColumnName("idRuta");

            builder.Property(e => e.idServicio)
                .HasColumnName("idServicio")
                .IsRequired();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.combustibleGls)
                .HasColumnName("combustibleGls")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.combustibleGls)
                .HasColumnName("combustibleGls")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.kmRecorridosCargado)
                .HasColumnName("kmRecorridosCargado")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.kmRecorridosVacio)
                .HasColumnName("kmRecorridosVacio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.esEspecializado)
                .HasColumnName("esEspecializado")
                .IsRequired();

            builder.Property(e => e.tipoViaje)
                .HasColumnName("tipoViaje")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.fechaVigencia)
                .HasColumnName("fechaVigencia")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.ubicacion)
                .WithMany()
                .HasForeignKey(e => e.idUbicacionOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.ubicacion)
                .WithMany()
                .HasForeignKey(e => e.idUbicacionDestino)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.servicio)
                .WithMany()
                .HasForeignKey(e => e.idServicio)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.ruta)
                .WithMany()
                .HasForeignKey(e => e.idRuta)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
