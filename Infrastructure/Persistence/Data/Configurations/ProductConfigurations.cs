using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(e => e.productBrand).WithMany().HasForeignKey(y => y.BrandId);
            builder.HasOne(e => e.productType).WithMany().HasForeignKey(y => y.TypeId);
            builder.Property(y => y.Price).HasColumnType("decimal(18,3)");
        }
    }
}
