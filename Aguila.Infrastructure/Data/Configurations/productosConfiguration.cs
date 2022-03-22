using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class productosConfiguration : IEntityTypeConfiguration<productos>
    {
        public void Configure(EntityTypeBuilder<productos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(45)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_productos_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.codigoQR)
                .HasColumnName("codigoQR")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.bienServicio)
                .HasColumnName("bienServicio")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.idsubCategoria)
                .HasColumnName("idsubCategoria")
                .IsRequired();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.idMedida)
                .HasColumnName("idMedida")
                .IsRequired();

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.HasOne(f => f.subCategoria)
                .WithMany()
                .HasForeignKey(f => f.idsubCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.empresa)
                .WithMany()
                .HasForeignKey(f => f.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.medida)
                .WithMany()
                .HasForeignKey(f => f.idMedida)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
