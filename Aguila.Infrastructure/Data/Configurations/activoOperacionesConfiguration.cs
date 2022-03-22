using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class activoOperacionesConfiguration : IEntityTypeConfiguration<activoOperaciones>
    {
        public void Configure(EntityTypeBuilder<activoOperaciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique();
                //.HasName("IX_activoOperaciones_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.categoria)//Cambiar char por varchar
                .HasColumnName("categoria")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.color)
                .HasColumnName("color")
                .HasMaxLength(25);

            builder.Property(e => e.marca)
                .HasColumnName("marca")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.vin)
                .HasColumnName("vin")
                .HasMaxLength(25);

            builder.Property(e => e.correlativo)
                .HasColumnName("correlativo")
                .IsRequired();

            builder.Property(e => e.serie)
                .HasColumnName("serie")
                .HasMaxLength(25);

            builder.Property(e => e.modeloAnio)
                .HasColumnName("modeloAnio");

            builder.Property(e => e.idActivoGenerales)
                .HasColumnName("idActivoGenerales");
               // .IsRequired();

            builder.Property(e => e.idTransporte)
                .HasColumnName("idTransporte")
                .IsRequired();

            builder.Property(e => e.coc)
                .HasColumnName("coc");

            builder.Property(e => e.idImagenRecursoFotos)
               .HasColumnName("idImagenRecursoFotos");

            builder.HasOne(e => e.transporte)
                .WithMany()
                .HasForeignKey(e => e.idTransporte)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.movimientoActual)
                .WithOne(e => e.activoOperacion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(f => f.Fotos)
            //    .WithMany()
            //    .HasForeignKey(f => f.idImagenRecursoFotos)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<ImagenRecurso>(e => e.Fotos)
                .WithOne()
                .HasForeignKey<activoOperaciones>(e => e.idImagenRecursoFotos)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
