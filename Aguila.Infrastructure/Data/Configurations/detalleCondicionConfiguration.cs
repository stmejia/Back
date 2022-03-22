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
    public class detalleCondicionConfiguration : IEntityTypeConfiguration<detalleCondicion>
    {
        public void Configure(EntityTypeBuilder<detalleCondicion> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idUsuario)
                .HasColumnName("idUsuario")
                .IsRequired();

            builder.Property(e => e.idUsuarioAutoriza)
                .HasColumnName("idUsuarioAutoriza")
                .IsRequired();

            builder.Property(e => e.idCondicion)
                .HasColumnName("idCondicion")
                .IsRequired();

            builder.Property(e => e.idReparacion)
                .HasColumnName("idReparacion")
                .IsRequired();

            builder.Property(e => e.cantidad)
                .HasColumnName("cantidad");

            builder.Property(e => e.aprobado)
                .HasColumnName("aprobado");

            builder.Property(e => e.nombreAutoriza)
                .HasColumnName("nombreAutoriza");

            builder.Property(e => e.observaciones)
                .HasColumnName("observaciones");

            builder.Property(e => e.fechaAprobacion)
                .HasColumnName("fechaAprobacion")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaEstimadoReparacion)
                .HasColumnName("fechaEstimadoReparacion")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaFinalizacionRep)
                .HasColumnName("fechaFinalizacionRep")
                .HasColumnType("datetime");
            
            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime");

            builder.HasOne(f => f.usuarios)
                .WithMany()
                .HasForeignKey(f => f.idUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.usuarioAutoriza)
                .WithMany()
                .HasForeignKey(f => f.idUsuarioAutoriza)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.condicionTallerVehiculo)
                .WithMany()
                .HasForeignKey(f => f.idCondicion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.reparaciones)
                .WithMany()
                .HasForeignKey(f => f.idReparacion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
