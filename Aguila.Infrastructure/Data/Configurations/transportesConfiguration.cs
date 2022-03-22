using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class transportesConfiguration : IEntityTypeConfiguration<transportes>
    {
        public void Configure(EntityTypeBuilder<transportes> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_transportes_Codigo_Unico");

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.idProveedor)
                .HasColumnName("idProveedor")
                .IsRequired();

            builder.Property(e => e.propio)
                .HasColumnName("propio")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.proveedor)
                .WithMany()
                .HasForeignKey(f => f.idProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("fk_transportes_proveedores");

        }
    }
}
