using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class pilotosConfiguration : IEntityTypeConfiguration<pilotos>
    {
        public void Configure(EntityTypeBuilder<pilotos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.idTipoPilotos)
                .HasColumnName("idTipoPilotos")
                .IsRequired();

            builder.Property(e => e.idEmpleado)
                .HasColumnName("idEmpleado")
                .IsRequired();

            builder.Property(e => e.codigoPiloto)
                .HasColumnName("codigoPiloto")
                .IsRequired();

            builder.HasIndex(e => e.codigoPiloto)
                .IsUnique()
                .HasName("IX_pilotos_Codigo_Unico");//Indica que el código es unico

            builder.Property(e => e.fechaIngreso)
                .HasColumnName("fechaIngreso")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.HasOne(f => f.pilotoTipo)
                .WithMany()
                .HasForeignKey(f => f.idTipoPilotos)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
