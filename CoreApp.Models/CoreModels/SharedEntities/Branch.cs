using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{
    

        public class Branch : BaseEntities.BaseEntity
    {
            /// <summary>
            ///     Initializes a new instance of the <see cref="Club" /> class.
            /// </summary>
            public Branch()
            {
               // ClubPaymentGatewayProfiles = new HashSet<ClubPaymentGatewayProfile>();
                ClubConfigurationSettings = new HashSet<ConfigurationSettings>();
            }

            /// <summary>
            ///     Gets or sets the club identifier.
            /// </summary>
            /// <value>The club identifier.</value>
            public long BranchId { get; set; }

            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string TimeZoneId { get; set; }

            /// <summary>
            ///     Gets or sets the contact person.
            /// </summary>
            /// <value>The contact person.</value>
            public string ContactPerson { get; set; }

            /// <summary>
            ///     Gets or sets the club group identifier.
            /// </summary>
            /// <value>The club group identifier.</value>
            public long? ClubGroupId { get; set; }

            /// <summary>
            ///     Gets or sets the code.
            /// </summary>
            /// <value>The code.</value>
            public virtual string Code { private set; get; }

            /// <summary>
            ///     Gets or sets the name of the domain.
            /// </summary>
            /// <value>
            ///     The name of the domain.
            /// </value>
            public string DomainName { get; set; }

            /// <summary>
            ///     Gets or sets a value indicating whether this instance is active.
            /// </summary>
            /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
            public bool IsActive { get; set; }

            /// <summary>
            ///     Gets or sets the address identifier.
            /// </summary>
            /// <value>The address identifier.</value>
            public long? AddressId { get; set; }

            /// <summary>
            ///     Gets or sets the address.
            /// </summary>
            /// <value>
            ///     The address.
            /// </value>
            public virtual Address Address { get; set; }

            /// <summary>
            ///     Gets or sets the club payment gateway profiles.
            /// </summary>
            /// <value>
            ///     The club payment gateway profiles.
            /// </value>
            //public virtual ICollection<ClubPaymentGatewayProfile> ClubPaymentGatewayProfiles { get; set; }

            /// <summary>
            ///     Gets or sets the club configuration settings.
            /// </summary>
            /// <value>
            ///     The club configuration settings.
            /// </value>
            public virtual ICollection<ConfigurationSettings> ClubConfigurationSettings { get; set; }
        }
    }
