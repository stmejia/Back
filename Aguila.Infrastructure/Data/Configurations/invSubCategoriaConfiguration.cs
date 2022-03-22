using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class invSubCategoriaConfiguration : IEntityTypeConfiguration<invSubCategoria>
    {
        public void Configure(EntityTypeBuilder<invSubCategoria> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idInvCategoria)
                .HasColumnName("idInvCategoria")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(45)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_invSubCategoria_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(45)
                .IsRequired();

            builder.HasOne(f => f.invCategoria)
                .WithMany()
                .HasForeignKey(f => f.idInvCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
