using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class controlContratistasConfiguration : IEntityTypeConfiguration<controlContratistas>
    {
        public void Configure(EntityTypeBuilder<controlContratistas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.identificacion)
                .HasColumnName("identificacion")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.empresa)
                .HasColumnName("empresa")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.vehiculo)
                .HasColumnName("vehiculo")
                .HasMaxLength(15);

            builder.Property(e => e.ingreso)
                .HasColumnName("ingreso")
                .HasColumnType("datetime");

            builder.Property(e => e.salida)
                .HasColumnName("salida")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime");

            builder.Property(e => e.empresaVisita)
              .HasColumnName("empresaVisita")
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(e => e.idUsuario)
             .HasColumnName("idUsuario");

            builder.Property(e => e.idEstacionTrabajo)
              .HasColumnName("idEstacionTrabajo")
              .IsRequired();

            builder.HasOne(f => f.usuario)
               .WithMany()
               .HasForeignKey(f => f.idUsuario);

            builder.HasOne(f => f.estacion)
               .WithMany()
               .HasForeignKey(f => f.idEstacionTrabajo);

            builder.HasOne<ImagenRecurso>(e => e.DPI)
                .WithOne()
                .HasForeignKey<controlContratistas>(e => e.idImagenRecursoDpi)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
