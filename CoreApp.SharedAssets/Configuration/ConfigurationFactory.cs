using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Xml;

namespace CoreApp.SharedAssets.Configuration
{

    #region ConfigurationFactory

    /// <summary>
    ///     StatePersistenceFactory handles creation of state
    ///     persistence providers
    /// </summary>
    public sealed class ConfigurationFactory
    {
        private const string CONFIGURATIONMANAGEMENT = "appConfigurationManagement";

        /// <summary>
        ///     Looks up current configuration and returns the appropriate
        ///     configuration provider
        /// </summary>
        /// <returns>IConfigurationReader interface</returns>
        public static IConfigurationReader GetProvider()
        {
            IConfigurationReader configurationProvider = null;

            // lookup configuration management provider settings
            // NOTE: this uses the application's configuration settings
            var typeSettings =
                (ConfigurationManagementProviderSettings)
                System.Configuration.ConfigurationManager.GetSection(CONFIGURATIONMANAGEMENT);

            try
            {
                // cast to our public interface
                configurationProvider = (IConfigurationReader)ReflectionHelper.Create(typeSettings, null);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "An instance of the type:{0} in assembly:{1} could not cast to the IConfigurationReader interface. ",
                        typeSettings.Type, typeSettings.Assembly), e);
            }

            // call Init on configuration provider with configured settings
            configurationProvider.Init(typeSettings.AdditionalAttributes);

            return configurationProvider;
        }
    }

    #endregion

    #region ConfigurationManagementSectionHandler

    /// <summary>
    ///     ConfigurationManagementSectionHandler reads configurationProvider information
    ///     from configuration file and returns provider settings
    /// </summary>
    public class ConfigurationManagementSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        ///     Interface implementation for reading our configuration provider's configuration
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public virtual object Create(object parent, object configContext, XmlNode section)
        {
            return new ConfigurationManagementProviderSettings(section);
        }
    }

    #endregion

    #region ConfigurationManagementProviderSettings

    /// <summary>
    ///     ConfigurationManagementProviderSettings defines the state persistence provider
    ///     settings within the configuration settings in the config file.
    /// </summary>
    public class ConfigurationManagementProviderSettings : ObjectTypeSettings
    {
        #region Variables

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates an instance of the StatePersistenceProviderSettings class using the specified configNode
        /// </summary>
        public ConfigurationManagementProviderSettings(XmlNode configNode) : base(configNode)
        {
            AdditionalAttributes = new NameValueCollection();
            foreach (XmlAttribute currentAttribute in configNode.Attributes)
                AdditionalAttributes.Add(currentAttribute.Name, currentAttribute.Value);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the state persistence attributes
        /// </summary>
        public NameValueCollection AdditionalAttributes { get; private set; }

        #endregion
    }

    #endregion
}
