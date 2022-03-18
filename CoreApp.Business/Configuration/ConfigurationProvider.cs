using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CoreApp.SharedAssets;
using CoreApp.SharedAssets.Configuration;
using CoreApp.Models.CoreModels.IRepositories.Shared;

namespace CoreApp.Business.BusinessProcess
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IConfigurationRepository mConfigurationRepository;

        public ConfigurationReader(IConfigurationRepository configurationRepository)
        {
            mConfigurationRepository = configurationRepository;
        }

        public void Init(NameValueCollection configurationPersistenceParameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Reads the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        public string Read(string settingName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[settingName];
        }

        /// <summary>
        ///     Reads the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns></returns>
        public string Read(string settingName, string customAttribute)
        {
            var cSettings = mConfigurationRepository.GetConfigurationSettings(Convert.ToInt64(customAttribute));
            return cSettings.FirstOrDefault(a => a.SettingName == settingName).Value;
        }

        /// <summary>
        ///     Reads the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        public object ReadSection(string sectionName)
        {
            return System.Configuration.ConfigurationManager.GetSection(sectionName);
        }

        /// <summary>
        ///     Reads the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ReadSection(string sectionName, string customAttribute)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persists the specified club configuration.
        /// </summary>
        /// <param name="ClubId">The club identifier.</param>
        public void Persist(long ClubId)
        {
            try
            {
                mConfigurationRepository.Persist(ClubId);
            }
            catch (CoreAppException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw new CoreAppException(exception.Message);
            }
        }
    }
}
