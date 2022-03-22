using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionCabezalConfiguration : IEntityTypeConfiguration<condicionCabezal>
    {
        public void Configure(EntityTypeBuilder<condicionCabezal> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();                     

            builder.Property(e => e.windShield)
                .HasColumnName("windShield")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.plumillas)
                .HasColumnName("plumillas")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.viscera)
                .HasColumnName("viscera")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.rompeVientos)
                .HasColumnName("rompeVientos")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.persiana)
                .HasColumnName("persiana")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.bumper)
                .HasColumnName("bumper")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.capo)
                .HasColumnName("capo")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.retrovisor)
                .HasColumnName("retrovisor")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.ojoBuey)
                .HasColumnName("ojoBuey")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.pataGallo)
                .HasColumnName("pataGallo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.portaLlanta)
                .HasColumnName("portaLlanta")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.spoilers)
                .HasColumnName("spoilers")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.salpicadera)
                .HasColumnName("salpicadera")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.guardaFango)
                .HasColumnName("guardaFango")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.taponCombustible)
                .HasColumnName("taponCombustible")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.baterias)
                .HasColumnName("baterias")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.lucesDelanteras)
                .HasColumnName("lucesDelanteras")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.lucesTraseras)
                .HasColumnName("lucesTraseras")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.pintura)
                .HasColumnName("pintura")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.llanta1)
                .HasColumnName("llanta1")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta2)
                .HasColumnName("llanta2")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta3)
                .HasColumnName("llanta3")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta4)
                .HasColumnName("llanta4")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta5)
                .HasColumnName("llanta5")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta6)
                .HasColumnName("llanta6")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta7)
                .HasColumnName("llanta7")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta8)
                .HasColumnName("llanta8")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta9)
                .HasColumnName("llanta9")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llanta10)
                .HasColumnName("llanta10")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llantaR)
                .HasColumnName("llantaR")
                .HasMaxLength(50);
            //.IsRequired();

            builder.Property(e => e.llantaR2)
               .HasColumnName("llantaR2")
               .HasMaxLength(50);

            builder.HasOne(f => f.condicionActivo)
                .WithMany()
                .HasForeignKey(f => f.idCondicionActivo)
                .OnDelete(DeleteBehavior.ClientSetNull);

           

            //builder.HasOne(f => f.reparacion)
            //    .WithMany()
            //    .HasForeignKey(f => f.idReparacion)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_condicionCabezal_reparaciones");
        }
    }
}
