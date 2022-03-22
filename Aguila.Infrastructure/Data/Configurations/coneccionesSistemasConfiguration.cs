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
    public class coneccionesSistemasConfiguration : IEntityTypeConfiguration<coneccionesSistemas>
    {
        public void Configure(EntityTypeBuilder<coneccionesSistemas> builder)
        {
            builder.HasNoKey();
        }
    }
}
