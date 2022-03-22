using Aguila.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Data.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.nombre)
                .HasColumnName("nombre")
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
