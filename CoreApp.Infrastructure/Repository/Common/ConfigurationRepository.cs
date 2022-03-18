using CoreApp.Infrastructure.DBContext;
using CoreApp.Models.CoreModels.BaseEntities;
using CoreApp.Models.CoreModels.Common;
using CoreApp.Models.CoreModels.IRepositories.Shared;
using CoreApp.Models.CoreModels.SharedEntities;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CoreApp.Infrastructure.Repository.Common
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        /// <summary>
        ///     Gets the configuration settings.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        /// <returns></returns>
        public List<ConfigurationSettings> GetConfigurationSettings(long branchId)
        {
            using (var dbContext = new DefaultDbContext())
            {
                return dbContext.ConfigurationSettings.Where(a => a.BranchId == branchId && a.IsActive).ToList();
            }
        }

        /// <summary>
        ///     Persists the specified club identifier.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        public void Persist(long clubId)
        {
            using (var scope = new TransactionScope())
            {
                using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
                {
                    var lstConfigurationSettingTemplate = dbContext.Table<ConfigurationSettingTemplate>().ToList();
                    lstConfigurationSettingTemplate.ForEach(item =>
                    {
                        var mConfigurationSettings = new ConfigurationSettings();
                        mConfigurationSettings.ObjectState = ObjectState.New;
                        mConfigurationSettings.CreateUserId = clubId;
                        mConfigurationSettings.IsActive = item.IsActive;
                        mConfigurationSettings.SettingName = item.SettingName;
                        mConfigurationSettings.Value = item.Value;
                        mConfigurationSettings.InternalDataId = Guid.NewGuid();
                        dbContext.Persist(mConfigurationSettings);
                    });
                    dbContext.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
