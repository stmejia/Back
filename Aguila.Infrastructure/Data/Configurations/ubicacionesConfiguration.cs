using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class ubicacionesConfiguration : IEntityTypeConfiguration<ubicaciones>
    {
        public void Configure(EntityTypeBuilder<ubicaciones> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idMunicipio)
                .HasColumnName("idMunicipio")
                .IsRequired();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(5);

            //builder.Property(e => e.esPuerto)
            //    .HasColumnName("esPuerto")
            //    .IsRequired();

            builder.Property(e => e.lugar)
                .HasColumnName("lugar")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.codigoPostal)
                .HasColumnName("codigoPostal")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.municipio)
                .WithMany()
                .HasForeignKey(f => f.idMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasConstraintName("fk_ubicaciones_municipios1");

            builder.HasOne(f => f.empresa)
                .WithMany()
                .HasForeignKey(f => f.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK_ubicaciones_empresa");
        }
    }
}
