using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionTallerVehiculoConfiguration : IEntityTypeConfiguration<condicionTallerVehiculo>
    {
        public void Configure(EntityTypeBuilder<condicionTallerVehiculo> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .IsRequired();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado")
                .IsRequired();

            builder.Property(e => e.idUsuario)
                .HasColumnName("idUsuario")
                .IsRequired();

            builder.Property(e => e.idEstacionTrabajo)
                .HasColumnName("idEstacionTrabajo")
                .IsRequired();

            builder.Property(e => e.serie)
                .HasColumnName("serie")
                .IsRequired();

            builder.Property(e => e.numero)
                .HasColumnName("numero")
                .IsRequired();

            builder.Property(e => e.vidrios)
                .HasColumnName("vidrios");

            builder.Property(e => e.llantas)
                .HasColumnName("llantas");

            builder.Property(e => e.tanqueCombustible)
                .HasColumnName("tanqueCombustible");

            builder.Property(e => e.observaciones)
                .HasColumnName("observaciones");

            builder.Property(e => e.fechaAprobacion)
                .HasColumnName("fechaAprobacion")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaRechazo)
                .HasColumnName("fechaRechazo")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaIngreso)
                .HasColumnName("fechaIngreso")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaSalida)
                .HasColumnName("fechaSalida")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime");

            builder.HasOne(f => f.vehiculos)
               .WithMany()
               .HasForeignKey(f => f.idActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.empleados)
               .WithMany()
               .HasForeignKey(f => f.idEmpleado)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarios)
               .WithMany()
               .HasForeignKey(f => f.idUsuario)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.estacionesTrabajo)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
