using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class EstacionesTrabajoConfiguration : IEntityTypeConfiguration<EstacionesTrabajo>
    {
        public void Configure(EntityTypeBuilder<EstacionesTrabajo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            builder.Property(e => e.Activa).HasColumnName("activa");

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasColumnName("codigo")
                .HasMaxLength(10);

            builder.Property(e => e.FchCreacion)
                .HasColumnName("fch_creacion")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre")
                .HasMaxLength(40);

            builder.Property(e => e.SucursalId).HasColumnName("sucursal_ID");

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasColumnName("tipo")
                .HasMaxLength(20);
                       
            builder.HasOne(d => d.Sucursal)
                .WithMany(p => p.EstacionesTrabajo)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(d => d.AsigUsuariosEstacionesTrabajo)
               .WithOne(p => p.EstacionTrabajo)
               .HasForeignKey(d => d.EstacionTrabajoId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
