using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets.Configuration
{
    public interface IConfigurationReader
    {
        /// <summary>
        ///     Inits the config provider
        /// </summary>
        /// <param name="statePersistenceParameters">provider settings</param>
        void Init(NameValueCollection configurationPersistenceParameters);

        /// <summary>
        ///     Reads the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        string Read(string settingName);

        /// <summary>
        ///     Reads the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns></returns>
        string Read(string settingName, string customAttribute);

        /// <summary>
        ///     Reads the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        object ReadSection(string sectionName);

        /// <summary>
        ///     Reads the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns></returns>
        object ReadSection(string sectionName, string customAttribute);

        /// <summary>
        /// Persists the specified club configuration.
        /// </summary>
        /// <param name="ClubID">The club identifier.</param>
        void Persist(long clubId);
    }
}
