using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoMecanicosConfiguration : IEntityTypeConfiguration<tipoMecanicos>
    {
        public void Configure(EntityTypeBuilder<tipoMecanicos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoMecanicos_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.especialidad)
                .HasColumnName("especialidad")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
