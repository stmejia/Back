using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class direccionesConfiguration : IEntityTypeConfiguration<direcciones>
    {
        public void Configure(EntityTypeBuilder<direcciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idMunicipio)
                .HasColumnName("idMunicipio")
                .IsRequired();

            builder.Property(e => e.colonia)
                .HasColumnName("colonia")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.zona)
                .HasColumnName("zona")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.codigoPostal)
                .HasColumnName("codigoPostal")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.direccion)
                .HasColumnName("direccion")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime");

            builder.HasOne(f => f.municipio)
                .WithMany()
                .HasForeignKey(f => f.idMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
