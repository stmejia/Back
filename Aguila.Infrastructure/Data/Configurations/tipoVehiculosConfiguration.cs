using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tipoVehiculosConfiguration : IEntityTypeConfiguration<tipoVehiculos>
    {
        public void Configure(EntityTypeBuilder<tipoVehiculos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.idEmpresa)
               .HasColumnName("idEmpresa")
               .IsRequired();

            builder.HasIndex(e => e.codigo)
                .IsUnique()
                .HasName("IX_tipoVehiculos_Codigo_Unico");//indica el indice unico para el campo codigo
            
            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.prefijo)
                .HasColumnName("prefijo")
                .IsRequired()
                .HasMaxLength(8);

            //builder.Property(e => e.correlativoLongitud)
            //    .HasColumnName("correlativoLongitud")
            //    .IsRequired();

            //builder.Property(e => e.estructuraCoc)
            //    .HasColumnName("estructuraCoc")
            //    //.IsRequired()
            //    .HasMaxLength(250);


            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
