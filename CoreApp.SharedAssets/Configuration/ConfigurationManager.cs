using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets.Configuration
{
    public sealed class ConfigurationManager
    {
        private static readonly IConfigurationReader mConfigurationProvider;

        /// <summary>
        ///     Static constructor for initializing static readonly variables.
        /// </summary>
        static ConfigurationManager()
        {
            mConfigurationProvider = ConfigurationFactory.GetProvider();
        }

        /// <summary>
        ///     Reads a basic setting
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns>Value of setting</returns>
        public static string Read(string settingName)
        {
            // required param checks
            if (settingName == null)
                throw new ArgumentNullException("SettingName cannot be null.", "settingName");
            if (settingName.Length == 0)
                throw new ArgumentException("SettingName must be non-zero length.", "settingName");

            return mConfigurationProvider.Read(settingName);
        }

        /// <summary>
        ///     Reads an organization-specific setting
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="customAttribute">Customizable attibute (manytimes, organizationID)</param>
        /// <returns>Value of setting</returns>
        public static string Read(string settingName, string customAttribute)
        {
            // required param checks
            if (settingName == null)
                throw new ArgumentNullException("SettingName cannot be null.", "settingName");
            if (settingName.Length == 0)
                throw new ArgumentException("SettingName must be non-zero length.", "settingName");

            if (customAttribute == null)
                throw new ArgumentNullException("CustomAttribute cannot be null.", "customAttribute");

            return mConfigurationProvider.Read(settingName, customAttribute);
        }

        /// <summary>
        ///     Reads a custom config section
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns>Object representing specified section</returns>
        public static object ReadSection(string sectionName)
        {
            // required param checks
            if (sectionName == null)
                throw new ArgumentNullException("SectionName cannot be null.", "sectionName");
            if (sectionName.Length == 0)
                throw new ArgumentException("SectionName must be non-zero length.", "sectionName");

            // Forward to ConfigurationSettings, for now
            return mConfigurationProvider.ReadSection(sectionName);
        }

        /// <summary>
        ///     Reads a custom config section
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="customAttribute"></param>
        /// <returns>Object representing specified section</returns>
        public static object ReadSection(string sectionName, string customAttribute)
        {
            // required param checks
            if (sectionName == null)
                throw new ArgumentNullException("SectionName cannot be null.", "sectionName");
            if (sectionName.Length == 0)
                throw new ArgumentException("SectionName must be non-zero length.", "sectionName");

            if (customAttribute == null)
                throw new ArgumentNullException("CustomAttribute cannot be null.", "customAttribute");

            // Forward to ConfigurationSettings, for now
            return mConfigurationProvider.ReadSection(sectionName, customAttribute);
        }
    }
}
