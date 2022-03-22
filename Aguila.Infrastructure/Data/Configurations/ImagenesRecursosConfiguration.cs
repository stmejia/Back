using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class ImagenesRecursosConfiguration : IEntityTypeConfiguration<ImagenRecurso>
    {
        public void Configure(EntityTypeBuilder<ImagenRecurso> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Ignore(e => e.ImagenesEliminar)
               .Ignore(e => e.ImagenDefault)
               .Ignore(e => e.Usuario);

            //NOTA las propiedades de relacion o virtuales , solo se pueden relacionar una vez

            builder.HasMany<Imagen>(e => e.Imagenes)
                   .WithOne(f => f.ImagenRecurso)
                   .HasForeignKey(f => f.ImagenRecurso_Id);

            builder.HasOne<ImagenRecursoConfiguracion>(e => e.ImagenRecursoConfiguracion)
                .WithMany(f => f.ImagenesRecursos)
                .HasForeignKey(f => f.ImagenRecursoConfig_Id);

            // con esta configuracion , no es necesario tener otra propiedad virtual dentro de la tabla foranea
            // se desactivo esta propiedad devido a dependencia circular al grabar
            //builder.HasOne<Imagen>(e => e.ImagenDefault)
            //    .WithOne().HasForeignKey<ImagenRecurso>(f => f.Imagen_IdDefault);
                

        }

    }
}
