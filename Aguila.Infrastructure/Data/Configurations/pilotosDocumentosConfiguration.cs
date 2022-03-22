using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class pilotosDocumentosConfiguration : IEntityTypeConfiguration<pilotosDocumentos>
    {
        public void Configure(EntityTypeBuilder<pilotosDocumentos> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombreDocumento)
                .HasColumnName("nombreDocumento")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.idImagenRecursoDocumentos)
                .HasColumnName("idImagenRecursoDocumentos")
                .HasColumnType("Guid");

            builder.Property(e => e.idPiloto)
                .HasColumnName("idPiloto")
                .IsRequired();

            builder.Property(e => e.tipoDocumento)
                .HasColumnName("tipoDocumento")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime");

            builder.Property(e => e.fechaVigencia)
                .HasColumnName("fechaVigencia")
                .HasColumnType("datetime");

            builder.HasOne(f => f.piloto)
                .WithMany()
                .HasForeignKey(f => f.idPiloto)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
