using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class pilotosTiposConfiguration : IEntityTypeConfiguration<pilotosTipos>
    {
        public void Configure(EntityTypeBuilder<pilotosTipos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasIndex(e => e.codigo)                
                .IsUnique()
                .HasName("IX_pilotosTipos_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
