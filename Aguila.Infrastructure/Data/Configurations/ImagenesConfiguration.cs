using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class ImagenesConfiguration : IEntityTypeConfiguration<Imagen> 
    {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName)
                .HasColumnName("FileName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.FchCreacion)
               .HasColumnName("fchCreacion")
               .IsRequired()
               .HasColumnType("smalldatetime");

            builder.Property(e => e.FchBorrada)
               .HasColumnName("fchBorrada")
               .HasColumnType("smalldatetime");


            builder.Ignore(e => e.SubirImagenBase64)
                   .Ignore(e => e.UrlImagen);

            builder.HasOne<ImagenRecurso>(e => e.ImagenRecurso)
                   .WithMany(f => f.Imagenes)
                   .HasForeignKey(e => e.ImagenRecurso_Id);            

        }
    }
}
