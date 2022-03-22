using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    
    public class ImagenesRecursosConfiguracionConfiguration : IEntityTypeConfiguration<ImagenRecursoConfiguracion>
    {
        public void Configure(EntityTypeBuilder<ImagenRecursoConfiguracion> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.Ignore(e => e.UrlImagenDefaul);

            builder.HasMany<ImagenRecurso>(e => e.ImagenesRecursos)
                .WithOne(f => f.ImagenRecursoConfiguracion)
                .HasForeignKey(f => f.ImagenRecursoConfig_Id);

            builder.Property(e => e.Recurso_Id).HasColumnName("Recurso_Id");
            builder.Property(e => e.Propiedad).HasColumnName("Propiedad");

            builder.Property(e => e.FchCreacion)
               .HasColumnName("FchCreacion")
               .HasColumnType("smalldatetime");

            builder.Property(e => e.Servidor).HasColumnName("Servidor");
            builder.Property(e => e.Carpeta).HasColumnName("Carpeta");
            builder.Property(e => e.PesoMaxMb).HasColumnName("PesoMaxMb");
            builder.Property(e => e.EliminacionFisica).HasColumnName("EliminacionFisica");
            builder.Property(e => e.MultiplesImagenes).HasColumnName("MultiplesImagenes");
            builder.Property(e => e.DefaultImagen).HasColumnName("DefaultImagen");
            builder.Property(e => e.NoMaxImagenes).HasColumnName("NoMaxImagenes");

            builder.HasOne(e => e.Recurso)
                .WithMany()
                .HasForeignKey(e => e.Recurso_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }

    }





}
