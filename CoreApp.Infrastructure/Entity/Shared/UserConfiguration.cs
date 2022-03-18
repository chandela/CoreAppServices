using System.Data.Entity.ModelConfiguration;
using CoreApp.Models.CoreModels.SharedEntities;


namespace CoreApp.Infrastructure.Entity.Shared
{

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        ///     Initializes a new instance of the UserConfiguration class.
        /// </summary>
        public UserConfiguration()
    {
        ToTable("User");
        HasKey(x => x.UserId);
        Property(c => c.UserId).IsRequired();
        Property(c => c.UserName).IsRequired();
        Property(c => c.FirstName).IsRequired();
        Property(c => c.LastName).IsRequired();
        Property(c => c.Age).IsOptional();
        Property(c => c.Gender).IsOptional();
        Property(c => c.Email).IsOptional();
        Property(c => c.IsActive).IsRequired();
        Property(c => c.IsDeleted).IsRequired();
        Property(c => c.IsActivated).IsOptional();
        Ignore(c => c.FullName);
    }
}
}
