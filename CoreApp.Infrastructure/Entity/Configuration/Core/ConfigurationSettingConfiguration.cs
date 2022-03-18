using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApp.Models.CoreModels.SharedEntities;

namespace CoreApp.Infrastructure.Entity.Configuration.Core
{
    public class ConfigurationSettingConfiguration : EntityTypeConfiguration<ConfigurationSettings>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationSettingConfiguration" /> class.
        /// </summary>
        public ConfigurationSettingConfiguration()
        {
            ToTable("ConfigurationSettings");
            HasKey(x => x.ConfigurationSettingId);
            Property(x => x.SettingName).IsRequired();
            Property(x => x.Value).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Ignore(x => x.CreateDateTime);
            Ignore(x => x.CreateUserId);
            Ignore(x => x.UpdateDateTime);
            Ignore(x => x.UpdateUserId);
            Ignore(x => x.InternalDataId);
        }
    }
}
