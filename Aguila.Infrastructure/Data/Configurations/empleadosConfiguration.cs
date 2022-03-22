
using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class empleadosConfiguration : IEntityTypeConfiguration<empleados>
    {
        public void Configure(EntityTypeBuilder<empleados> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10);

            builder.Property(e => e.codigoAnterior)
               .HasColumnName("codigoAnterior")
               .HasMaxLength(25);

            builder.HasIndex(e => e.codigo)
                .IsUnique();
                //.HasName("IX_empleados_Codigo_Unico");//indica el indice unico para el campo codigo

            builder.Property(e => e.nombres)
                .HasColumnName("nombres")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.apellidos)
                .HasColumnName("apellidos")
                .HasMaxLength(50);

            builder.Property(e => e.dpi)
                .HasColumnName("dpi")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.nit)
                .HasColumnName("nit")
                .HasMaxLength(15);

            builder.Property(e => e.idDireccion)
                .HasColumnName("idDireccion");

            builder.Property(e => e.telefono)
                .HasColumnName("telefono")
                .IsRequired();

            builder.Property(e => e.fechaAlta)
                .HasColumnName("fechaAlta")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.licenciaConducir)
                .HasColumnName("licenciaConducir")
                .HasMaxLength(30);

            builder.Property(e => e.fechaNacimiento)
                .HasColumnName("fechaNacimiento")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.fechaBaja)
                .HasColumnName("fechaBaja")
                .HasColumnType("datetime");

            builder.Property(e => e.idEmpresa)
               .HasColumnName("idEmpresa")
               .IsRequired();

            builder.Property(e => e.pais)
                .HasColumnName("pais");                     

            builder.Property(e => e.correlativo)
                .HasColumnName("correlativo")
                .HasMaxLength(4); ;

            builder.Property(e => e.departamento)
                .HasColumnName("departamento");

            builder.Property(e => e.area)
                .HasColumnName("area");

            builder.Property(e => e.subArea)
                .HasColumnName("subArea");

            builder.Property(e => e.puesto)
                .HasColumnName("puesto");

            builder.Property(e => e.categoria)
                .HasColumnName("categoria");

            builder.Property(e => e.localidad)
                .HasColumnName("localidad");

            builder.Property(e => e.idEmpresaEmpleador)
                .HasColumnName("idEmpresaEmpleador")
                .IsRequired(); ;

            builder.Property(e => e.estado)
                .HasColumnName("estado");

            builder.Property(e => e.dependencia)
               .HasColumnName("dependencia");

            builder.Property(e => e.cop)
                .HasColumnName("cop");

            builder.Property(e => e.placas)
                .HasColumnName("placas");

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.empresa)
                .WithMany()
                .HasForeignKey(e => e.idEmpresaEmpleador)
                .OnDelete(DeleteBehavior.ClientSetNull);

            

            builder.HasOne(e => e.direccion)
                .WithMany()
                .HasForeignKey(e => e.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<ImagenRecurso>(e => e.Fotos)
                .WithOne()
                .HasForeignKey<empleados>(e => e.idImagenRecursoFotografias)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
