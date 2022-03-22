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
    public class empleadosIngresosConfiguration : IEntityTypeConfiguration<empleadosIngresos>
    {
        public void Configure(EntityTypeBuilder<empleadosIngresos> builder) 
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado");

            builder.Property(e => e.cui)
                .HasColumnName("cui")
                .IsRequired();

            builder.Property(e => e.evento)
               .HasColumnName("evento")
               .IsRequired();

            builder.Property(e => e.fechaEvento)
               .HasColumnName("fechaEvento");

            builder.Property(e => e.vehiculo)
               .HasColumnName("vehiculo");

            builder.Property(e => e.idUsuario)
              .HasColumnName("idUsuario");

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion");

            builder.Property(e => e.idEstacionTrabajo)
             .HasColumnName("idEstacionTrabajo")
             .IsRequired();


            builder.HasOne(f => f.empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado);
                

            builder.HasOne(f => f.usuario)
              .WithMany()
              .HasForeignKey(f => f.idUsuario);

            builder.HasOne(f => f.estacion)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo);

        }
    }
}
