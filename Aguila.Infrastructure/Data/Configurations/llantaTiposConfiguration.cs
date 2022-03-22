using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class llantaTiposConfiguration : IEntityTypeConfiguration<llantaTipos>
    {
        public void Configure(EntityTypeBuilder<llantaTipos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_llantaTipos_Codigo_Unico");//indica el indice unico para el campo codigo
            
            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(50);

            //builder.Property(e => e.proposito)
            //    .HasColumnName("proposito")
            //    .IsRequired();
        }
    }
}
