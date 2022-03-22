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
     public class CondicionTallerConfiguration : IEntityTypeConfiguration<CondicionTaller>
    {
        //NOTA IMPORTANTE   **********************************
        //Esta en la configuracion de una clase heredada en este caso la clase heredada es
        //CondicionTalle hereda de documento, notar que en la configuracion la clase principal lleva el incremento del pkey
        //La clase principal se define como ABSTRACT, las clases heredadas deben de herdar de esta y tienen que copartir la misma pKey con el mismo nombre
        // en la utilizacion en la clase heredada se hace la grabacion y guarda de la clase principal



        public void Configure(EntityTypeBuilder<CondicionTaller> builder)
        {
            builder.ToTable("CondicionesTaller");
            //builder.HasKey(e => e.id);

            //builder.Property(p => p.id)                
            //    .ValueGeneratedOnAdd()
            //    .IsRequired();


        }
    }
}
