using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class proveedoresConfiguration : IEntityTypeConfiguration<proveedores>
    {
        public void Configure(EntityTypeBuilder<proveedores> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_proveedores_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.idDireccion)
                .HasColumnName("idDireccion")
                .HasColumnType("bigint");

            builder.Property(e => e.idTipoProveedor)
                .HasColumnName("idTipoProveedor")
                .IsRequired();

            //builder.Property(e => e.idCorporacion)
            //    .HasColumnName("idCorporacion")
            //    .IsRequired();

            builder.Property(e => e.idEntidadComercial)
                .HasColumnName("idEntidadComercial");

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

            builder.HasOne(f => f.direccion)
                .WithMany()
                .HasForeignKey(f => f.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.tipoProveedor)
                .WithMany()
                .HasForeignKey(f => f.idTipoProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(f => f.corporacion)
            //    .WithMany()
            //    .HasForeignKey(f => f.idCorporacion)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_proveedores_corporacion");

            builder.HasOne(f => f.entidadComercial)
                .WithMany()
                .HasForeignKey(f => f.idEntidadComercial)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.empresa)
                .WithMany()
                .HasForeignKey(f => f.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
