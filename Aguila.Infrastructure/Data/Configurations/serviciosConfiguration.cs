using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class serviciosConfiguration : IEntityTypeConfiguration<servicios>
    {
        public void Configure(EntityTypeBuilder<servicios> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(15)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
               .IsUnique()
               .HasName("IX_servicios_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(f => f.empresa)
                .WithMany()
                .HasForeignKey(f => f.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
