using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApp.Models.CoreModels.SharedEntities;

namespace CoreApp.Models.CoreModels.IRepositories.Shared
{
   public interface IConfigurationRepository
    {
        /// <summary>
        /// Gets the configuration settings.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        /// <returns></returns>
        List<ConfigurationSettings> GetConfigurationSettings(long clubId);

        /// <summary>
        /// Insert configuration settings
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        void Persist(long userassociate);
    }
}
