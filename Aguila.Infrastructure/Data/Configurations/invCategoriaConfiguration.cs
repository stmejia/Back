using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class invCategoriaConfiguration : IEntityTypeConfiguration<invCategoria>
    {
        public void Configure(EntityTypeBuilder<invCategoria> builder)
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
                .HasName("IX_invCategoria_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
