using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{
    public class AuthenticationInfo
    {
        /// <summary>
        ///     Gets or sets the user club internal data identifier.
        /// </summary>
        /// <value>
        ///     The user club internal data identifier.
        /// </value>
        public Guid UserClubInternalDataId { get; set; }

        /// <summary>
        ///     Gets or sets the user internal identifier.
        /// </summary>
        /// <value>
        ///     The user internal identifier.
        /// </value>
        public Guid UserInternalId { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is activated; otherwise, <c>false</c>.
        /// </value>
        public bool IsActivated { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     Gets or sets the user client identifier.
        /// </summary>
        /// <value>
        ///     The user client identifier.
        /// </value>
       // public long UserMemberId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is club activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is Branch activated; otherwise, <c>false</c>.
        /// </value>
        public bool IsBranchActivated { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}
