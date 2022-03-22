using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoMovimientosConfiguration : IEntityTypeConfiguration<activoMovimientos>
    {
        public void Configure(EntityTypeBuilder<activoMovimientos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .IsRequired();

            builder.Property(e => e.ubicacionId)
                .HasColumnName("ubicacionId");

            builder.Property(e => e.idRuta)
                 .HasColumnName("idRuta");

            builder.Property(e => e.idEstado)
                .HasColumnName("idEstado");

            builder.Property(e => e.idServicio)
                .HasColumnName("idServicio");

            builder.Property(e => e.idEstacionTrabajo)
                .HasColumnName("idEstacionTrabajo")
                .IsRequired();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado");

            builder.Property(e => e.idUsuario)
                .HasColumnName("idUsuario")
                .IsRequired();

            builder.Property(e => e.documento)
                .HasColumnName("documento");

            builder.Property(e => e.tipoDocumento)
                .HasColumnName("tipoDocumento");


            builder.Property(e => e.lugar)
                .HasColumnName("lugar")
                .HasMaxLength(30);

            builder.Property(e => e.cargado)
                .HasColumnName("cargado");

            builder.Property(e => e.observaciones)
                    .HasColumnName("observaciones")
                    .HasMaxLength(300);

            builder.Property(e => e.fecha)
                    .HasColumnName("fecha")
                    .HasMaxLength(300);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientos_activoOperaciones");

            builder.HasOne(f => f.estado)
                .WithMany()
                .HasForeignKey(f => f.idEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientos_estados");

            builder.HasOne(f => f.servicio)
                .WithMany()
                .HasForeignKey(f => f.idServicio)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientos_servicios");

            builder.HasOne(f => f.estacionTrabajo)
                .WithMany()
                .HasForeignKey(f => f.idEstacionTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK_activoMovimientos_estacionTrabajo");

            builder.HasOne(f => f.piloto)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientos_pilotos");

            builder.HasOne(f => f.usuario)
                .WithMany()
                .HasForeignKey(f => f.idUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientos_usuarios");

            builder.HasOne(f => f.ruta)
                 .WithMany()
                 .HasForeignKey(f => f.idRuta)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.ubicacion)
                 .WithMany()
                 .HasForeignKey(f => f.ubicacionId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
