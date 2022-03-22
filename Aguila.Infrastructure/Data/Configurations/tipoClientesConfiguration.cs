using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoClientesConfiguration : IEntityTypeConfiguration<tipoClientes>
    {
        public void Configure(EntityTypeBuilder<tipoClientes> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(5);

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoClientes_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.naviera)
                .HasColumnName("naviera")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
