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
    public class controlVisitasConfiguration : IEntityTypeConfiguration<controlVisitas>
    {
        public void Configure(EntityTypeBuilder<controlVisitas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(e => e.identificacion)
              .HasColumnName("identificacion")
              .IsRequired();

            builder.Property(e => e.motivoVisita)
              .HasColumnName("motivoVisita")
              .IsRequired();

            builder.Property(e => e.areaVisita)
              .HasColumnName("areaVisita")
              .IsRequired();

            builder.Property(e => e.nombreQuienVisita)
              .HasColumnName("nombreQuienVisita")
              .IsRequired();

            builder.Property(e => e.vehiculo)
              .HasColumnName("vehiculo");

            builder.Property(e => e.ingreso)
              .HasColumnName("ingreso")
              .HasColumnType("datetime");

            builder.Property(e => e.salida)
              .HasColumnName("salida")
              .HasColumnType("datetime");

            builder.Property(e => e.idUsuario)
              .HasColumnName("idUsuario");

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            builder.Property(e => e.idEstacionTrabajo)
              .HasColumnName("idEstacionTrabajo")
              .IsRequired();

            builder.Property(e => e.empresaVisita)
              .HasColumnName("empresaVisita")
              .IsRequired();

            builder.HasOne(f => f.usuario)
               .WithMany()
               .HasForeignKey(f => f.idUsuario);

            builder.HasOne(f => f.estacion)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo);

            builder.HasOne<ImagenRecurso>(e => e.DPI)
                .WithOne()
                .HasForeignKey<controlVisitas>(e => e.idImagenRecursoDpi)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
