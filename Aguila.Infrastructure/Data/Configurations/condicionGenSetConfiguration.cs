using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionGenSetConfiguration : IEntityTypeConfiguration<condicionGenSet>
    {
        public void Configure(EntityTypeBuilder<condicionGenSet> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();

            builder.Property(e => e.galonesRequeridos)
                .HasColumnName("galonesRequeridos");

            builder.Property(e => e.galonesGenSet)
                .HasColumnName("galonesGenSet");

            builder.Property(e => e.galonesCompletar)
                .HasColumnName("galonesCompletar");

            builder.Property(e => e.horometro)
                .HasColumnName("horometro");

            builder.Property(e => e.horaEncendida)
                .HasColumnName("horaEncendida");

            builder.Property(e => e.horaApagada)
                .HasColumnName("horaApagada");

            builder.Property(e => e.dieselEntradaSalida)
                .HasColumnName("dieselEntradaSalida");

            builder.Property(e => e.dieselConsumido)
                .HasColumnName("dieselConsumido");

            builder.Property(e => e.horasTrabajadas)
                .HasColumnName("horasTrabajadas");

            builder.Property(e => e.estExPuertasGolpeadas)
                .HasColumnName("estExPuertasGolpeadas")
                .IsRequired();

            builder.Property(e => e.estExPuertasQuebradas)
                .HasColumnName("estExPuertasQuebradas")
                .IsRequired();

            builder.Property(e => e.estExPuertasFaltantes)
                .HasColumnName("estExPuertasFaltantes")
                .IsRequired();

            builder.Property(e => e.estExPuertasSueltas)
                .HasColumnName("estExPuertasSueltas")
                .IsRequired();

            builder.Property(e => e.estExBisagrasQuebradas)
                .HasColumnName("estExBisagrasQuebradas")
                .IsRequired();

            builder.Property(e => e.panelGolpes)
                .HasColumnName("panelGolpes")
                .IsRequired();

            builder.Property(e => e.panelTornillosFaltantes)
                .HasColumnName("panelTornillosFaltantes")
                .IsRequired();

            builder.Property(e => e.panelOtros)
                .HasColumnName("panelOtros")
                .IsRequired();

            builder.Property(e => e.soporteGolpes)
                .HasColumnName("soporteGolpes")
                .IsRequired();

            builder.Property(e => e.soporteTornillosFaltantes)
                .HasColumnName("soporteTornillosFaltantes");

            builder.Property(e => e.soporteMarcoQuebrado)
                .HasColumnName("soporteMarcoQuebrado")
                .IsRequired();

            builder.Property(e => e.soporteMarcoFlojo)
                .HasColumnName("soporteMarcoFlojo")
                .IsRequired();

            builder.Property(e => e.soporteBisagrasQuebradas)
                .HasColumnName("soporteBisagrasQuebradas")
                .IsRequired();

            builder.Property(e => e.soporteSoldaduraEstado)
                .HasColumnName("soporteSoldaduraEstado")
                .IsRequired();

            builder.Property(e => e.revIntCablesQuemados)
                .HasColumnName("revIntCablesQuemados")
                .IsRequired();

            builder.Property(e => e.revIntCablesSueltos)
                .HasColumnName("revIntCablesSueltos")
                .IsRequired();

            builder.Property(e => e.revIntReparacionesImpropias)
                .HasColumnName("revIntReparacionesImpropias")
                .IsRequired();

            builder.Property(e => e.tanqueAgujeros)
                .HasColumnName("tanqueAgujeros")
                .IsRequired();

            builder.Property(e => e.tanqueSoporteDanado)
                .HasColumnName("tanqueSoporteDanado")
                .IsRequired();

            builder.Property(e => e.tanqueMedidorDiesel)
                .HasColumnName("tanqueMedidorDiesel")
                .IsRequired();

            builder.Property(e => e.tanqueCodoQuebrado)
                .HasColumnName("tanqueCodoQuebrado")
                .IsRequired();

            builder.Property(e => e.tanqueTapon)
                .HasColumnName("tanqueTapon")
                .IsRequired();

            builder.Property(e => e.tanqueTuberia)
                .HasColumnName("tanqueTuberia")
                .IsRequired();

            builder.Property(e => e.pFaltMedidorAceite)
                .HasColumnName("pFaltMedidorAceite")
                .IsRequired();

            builder.Property(e => e.pFaltTapaAceite)
                .HasColumnName("pFaltTapaAceite")
                .IsRequired();

            builder.Property(e => e.pFaltTaponRadiador)
                .HasColumnName("pFaltTaponRadiador")
                .IsRequired();

            builder.HasOne(f => f.condicionActivo)
               .WithMany()
               .HasForeignKey(f => f.idCondicionActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
