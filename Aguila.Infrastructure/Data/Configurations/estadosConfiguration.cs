using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class estadosConfiguration : IEntityTypeConfiguration<estados>
    {
        public void Configure(EntityTypeBuilder<estados> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique();
                //.HasName("IX_estados_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.tipo)
                .HasColumnName("tipo")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.numeroOrden)
                .HasColumnName("numeroOrden")
                .IsRequired();

            builder.Property(e => e.disponible)
                .HasColumnName("disponible")
                .IsRequired();

            builder.Property(e => e.evento)
                .HasColumnName("evento")
                .HasMaxLength(100);

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
