using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class productosBusquedaConfiguration :IEntityTypeConfiguration<productosBusqueda>
    {
        public void Configure(EntityTypeBuilder<productosBusqueda> builder)
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
                .HasName("IX_productosBusqueda_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(e => e.idProducto)
                .HasColumnName("idProducto")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.producto)
                .WithMany()
                .HasForeignKey(f => f.idProducto)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
