using CoreApp.Models.CoreModels.SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Infrastructure.Entity.Shared
{
    public class AddressConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Address>
    {
        /// <summary>
        /// Initializes a new instance of the AddressConfiguration class.
        /// </summary>
        public AddressConfiguration()
        {
            ToTable("Address");
            Property(c => c.AddressId).IsRequired();
            Property(c => c.City).HasMaxLength(128);
            // Property(c => c.StateId);
            Property(c => c.ZipCode).HasMaxLength(20);
            Property(c => c.IsDeleted).IsRequired();
            Property(c => c.State).HasMaxLength(128);
            Property(c => c.Country).HasMaxLength(128);
            //  Property(c => c.GeometryLat).IsOptional();
        }
    }
}
