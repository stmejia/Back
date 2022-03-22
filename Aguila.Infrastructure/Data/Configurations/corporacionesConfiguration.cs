using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class corporacionesConfiguration : IEntityTypeConfiguration<corporaciones>
    {
        public void Configure(EntityTypeBuilder<corporaciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(15);

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_corporaciones_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(50);

            builder.Property(e => e.propio)
                .IsRequired()
                .HasColumnName("propio");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
