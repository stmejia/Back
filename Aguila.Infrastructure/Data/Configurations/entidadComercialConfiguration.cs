using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class entidadComercialConfiguration : IEntityTypeConfiguration<entidadComercial>
    {
        public void Configure(EntityTypeBuilder<entidadComercial> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.razonSocial)
                .HasColumnName("razonSocial")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.idDireccionFiscal)
                .HasColumnName("idDireccionFiscal");

            //builder.Property(e => e.tipo)
            //    .HasColumnName("tipo")
            //    .IsRequired()
            //    .HasMaxLength(1);

            builder.Property(e => e.nit)
                .HasColumnName("nit")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.tipoNit)
                .HasColumnName("tipoNit")
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.idCorporacion)
                .HasColumnName("idCorporacion");

            //builder.HasOne(e => e.corporacion)
            //   .WithMany()
            //   .HasForeignKey(e => e.idCorporacion)
            //   .OnDelete(DeleteBehavior.ClientSetNull)
            //   .HasConstraintName("FK_entidadComercial_corporacion");

        }
    }
}
