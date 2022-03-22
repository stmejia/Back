using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class equipoRemolqueConfiguration : IEntityTypeConfiguration<equipoRemolque>
    {
        public void Configure(EntityTypeBuilder<equipoRemolque> builder)
        {
            builder.HasKey(e => e.idActivo);

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo")
                .HasColumnType("int");

            builder.Property(e => e.idTipoEquipoRemolque)
                .HasColumnName("idTipoEquipoRemolque")
                .HasColumnType("int")
                .IsRequired();            

            builder.Property(e => e.tarjetaCirculacion)
                .HasColumnName("tarjetaCirculacion")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.noEjes)
                .HasColumnName("noEjes")          
                .IsRequired();

            builder.Property(e => e.tandemCorredizo)
               .HasColumnName("tandemCorredizo");

            builder.Property(e => e.chasisExtensible)
                .HasColumnName("chasisExtensible");

            builder.Property(e => e.tipoCuello)
                .HasColumnName("tipoCuello");

            builder.Property(e => e.acopleGenset)
               .HasColumnName("acopleGenset");

            builder.Property(e => e.acopleDolly)
                .HasColumnName("acopleDolly");

            builder.Property(e => e.capacidadCargaLB)
                .HasColumnName("capacidadCargaLB");

            builder.Property(e => e.medidaLB)
                .HasColumnName("medidaLB");

            builder.Property(e => e.medidaPlataforma)
                .HasColumnName("medidaPlataforma");

            builder.Property(e => e.placa)
                .HasColumnName("placa")
                .IsRequired();

            builder.Property(e => e.pechera)
                .HasColumnName("pechera");

            builder.Property(e => e.alturaContenedor)
                .HasColumnName("alturaContenedor");

            builder.Property(e => e.tipoContenedor)
                .HasColumnName("tipoContenedor");

            builder.Property(e => e.marcaUR)
               .HasColumnName("marcaUR");

            builder.Property(e => e.largoFurgon)
             .HasColumnName("largoFurgon");

            //builder.Property(e => e.rielesHorizontales)
            // .HasColumnName("rielesHorizontales");

            //builder.Property(e => e.rielesVerticales)
            // .HasColumnName("rielesVerticales");

            builder.Property(e => e.suspension)
             .HasColumnName("suspension");

            builder.Property(e => e.rieles)
             .HasColumnName("rieles");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.tipoEquipoRemolque)
               .WithMany()
               .HasForeignKey(f => f.idTipoEquipoRemolque)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<ImagenRecurso>(e => e.imagenTarjetaCirculacion)
            .WithOne()
            .HasForeignKey<equipoRemolque>(e => e.idImagenRecursoTarjetaCirculacion)
            .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
