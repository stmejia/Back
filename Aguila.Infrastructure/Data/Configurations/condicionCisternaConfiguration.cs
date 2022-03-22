using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class condicionCisternaConfiguration : IEntityTypeConfiguration<condicionCisterna>
    {
        public void Configure(EntityTypeBuilder<condicionCisterna> builder)
        {
            builder.HasKey(e => e.idCondicionActivo);

            builder.Property(e => e.idCondicionActivo)
               .HasColumnName("idCondicionActivo")
               .IsRequired();

            builder.Property(e => e.luzLateral)
                .HasColumnName("luzLateral")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.luzTrasera)
                .HasColumnName("luzTrasera")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.guardaFango)
                .HasColumnName("guardaFango")
                .HasMaxLength(15)
                .IsRequired();
            
            builder.Property(e => e.cintaReflectiva)
                .HasColumnName("cintaReflectiva")
                .HasMaxLength(15)
                .IsRequired();
            
            builder.Property(e => e.manitas)
                .HasColumnName("manitas")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.bumper)//Campo tipo bit
                .HasColumnName("bumper");
            
            builder.Property(e => e.patas)//Campo tipo bit
                .HasColumnName("patas");
            
            builder.Property(e => e.ganchos)//Campo tipo bit
                .HasColumnName("ganchos");

            builder.Property(e => e.fricciones)
                .HasColumnName("fricciones")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.friccionesLlantas)
                .HasColumnName("friccionesLlantas")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.escalera)
                .HasColumnName("escalera")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.señalizacion)
                .HasColumnName("señalizacion")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.taponValvulas)
                .HasColumnName("taponValvulas")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.manHole)
                .HasColumnName("manHole")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.platinas)
                .HasColumnName("platinas")
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
