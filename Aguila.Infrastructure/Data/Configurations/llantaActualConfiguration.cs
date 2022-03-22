using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class llantaActualConfiguration : IEntityTypeConfiguration<llantaActual>
    {
        public void Configure(EntityTypeBuilder<llantaActual> builder)
        {
            builder.HasKey(e => e.idLlanta);

            builder.Property(e => e.idLlanta)
                .HasColumnName("idLlanta")
                .IsRequired();

            builder.Property(e => e.idLlantaTipo)
                .HasColumnName("idLlantaTipo")
                .IsRequired();

            builder.Property(e => e.idActivoOperaciones)
                .HasColumnName("idActivoOperaciones")
                .IsRequired();

            builder.Property(e => e.idEstado)
                .HasColumnName("idEstado")
                .IsRequired();

            builder.Property(e => e.ubicacionId)
                .HasColumnName("ubicacionId")
                .IsRequired();

            builder.Property(e => e.documentoEstado)
                .HasColumnName("documentoEstado")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.documentoUbicacion)
                .HasColumnName("documentoUbicacion")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.observacion)
                .HasColumnName("observacion")
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(e => e.posicion)
                .HasColumnName("posicion")
                .IsRequired();

            builder.Property(e => e.profundidadIzquierda)
                .HasColumnName("profundidadIzquierda")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.profundidadCentro)
                .HasColumnName("profundidadCentro")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.profundidadDerecho)
                .HasColumnName("profundidadDerecho")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.reencauche)
                .HasColumnName("reencauche")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.proposito)
                .HasColumnName("proposito")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.fechaEstado)
                .HasColumnName("fechaEstado")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaUbicacion)
                .HasColumnName("fechaUbicacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.llanta)
                .WithMany()
                .HasForeignKey(e => e.idLlanta)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.llantaTipo)
                .WithMany()
                .HasForeignKey(e => e.idLlantaTipo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.activoOperacion)
                .WithMany()
                .HasForeignKey(e => e.idActivoOperaciones)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.estado)
                .WithMany()
                .HasForeignKey(e => e.idEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.ubicacion)
                .WithMany()
                .HasForeignKey(e => e.ubicacionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
