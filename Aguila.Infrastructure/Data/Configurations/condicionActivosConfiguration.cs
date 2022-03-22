using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionActivosConfiguration : IEntityTypeConfiguration<condicionActivos>
    {
        public void Configure(EntityTypeBuilder<condicionActivos> builder) {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.tipoCondicion)
                .HasColumnName("tipoCondicion")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.idActivo)
               .HasColumnName("idActivo")
               .IsRequired();

            builder.Property(e => e.idEstacionTrabajo)
               .HasColumnName("idEstacionTrabajo")
               .IsRequired();

            builder.Property(e => e.idEmpleado)
               .HasColumnName("idEmpleado")
               .IsRequired();

            builder.Property(e => e.idReparacion)
               .HasColumnName("idReparacion");

            builder.Property(e => e.idEstado)
                .HasColumnName("idEstado");
                //.IsRequired();

            builder.Property(e => e.idImagenRecursoFirma)
               .HasColumnName("idImagenRecursoFirma");

            builder.Property(e => e.idImagenRecursoFotos)
               .HasColumnName("idImagenRecursoFotos");

            builder.Property(e => e.ubicacionIdEntrega)
               .HasColumnName("ubicacionIdEntrega")
               .HasMaxLength(50);

            builder.Property(e => e.idUsuario)
                   .HasColumnName("idUsuario")
                   .IsRequired();

            builder.Property(e => e.movimiento)
                   .HasColumnName("movimiento")
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Property(e => e.cargado)
                   .HasColumnName("cargado")
                   .IsRequired();

            builder.Property(e => e.serie)
                    .HasColumnName("serie")
                    .HasMaxLength(15);
                    //.IsRequired();

            builder.Property(e => e.numero)
                    .HasColumnName("numero");
            //.IsRequired();

            builder.Property(e => e.inspecVeriOrden)
                    .HasColumnName("inspecVeriOrden")
                    .IsRequired();

            builder.Property(e => e.observaciones)
                    .HasColumnName("observaciones")
                    .HasMaxLength(300);

            builder.Property(e => e.irregularidadesObserv)
                    .HasColumnName("irregularidadesObserv")
                    .HasMaxLength(200);

            builder.Property(e => e.daniosObserv)
                    .HasColumnName("daniosObserv")
                    .HasMaxLength(1000);

            builder.Property(e => e.fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

            builder.Property(e => e.fechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasColumnType("datetime");

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_condicionActivos_activoOperaciones");

            builder.HasOne(f => f.estacionTrabajo)
                .WithMany()
                .HasForeignKey(f => f.idEstacionTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_condicionActivos_estacionesTrabajo");

            builder.HasOne(f => f.empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_condicionActivos_empleados");

            builder.HasOne(f => f.reparacion)
                .WithMany()
                .HasForeignKey(f => f.idReparacion)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("FK_condicionActivos_reparaciones");                      

            builder.HasOne<ImagenRecurso>(f => f.ImagenFirmaPiloto)
                .WithOne()
                .HasForeignKey<condicionActivos>(f => f.idImagenRecursoFirma)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne<ImagenRecurso>(f => f.Fotos)
                .WithOne()
                .HasForeignKey<condicionActivos>(f => f.idImagenRecursoFotos)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(f => f.usuario)
                .WithMany()
                .HasForeignKey(f => f.idUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(f => f.ubicacionEntrega)
               .WithMany()
               .HasForeignKey(f => f.ubicacionIdEntrega)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.estado)
               .WithMany()
               .HasForeignKey(f => f.idEstado)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
