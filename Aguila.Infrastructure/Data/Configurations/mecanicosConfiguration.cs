using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class mecanicosConfiguration : IEntityTypeConfiguration<mecanicos>
    {
        public void Configure(EntityTypeBuilder<mecanicos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_mecanicos_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.idTipoMecanico)
                .HasColumnName("idTipoMecanico")
                .IsRequired();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado")
                .IsRequired();

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(e => e.tipoMecanico)
                .WithMany()
                .HasForeignKey(e => e.idTipoMecanico)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.empleado)
                .WithMany()
                .HasForeignKey(e => e.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
