using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoreApp.SharedAssets.Configuration
{


    public sealed class ReflectionHelper
    {
        /// <summary>
        ///     Creates an object using the full name type contained in typeSettings.
        /// </summary>
        /// <param name="typeSettings">A typeSetting object with the needed type information to create a class instance.</param>
        /// <param name="args">Constructor arguments.</param>
        /// <returns>An instance of the specified type.</returns>
        public static object Create(ObjectTypeSettings typeSettings, object[] args)
        {
            Assembly assemblyInstance = null;
            Type typeInstance = null;

            try
            {
                //  Use full assembly name to get assembly instance
                assemblyInstance = Assembly.Load(typeSettings.Assembly);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "The assembly {0} could not be loaded.  Please check that the assembly type is fully qualified and is of format that follows.  Also be sure that the version and public key token are correct.  The correct format is: type='Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard.WizardController,   Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard, Version=1.0.1.0, Culture=neutral, PublicKeyToken=d69d63db1380c14d'",
                        typeSettings.Assembly), e);
            }

            //  use type name to get type from assembly
            try
            {
                typeInstance = assemblyInstance.GetType(typeSettings.Type, true, false);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "An instance of the type {0} could not be loaded.  Please check that the assembly type is fully qualified and is of the format: type='Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard.WizardController,   Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard, Version=1.0.1.0, Culture=neutral, PublicKeyToken=d69d63db1380c14d' ANOTHER possibility is that the object creation failed during construction; be sure you have supplied the correct constructor arguments, and that the type to be created has implemented the constructor overloads/overrides required",
                        typeInstance), e);
            }

            try
            {
                if (args != null)
                    return Activator.CreateInstance(typeInstance, args);
                return Activator.CreateInstance(typeInstance);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "An instance of the type {0} could not be loaded.  Please check that the assembly type is fully qualified and is of the format: type='Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard.WizardController,   Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard, Version=1.0.1.0, Culture=neutral, PublicKeyToken=d69d63db1380c14d' ANOTHER possibility is that the object creation failed during construction; be sure you have supplied the correct constructor arguments, and that the type to be created has implemented the constructor overloads/overrides required",
                        typeSettings.Type), e);
            }
        }

        /// <summary>
        ///     Creates an object using the full name type contained in typeSettings.
        /// </summary>
        /// <param name="typeSettings">A typeSetting object with the needed type information to create a class instance.</param>
        /// <param name="args">Constructor arguments.</param>
        /// <returns>An instance of the specified type.</returns>
        public static object CreateFrom(ObjectTypeSettings typeSettings, object[] args)
        {
            Assembly assemblyInstance = null;
            Type typeInstance = null;

            // load the assembly
            try
            {
                assemblyInstance = Assembly.Load(typeSettings.Assembly);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "The assembly {0} could not be loaded.  Please check that the assembly path is correct.",
                        typeSettings.Assembly), e);
            }

            //  use type name to get type from assembly
            try
            {
                typeInstance = assemblyInstance.GetType(typeSettings.Type, true, false);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "An instance of the type {0} could not be loaded.  Please check that the assembly type is fully qualified and is of the format: type='Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard.WizardController,   Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard, Version=1.0.1.0, Culture=neutral, PublicKeyToken=d69d63db1380c14d' ANOTHER possibility is that the object creation failed during construction; be sure you have supplied the correct constructor arguments, and that the type to be created has implemented the constructor overloads/overrides required",
                        typeInstance), e);
            }

            // now create the specified instance
            try
            {
                if (args != null)
                    return Activator.CreateInstance(typeInstance, args);
                return Activator.CreateInstance(typeInstance);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    string.Format(
                        "An instance of the type {0} could not be loaded.  Please check that the assembly type is fully qualified and is of the format: type='Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard.WizardController,   Microsoft.ApplicationBlocks.UIProcess.Samples.AddressWizard, Version=1.0.1.0, Culture=neutral, PublicKeyToken=d69d63db1380c14d' ANOTHER possibility is that the object creation failed during construction; be sure you have supplied the correct constructor arguments, and that the type to be created has implemented the constructor overloads/overrides required",
                        typeSettings.Type), e);
            }
        }
    }


    #region ObjectTypeSettings

    /// <summary>
    ///     Base class for all providers settings.
    /// </summary>
    [Serializable]
    public abstract class ObjectTypeSettings
    {
        /// <summary>
        ///     Takes the incoming full type string, defined as:
        ///     "Microsoft.ApplicationBlocks.UIProcess.WinFormViewManager,   Microsoft.ApplicationBlocks.UIProcess,
        ///     Version=1.0.0.4, Culture=neutral, PublicKeyToken=d69d63db1380c14d"
        ///     and splits the type into two strings: the typeName and the assemblyName. These are passed as OUT params.
        ///     This routine also cleans up any extra white space and throws an exception if the full type string
        ///     does not have five comma-delimited parts. It expects the true full name, complete with version and publicKeyToken.
        /// </summary>
        /// <param name="fullType">The full type string defined in the configuration file.</param>
        private void SplitType(string fullType)
        {
            var parts = fullType.Split(COMMA.ToCharArray());

            if (parts.Length == 1)
            {
                Type = fullType;
            }
            else if (parts.Length == 2)
            {
                //  set the object type name
                Type = parts[0].Trim();

                //  set the object assembly name
                Assembly = parts[1].Trim();
            }
            else if (parts.Length == 5)
            {
                //  set the object type name
                Type = parts[0].Trim();

                //  set the object assembly name
                Assembly = string.Concat(parts[1].Trim() + COMMA,
                    parts[2].Trim() + COMMA,
                    parts[3].Trim() + COMMA,
                    parts[4].Trim());
            }
            else
            {
                throw new ArgumentException(
                    "The type names must be specified using the full type name and the 5 parts of the assembly name: typeName, assemblyName, version, publicToken, culture.",
                    "fullType");
            }
        }

        #region Declares Variables

        private const string ATTRIBUTENAME = "name";
        private const string ATTRIBUTETYPE = "type";
        private const string COMMA = ",";

        #endregion

        #region Constructors

        /// <summary>
        ///     Overloaded. Default constructor.
        /// </summary>
        public ObjectTypeSettings()
        {
        }

        public ObjectTypeSettings(string type) : this(string.Empty, type)
        {
        }

        public ObjectTypeSettings(string name, string type)
        {
            Name = name;
            SplitType(type);
        }

        /// <summary>
        ///     Overloaded. Initialized an instance of the ObjectTypeSettings class using the specified configNode.
        /// </summary>
        /// <param name="configNode">The XmlNode from the configuration file.</param>
        public ObjectTypeSettings(XmlNode configNode)
        {
            LoadAttributes(configNode);
        }

        private void LoadAttributes(XmlNode configNode)
        {
            XmlNode typeAttribute, nameAttribute;

            //Gets the typed object attributes
            typeAttribute = configNode.Attributes.RemoveNamedItem(ATTRIBUTETYPE);
            string fullType;
            if (typeAttribute.Value.Trim().Length > 0)
                fullType = typeAttribute.Value;
            else
                throw new ConfigurationErrorsException(
                    string.Format(
                        "Can´t load the xml configuration because the attribute {0} has a invalid value. Xml node {1}",
                        ATTRIBUTETYPE, configNode.Name));

            //  fix up type/asm strings
            SplitType(fullType);

            nameAttribute = configNode.Attributes.RemoveNamedItem(ATTRIBUTENAME);
            if (nameAttribute.Value.Trim().Length > 0)
                Name = nameAttribute.Value;
            else
                throw new ConfigurationErrorsException(
                    string.Format(
                        "Can´t load the xml configuration because the attribute {0} has a invalid value. Xml node {1}",
                        ATTRIBUTENAME, configNode.Name));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the object name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the object fully qualified type name.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        ///     Gets the object fully qualified assembly name.
        /// </summary>
        public string Assembly { get; private set; }

        #endregion
    }

    #endregion
}
