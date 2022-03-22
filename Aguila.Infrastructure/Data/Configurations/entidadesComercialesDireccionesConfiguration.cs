using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class entidadesComercialesDireccionesConfiguration : IEntityTypeConfiguration<entidadesComercialesDirecciones>
    {
        public void Configure(EntityTypeBuilder<entidadesComercialesDirecciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEntidadComercial)
                .HasColumnName("idEntidadComercial")
                .IsRequired();

            builder.Property(e => e.idDireccion)
                .HasColumnName("idDireccion");
                

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.entidadComercial)
                .WithMany()
                .HasForeignKey(f => f.idEntidadComercial)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.direccion)
                .WithMany()
                .HasForeignKey(f => f.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
