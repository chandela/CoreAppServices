﻿using CoreApp.Models.CoreModels.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{
    public class ConfigurationSettings : BaseEntity
    {

        /// <summary>
        /// Gets or sets the ConfigurationSettingId
        /// </summary>
        /// <value>The ConfigurationSetting identifier.</value>
        public long ConfigurationSettingId { get; set; }

        public long BranchId { get; set; }
        /// <summary>
        /// Gets or sets the SettingName
        /// </summary>
        /// <value>The SettingName.</value>
        public string SettingName { get; set; }
        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        /// <value>The Value.</value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }
    }
}
