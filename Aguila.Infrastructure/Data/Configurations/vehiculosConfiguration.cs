using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class vehiculosConfiguration : IEntityTypeConfiguration<vehiculos>
    {
        public void Configure(EntityTypeBuilder<vehiculos> builder) {
            builder.HasKey(e => e.idActivo);

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo");
                //.IsRequired();

            builder.Property(e => e.motor)
               .HasColumnName("motor")
               .HasMaxLength(25);

            builder.Property(e => e.ejes)
               .HasColumnName("ejes");

            builder.Property(e => e.tarjetaCirculacion)
               .HasColumnName("tarjetaCirculacion")
               .HasMaxLength(20);

            builder.Property(e => e.placa)
               .HasColumnName("placa")
               .HasMaxLength(9);

            builder.Property(e => e.tamanoMotor)
              .HasColumnName("tamanoMotor");

            //builder.Property(e => e.toneladas)
            //  .HasColumnName("toneladas");


            //builder.Property(e => e.correlativo)
            //  .HasColumnName("correlativo")
            //.IsRequired();

            builder.Property(e => e.llantas)
                .HasColumnName("llantas")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion");

            builder.Property(e => e.distancia)
                .HasColumnName("distancia");

            builder.Property(e => e.potencia)
                .HasColumnName("potencia");

            builder.Property(e => e.tornamesaGraduable)
               .HasColumnName("tornamesaGraduable");

            builder.Property(e => e.tipoMovimiento)
              .HasColumnName("tipoMovimiento");

            builder.Property(e => e.dobleRemolque)
              .HasColumnName("dobleRemolque");

            builder.Property(e => e.aptoParaCentroAmerica)
              .HasColumnName("aptoParaCentroAmerica");

            builder.Property(e => e.capacidadCarga)
               .HasColumnName("capacidadCarga");

            builder.Property(e => e.carroceria)
               .HasColumnName("carroceria");

            builder.Property(e => e.tipoCarga)
               .HasColumnName("tipoCarga");

            builder.Property(e => e.medidaFurgon)
               .HasColumnName("medidaFurgon");

            builder.Property(e => e.tipoVehiculo)
               .HasColumnName("tipoVehiculo");

            //builder.Property(e => e.capacidadMontacarga)
            //   .HasColumnName("capacidadMontacarga");

            builder.Property(e => e.tipoMotor)
                .HasColumnName("tipoMotor");

            builder.Property(e => e.polizaSeguro)
                .HasColumnName("polizaSeguro")
                .HasMaxLength(30);

            //builder.Property(e => e.tipoMaquinaria)
            // .HasColumnName("tipoVehiculo");

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.tipoVehiculos)
               .WithMany()
               .HasForeignKey(f => f.idTipoVehiculo)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<ImagenRecurso>(e => e.imagenTarjetaCirculacion)
                .WithOne()
                .HasForeignKey<vehiculos>(e => e.idImagenRecursoTarjetaCirculacion)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
