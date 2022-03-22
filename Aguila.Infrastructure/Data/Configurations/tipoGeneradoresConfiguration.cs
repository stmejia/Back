using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoGeneradoresConfiguration : IEntityTypeConfiguration<tipoGeneradores>
    {
        public void Configure(EntityTypeBuilder<tipoGeneradores> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEmpresa)
              .HasColumnName("idEmpresa")
              .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoGeneradores_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.estructuraCoc)
                .HasColumnName("estructuraCoc")
              .IsRequired()
               .HasMaxLength(250);

            builder.Property(e => e.prefijo)
                .HasColumnName("prefijo")
                .IsRequired()
                .HasMaxLength(8);
        }
    }
}
