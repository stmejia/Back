using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class generadoresConfiguration : IEntityTypeConfiguration<generadores>
    {
        public void Configure(EntityTypeBuilder<generadores> builder)
        {
            builder.HasKey(e=>e.idActivo);

            builder.Property(e => e.idActivo)
                .HasColumnName("idActivo");

            builder.Property(e => e.idTipoGenerador)
                .HasColumnName("idTipoGenerador")
                .IsRequired();

            builder.Property(e => e.capacidadGalones)
                .HasColumnName("capacidadGalones")
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(e => e.numeroCilindros)
                .HasColumnName("numeroCilindros")
                .IsRequired();

            builder.Property(e => e.marcaGenerador)
                .HasColumnName("marcaGenerador")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.tipoInstalacion)
                .HasColumnName("tipoInstalacion")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.tipoEnfriamiento)
                .HasColumnName("tipoEnfriamiento")
                .HasMaxLength(10)
                .IsRequired();


            builder.Property(e => e.aptoParaCA)
                .HasColumnName("aptoParaCA")
                .HasMaxLength(1);

            builder.Property(e => e.codigoAnterior)
               .HasColumnName("codigoAnterior")
               .HasMaxLength(25);

            builder.Property(e => e.tipoMotor)
               .HasColumnName("tipoMotor")
               .HasMaxLength(25);

            builder.Property(e => e.noMotor)
               .HasColumnName("noMotor")
               .HasMaxLength(25);

            builder.Property(e => e.velocidad)
               .HasColumnName("velocidad")
               .HasMaxLength(25);

            builder.Property(e => e.potenciaMotor)
               .HasColumnName("potenciaMotor")
               .HasMaxLength(25);

            builder.Property(e => e.modeloGenerador)
               .HasColumnName("modeloGenerador")
               .HasMaxLength(25);

            builder.Property(e => e.serieGenerador)
               .HasColumnName("serieGenerador")
               .HasMaxLength(25);

            builder.Property(e => e.tipoGeneradorGen)
               .HasColumnName("tipoGeneradorGen")
               .HasMaxLength(25);

            builder.Property(e => e.potenciaGenerador)
               .HasColumnName("potenciaGenerador")
               .HasMaxLength(25);

            builder.Property(e => e.tensionGenerador)
               .HasColumnName("tensionGenerador")
               .HasMaxLength(25);

            builder.Property(e => e.tipoTanque)
               .HasColumnName("tipoTanque")
               .HasMaxLength(50);

            builder.Property(e => e.tipoAceite)
               .HasColumnName("tipoAceite")
               .HasMaxLength(25);



            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.activoOperacion)
                .WithMany()
                .HasForeignKey(f => f.idActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.tipoGenerador)
                .WithMany()
                .HasForeignKey(f => f.idTipoGenerador)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
