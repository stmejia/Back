using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aguila.Infrastructure.Data.Configurations
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        //Configuracion de clase principal Documento la misma se defina con Abstract para poder heredar
        //Las clases derivada no generan pKey y deben de tener el mismo atributo de clave con el mismo nombre


        
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documentos");
            builder.HasKey(e => e.id);
            builder.Property(p => p.id)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}



