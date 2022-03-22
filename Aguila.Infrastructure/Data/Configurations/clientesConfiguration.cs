using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Aguila.Infrastructure.Data.Configurations
{
    public class clientesConfiguration : IEntityTypeConfiguration<clientes>
    {
        public void Configure(EntityTypeBuilder<clientes> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idTipoCliente)
                .HasColumnName("idTipoCliente")
                .IsRequired();

            builder.Property(e => e.codigo)
               .HasColumnName("codigo")
               .IsRequired();

            builder.Property(e => e.idDireccion)
                .HasColumnName("idDireccion");

            builder.Property(e => e.idEntidadComercial)
                .HasColumnName("idEntidadComercial");

            //builder.Property(e => e.idCorporacion)
            //    .HasColumnName("idCorporacion")
            //    .IsRequired();

            builder.Property(e => e.diasCredito)
                .HasColumnName("diasCredito")
                .IsRequired();

            builder.Property(e => e.idEmpresa)
               .HasColumnName("idEmpresa")
               .IsRequired();

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.tipoCliente)
                .WithMany()
                .HasForeignKey(f => f.idTipoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.direccion)
                .WithMany()
                .HasForeignKey(f => f.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.entidadComercial)
                .WithMany()
                .HasForeignKey(f => f.idEntidadComercial)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(f => f.corporacion)
            //    .WithMany()
            //    .HasForeignKey(f => f.idCorporacion)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_clientes_corporaciones1");
        }
    }
}
