using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreAppServices.Models
{
    [Serializable]
    public class JavascriptUserSession
    {


        /// <summary>
        ///     Gets or sets the current selected user identifier.
        /// </summary>
        /// <value>
        ///     The current selected user identifier.
        /// </value>
        public long CurrentSelectedUserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the current selected user.
        /// </summary>
        /// <value>
        ///     The name of the current selected user.
        /// </value>
        public string CurrentSelectedUserName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the current selected user frist.
        /// </summary>
        /// <value>
        ///     The name of the current selected user frist.
        /// </value>
        public string CurrentSelectedUserFristName { get; set; }

        /// <summary>
        ///     Gets or sets the last name of the current selected user.
        /// </summary>
        /// <value>
        ///     The last name of the current selected user.
        /// </value>
        public string CurrentSelectedUserLastName { get; set; }

        /// <summary>
        ///     Gets or sets the current selected associate identifier.
        /// </summary>
        /// <value>
        ///     The current selected associate identifier.
        /// </value>
        public long CurrentSelectedAssociateId { get; set; }

        /// <summary>
        ///     Gets or sets the user associate identifier.
        /// </summary>
        /// <value>
        ///     The user associate identifier.
        /// </value>
        public long UserAssociateId { get; set; }

        /// <summary>
        ///     Gets the club code.
        /// </summary>
        /// <value>
        ///     The club code.
        /// </value>
        public string ClubCode { get; set; }

        /// <summary>
        ///     Gets or sets the name of the club.
        /// </summary>
        /// <value>
        ///     The name of the club.
        /// </value>
        public string ClubName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the TimeZoneId.
        /// </summary>
        /// <value>
        ///     The name of the TimeZoneId.
        /// </value>
        public string TimeZoneId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is trainer.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is trainer; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrainer { get; set; }

        /// <summary>
        ///     Gets the internal user identifier.
        /// </summary>
        /// <value>
        ///     The internal user identifier.
        /// </value>
        public Guid InternalUserId { get; set; }

        /// <summary>
        ///     Get and Set ClubID
        /// </summary>
        /// <value>
        ///     The club identifier.
        /// </value>
        public long ClubId { get; set; }

        /// <summary>
        ///     Gets the external club identifier.
        /// </summary>
        /// <value>
        ///     The external club identifier.
        /// </value>
        public Guid ExternalClubId { get; set; }

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets the user admin identifier.
        /// </summary>
        /// <value>
        ///     The user admin identifier.
        /// </value>
        public long UserAdminId { get; set; }

        /// <summary>
        ///     Gets or sets the user member identifier.
        /// </summary>
        /// <value>
        ///     The user member identifier.
        /// </value>
        public long UserMemberId { get; set; }

        /// <summary>
        ///     Gets the user club identifier.
        /// </summary>
        /// <value>
        ///     The user club identifier.
        /// </value>
        public long UserClubId { get; set; }

        /// <summary>
        ///     Gets the user club internal data identifier.
        /// </summary>
        /// <value>
        ///     The user club internal data identifier.
        /// </value>
        public Guid UserClubInternalDataId { get; set; }

        /// <summary>
        ///     Gets the session timeout minutes.
        /// </summary>
        /// <value>
        ///     The session timeout minutes.
        /// </value>
        public int SessionTimeoutMinutes { get; set; }



        /// <summary>
        ///     Gets the feature groups.
        /// </summary>
        /// <value>
        ///     The feature groups.
        /// </value>
        public Dictionary<string, int> UserFeatureGroups { get; set; }

        /// <summary>
        ///     Gets the features.
        /// </summary>
        /// <value>
        ///     The features.
        /// </value>
        public Dictionary<string, int> UserFeatures { get; set; }

        public Dictionary<string, int> Feature { get; set; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        ///     Gets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; set; }


        public Dictionary<string, int> MembershipStatus { get; set; }

        public Dictionary<string, string> Menus { get; set; }

        /// <summary>
        ///     Gets or sets the additional attributes.
        /// </summary>
        /// <value>
        ///     The additional attributes.
        /// </value>
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public string AssemblyVersion { get; set; }
    }
}