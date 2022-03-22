using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionTecnicaGenSetConfiguration : IEntityTypeConfiguration<condicionTecnicaGenSet>
    {
        public void Configure(EntityTypeBuilder<condicionTecnicaGenSet> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();

            builder.Property(e => e.bateriaCodigo)
                .HasColumnName("bateriaCodigo")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.bateriaNivelAcido)
                .HasColumnName("bateriaNivelAcido");

            builder.Property(e => e.bateriaArnes)
                .HasColumnName("bateriaArnes");

            builder.Property(e => e.bateriaTerminales)
                .HasColumnName("bateriaTerminales");

            builder.Property(e => e.bateriaGolpes)
                .HasColumnName("bateriaGolpes");

            builder.Property(e => e.bateriaCarga)
                .HasColumnName("bateriaCarga");

            builder.Property(e => e.combustibleDiesel)
                .HasColumnName("combustibleDiesel");

            builder.Property(e => e.combustibleAgua)
                .HasColumnName("combustibleAgua");

            builder.Property(e => e.combustibleAceite)
                .HasColumnName("combustibleAceite");

            builder.Property(e => e.combustibleFugas)
                .HasColumnName("combustibleFugas");

            builder.Property(e => e.filtroAceite)//Campo tipo bit
                .HasColumnName("filtroAceite");

            builder.Property(e => e.filtroDiesel)//Campo tipo bit
                .HasColumnName("filtroDiesel");

            builder.Property(e => e.bombaAguaEstado)
                .HasColumnName("bombaAguaEstado");

            builder.Property(e => e.escapeAgujeros)
                .HasColumnName("escapeAgujeros");

            builder.Property(e => e.escapeDañado)
                .HasColumnName("escapeDañado");

            builder.Property(e => e.cojinetesEstado)
                .HasColumnName("cojinetesEstado");

            builder.Property(e => e.arranqueFuncionamiento)
                .HasColumnName("arranqueFuncionamiento");

            builder.Property(e => e.fajaAlternador)
                .HasColumnName("fajaAlternador");

            builder.Property(e => e.enfriamientoAire)
                .HasColumnName("enfriamientoAire");

            builder.Property(e => e.enfriamientoAgua)
                .HasColumnName("enfriamientoAgua");

            builder.Property(e => e.cantidadGeneradaVolts)//Campo tipo bit
                .HasColumnName("cantidadGeneradaVolts");

            builder.HasOne(f => f.condicionActivo)
             .WithMany()
             .HasForeignKey(f => f.idCondicionActivo)
             .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
