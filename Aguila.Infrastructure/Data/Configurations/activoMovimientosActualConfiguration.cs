using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoMovimientosActualConfiguration : IEntityTypeConfiguration<activoMovimientosActual>
    {
        public void Configure(EntityTypeBuilder<activoMovimientosActual> builder)
        {
            builder.HasKey(e => e.idActivo);


            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .IsRequired();

            builder.Property(e => e.idEstado)
                .HasColumnName("idEstado")
                .IsRequired();

            builder.Property(e => e.idEstacionTrabajo)
                .HasColumnName("idEstacionTrabajo")
                .IsRequired();

            builder.Property(e => e.idServicio)
                .HasColumnName("idServicio");

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado");

            builder.Property(e => e.ubicacionId)
                .HasColumnName("ubicacionId");

            builder.Property(e => e.idRuta)
                .HasColumnName("idRuta");

            builder.Property(e => e.lugar)
                .HasColumnName("lugar");

            builder.Property(e => e.idUsuario)
                .HasColumnName("idUsuario")
                .IsRequired();

            builder.Property(e => e.documento)
                .HasColumnName("documento");

            builder.Property(e => e.tipoDocumento)
                .HasColumnName("tipoDocumento")
                .HasMaxLength(25);

            builder.Property(e => e.cargado)
                .HasColumnName("cargado");

            builder.Property(e => e.observaciones)
                    .HasColumnName("observaciones")
                    .HasMaxLength(300);

            builder.Property(e => e.fecha)
                .HasColumnName("fecha")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientosActual_activoOperaciones");

            builder.HasOne(f => f.estado)
                .WithMany()
                .HasForeignKey(f => f.idEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientosActual_estados");

            builder.HasOne(f => f.estacionTrabajo)
                .WithMany()
                .HasForeignKey(f => f.idEstacionTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_activoMovimientosActual_estacionesTrabajo");

            builder.HasOne(f => f.servicio)
                .WithMany()
                .HasForeignKey(f => f.idServicio)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientosActual_servicios");

            builder.HasOne(f => f.empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientosActual_pilotos");

            builder.HasOne(f => f.ruta)
                .WithMany()
                .HasForeignKey(f => f.idRuta)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientosActual_rutas");

            builder.HasOne(f => f.usuario)
                .WithMany()
                .HasForeignKey(f => f.idUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //    .HasConstraintName("FK_activoMovimientosActual_usuarios");
        }
    }
}
