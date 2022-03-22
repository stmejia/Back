using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class llantasConfiguration : IEntityTypeConfiguration<llantas>
    {
        public void Configure(EntityTypeBuilder<llantas> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.idEstadoIngreso)
                .HasColumnName("idEstadoIngreso")
                .IsRequired();

            builder.Property(e => e.proveedorId)
                .HasColumnName("proveedorId")
                .IsRequired();

            builder.Property(e => e.idLlantaTipo)
                .HasColumnName("idLlantaTipo")
                .IsRequired();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.marca)
                .HasColumnName("marca")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.serie)
                .HasColumnName("serie")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.reencaucheIngreso)
                .HasColumnName("reencaucheIngreso")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.medidas)
                .HasColumnName("medidas")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.precio)
                .HasColumnName("precio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.llantaDoble)//Campo tipo bit
                .HasColumnName("llantaDoble");

            builder.Property(e => e.fechaIngreso)
                .HasColumnName("fechaIngreso")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.Property(e => e.propositoIngreso)
                .HasColumnName("propositoIngreso")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.fechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(f => f.estado)
                .WithMany()
                .HasForeignKey(f => f.idEstadoIngreso)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.listaProveedor)
                .WithMany()
                .HasForeignKey(f => f.proveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(f => f.llantaTipo)
                .WithMany()
                .HasForeignKey(f => f.idLlantaTipo)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
