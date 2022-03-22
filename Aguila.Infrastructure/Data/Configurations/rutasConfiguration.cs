using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class rutasConfiguration : IEntityTypeConfiguration<rutas>
    {
        public void Configure(EntityTypeBuilder<rutas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEmpresa)
                .HasColumnName("idEmpresa")
                .IsRequired();

            builder.Property(e => e.idUbicacionOrigen)
                .HasColumnName("idUbicacionOrigen")
                .IsRequired();

            builder.Property(e => e.idUbicacionDestino)
                .HasColumnName("idUbicacionDestino")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(e => e.codigo)
               .IsUnique()
               .HasName("IX_rutas_empresa_codigo_unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

            //builder.Property(e => e.existeRutaAlterna)
            //    .HasColumnName("existeRutaAlterna")
            //    .IsRequired();

            builder.Property(e => e.distanciaKms)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("distanciaKms")
                .IsRequired();

            builder.Property(e => e.gradoPeligrosidad)
                .HasColumnName("gradoPeligrosidad")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.estadoCarretera)
                .HasColumnName("estadoCarretera")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        } 
    }
}
