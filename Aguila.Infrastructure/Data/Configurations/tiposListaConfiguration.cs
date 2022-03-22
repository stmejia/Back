using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class tiposListaConfiguration : IEntityTypeConfiguration<tiposLista>
    {
        public void Configure(EntityTypeBuilder<tiposLista> builder)
        {
            builder.HasKey(e=>e.id);

            builder.Property(e => e.id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.idRecurso)
               .HasColumnName("idRecurso")
               .IsRequired();

            builder.Property(e => e.tipoDato)
              .HasColumnName("tipoDato")
              .HasMaxLength(20)
              .IsRequired();

            builder.Property(e => e.campo)
              .HasColumnName("campo")
              .HasMaxLength(20)
              .IsRequired();

            builder.Property(e => e.fechaCreacion)
              .HasColumnName("fechaCreacion")
              .HasColumnType("datetime");

            //llave foranea para idRecurso desde Entity Recursos
            builder.HasOne(d => d.recurso)
                .WithMany()
                .HasForeignKey(d => d.idRecurso)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK_tiposLista_Recurso");
        }
    }
}
