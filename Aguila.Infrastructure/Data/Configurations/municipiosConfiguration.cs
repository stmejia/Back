using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class municipiosConfiguration : IEntityTypeConfiguration<municipios>
    {
        public void Configure(EntityTypeBuilder<municipios> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idDepartamento)
                .HasColumnName("idDepartamento")
                .IsRequired();

            builder.Property(e => e.codMunicipio)
                .HasColumnName("codMunicipio")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.nombreMunicipio)
                .HasColumnName("nombreMunicipio")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.departamento)
                .WithMany()
                .HasForeignKey(e => e.idDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
