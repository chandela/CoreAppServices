using CoreApp.Infrastructure.Entity.Configuration.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Infrastructure
{
    public class DefaultDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDbContext"/> class.
        /// </summary>
        public DefaultDbContext()
        {
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            base.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultDbContext"].ConnectionString;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Gets or sets the configuration settings.
        /// </summary>
        /// <value>
        /// The configuration settings.
        /// </value>
        public DbSet<CoreApp.Models.CoreModels.SharedEntities.ConfigurationSettings> ConfigurationSettings { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ConfigurationSettingConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
