using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{

        /// <summary>
        /// Data transformation class used to pass associate accessible club data between the business layer and the data layer.
        /// </summary>
        [Serializable]
        public class SignInformation
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            public string ClubName { set; get; }

            /// <summary>
            /// Gets or sets the internal data identifier.
            /// </summary>
            /// <value>
            /// The internal data identifier.
            /// </value>
            public Guid InternalDataId { set; get; }

            /// <summary>
            /// Gets or sets the club identifier.
            /// </summary>
            /// <value>
            /// The club identifier.
            /// </value>
            public long ClubId { set; get; }

            /// <summary>
            /// Gets or sets the type of the application.
            /// </summary>
            /// <value>
            /// The type of the application.
            /// </value>
           // public ApplicationType ApplicationType { set; get; }

            /// <summary>
            /// Gets or sets the common identifier.
            /// </summary>
            /// <value>
            /// The common identifier.
            /// </value>
            public long EntityId { set; get; }

            /// <summary>
            /// Gets or sets the club group identifier.
            /// </summary>
            /// <value>
            /// The club group identifier.
            /// </value>
            public long? ClubGroupId { set; get; }

            /// <summary>
            /// Gets or sets the name of the club group.
            /// </summary>
            /// <value>
            /// The name of the club group.
            /// </value>
            public string ClubGroupName { set; get; }

            /// <summary>
            /// Gets or sets the entity unique identifier identifier.
            /// </summary>
            /// <value>
            /// The entity unique identifier identifier.
            /// </value>
            public Guid UserInteralId { get; set; }

            /// <summary>
            /// Gets or sets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>
            public long UserId { get; set; }

            /// <summary>
            /// Gets or sets the club internal identifier.
            /// </summary>
            /// <value>
            /// The club internal identifier.
            /// </value>
            public Guid ClubInternalId { get; set; }
        }
    }
