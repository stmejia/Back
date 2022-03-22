using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class asesoresConfiguration : IEntityTypeConfiguration<asesores>
    {
        public void Configure(EntityTypeBuilder<asesores> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_asesores_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado")
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
