using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionFurgonConfiguration : IEntityTypeConfiguration<condicionFurgon>
    {
        public void Configure(EntityTypeBuilder<condicionFurgon> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();

            builder.Property(e => e.revExtGolpe)//Campo tipo bit
                .HasColumnName("revExtGolpe");

            builder.Property(e => e.revExtSeparacion)//Campo tipo bit
                .HasColumnName("revExtSeparacion");

            builder.Property(e => e.revExtRoturas)//Campo tipo bit
                .HasColumnName("revExtRoturas");

            builder.Property(e => e.revIntGolpes)//Campo tipo bit
                .HasColumnName("revIntGolpes");

            builder.Property(e => e.revIntSeparacion)//Campo tipo bit
                .HasColumnName("revIntSeparacion");

            builder.Property(e => e.revIntSeparacion)//Campo tipo bit
                .HasColumnName("revIntSeparacion");

            builder.Property(e => e.revIntFiltra)//Campo tipo bit
                .HasColumnName("revIntFiltra");

            builder.Property(e => e.revIntRotura)//Campo tipo bit
                .HasColumnName("revIntRotura");

            builder.Property(e => e.revIntPisoH)//Campo tipo bit
                .HasColumnName("revIntPisoH");

            builder.Property(e => e.revIntManchas)//Campo tipo bit
                .HasColumnName("revIntManchas");

            builder.Property(e => e.revIntOlores)//Campo tipo bit
                .HasColumnName("revIntOlores");

            builder.Property(e => e.revPuertaCerrado)
                .HasColumnName("revPuertaCerrado")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.revPuertaEmpaque)
                .HasColumnName("revPuertaEmpaque")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.revPuertaCinta)
                .HasColumnName("revPuertaCinta")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.limpPiso)//Campo tipo bit
                .HasColumnName("limpPiso");
            
            builder.Property(e => e.limpTecho)//Campo tipo bit
                .HasColumnName("limpTecho");
            
            builder.Property(e => e.limpLateral)//Campo tipo bit
                .HasColumnName("limpLateral");

            builder.Property(e => e.limpExt)//Campo tipo bit
                .HasColumnName("limpExt");

            builder.Property(e => e.limpPuerta)//Campo tipo bit
                .HasColumnName("limpPuerta");

            builder.Property(e => e.limpMancha)//Campo tipo bit
                .HasColumnName("limpMancha");

            builder.Property(e => e.limpOlor)//Campo tipo bit
                .HasColumnName("limpOlor");

            builder.Property(e => e.limpRefuerzo)//Campo tipo bit
                .HasColumnName("limpRefuerzo");

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
            
            builder.Property(e => e.lucesG)//Campo tipo bit
                .HasColumnName("lucesG");
            
            builder.Property(e => e.lucesH)//Campo tipo bit
                .HasColumnName("lucesH");
            
            builder.Property(e => e.lucesI)//Campo tipo bit
                .HasColumnName("lucesI");
            
            builder.Property(e => e.lucesJ)//Campo tipo bit
                .HasColumnName("lucesJ");
            
            builder.Property(e => e.lucesK)//Campo tipo bit
                .HasColumnName("lucesK");
            
            builder.Property(e => e.lucesL)//Campo tipo bit
                .HasColumnName("lucesL");
            
            builder.Property(e => e.lucesM)//Campo tipo bit
                .HasColumnName("lucesM");
            
            builder.Property(e => e.lucesN)//Campo tipo bit
                .HasColumnName("lucesN");
            
            builder.Property(e => e.lucesO)//Campo tipo bit
                .HasColumnName("lucesO");

            builder.Property(e => e.guardaFangosI)//Campo tipo bit
                .HasColumnName("guardaFangosI");

            builder.Property(e => e.guardaFangosD)//Campo tipo bit
                .HasColumnName("guardaFangosD");

            builder.Property(e => e.fricciones)
                .HasColumnName("fricciones")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(e => e.senalizacion)
                .HasColumnName("senalizacion")
                .HasMaxLength(1)
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
