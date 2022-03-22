using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionEquipoConfiguration : IEntityTypeConfiguration<condicionEquipo>
    {
        public void Configure(EntityTypeBuilder<condicionEquipo> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();

            builder.Property(e => e.lucesA)//Campo tipo bit
                .HasColumnName("lucesA");

            builder.Property(e => e.lucesB)//Campo tipo bit
                .HasColumnName("lucesB");

            builder.Property(e => e.lucesC)//Campo tipo bit
                .HasColumnName("lucesC");

            builder.Property(e => e.lucesD)//Campo tipo bit
                .HasColumnName("lucesD");

            builder.Property(e => e.lucesE)//Campo tipo bit
                .HasColumnName("lucesE");

            builder.Property(e => e.lucesF)//Campo tipo bit
                .HasColumnName("lucesF");

            builder.Property(e => e.pi)//Campo tipo bit
                .HasColumnName("pi");

            builder.Property(e => e.pd)//Campo tipo bit
                .HasColumnName("pd");

            builder.Property(e => e.sd)//Campo tipo bit
                .HasColumnName("sd");

            builder.Property(e => e.guardaFangosG)
                .HasColumnName("guardaFangosG")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.guardaFangosI)
                .HasColumnName("guardaFangosI")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.cintaReflectivaLat)
                .HasColumnName("cintaReflectivaLat")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.cintaReflectivaFront)
                .HasColumnName("cintaReflectivaFront")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.cintaReflectivaTra)
                .HasColumnName("cintaReflectivaTra")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.manitas1)
                .HasColumnName("manitas1")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.manitas2)
                .HasColumnName("manitas2")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.bumper)
                .HasColumnName("bumper")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.fricciones)
                .HasColumnName("fricciones")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.friccionesLlantas)
                .HasColumnName("friccionesLlantas")
                .HasMaxLength(45);

            builder.Property(e => e.patas)
                .HasColumnName("patas")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.ganchos)
                .HasColumnName("ganchos")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.balancines)
                .HasColumnName("balancines")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.hojasResortes)
                .HasColumnName("hojasResortes")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.placaPatin)
               .HasColumnName("placaPatin")               
               .IsRequired();

            builder.Property(e => e.llanta1)
                .HasColumnName("llanta1")
                .HasMaxLength(50);

            builder.Property(e => e.llanta2)
                .HasColumnName("llanta2")
                .HasMaxLength(50);

            builder.Property(e => e.llanta3)
                .HasColumnName("llanta3")
                .HasMaxLength(50);

            builder.Property(e => e.llanta4)
                .HasColumnName("llanta4")
                .HasMaxLength(50);

            builder.Property(e => e.llanta5)
                .HasColumnName("llanta5")
                .HasMaxLength(50);

            builder.Property(e => e.llanta6)
                .HasColumnName("llanta6")
                .HasMaxLength(50);

            builder.Property(e => e.llanta7)
                .HasColumnName("llanta7")
                .HasMaxLength(50);

            builder.Property(e => e.llanta8)
                .HasColumnName("llanta8")
                .HasMaxLength(50);

            builder.Property(e => e.llanta9)
                .HasColumnName("llanta9")
                .HasMaxLength(50);

            builder.Property(e => e.llanta10)
                .HasColumnName("llanta10")
                .HasMaxLength(50);

            builder.Property(e => e.llanta11)
                .HasColumnName("llanta11")
                .HasMaxLength(50);

            builder.Property(e => e.llanta12)
                .HasColumnName("llanta12")
                .HasMaxLength(50);

            builder.Property(e => e.llantaR)
                .HasColumnName("llantaR")
                .HasMaxLength(50);

            builder.Property(e => e.llantaR2)
                .HasColumnName("llantaR2")
                .HasMaxLength(50);

            builder.HasOne(f => f.condicionActivo)
               .WithMany()
               .HasForeignKey(f => f.idCondicionActivo)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
