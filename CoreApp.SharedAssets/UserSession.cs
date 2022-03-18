using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets
{
    public class UserSession : IIdentity,IPrincipal
    {
        private Dictionary<string, string> mMenus;

        private TimeZoneInfo mTimezone;

        public TimeZoneInfo TimeZone
        {
            get
            {
                if (mTimezone == null || mTimezone.Id != this.TimeZoneId)
                {
                    mTimezone = TimeZoneInfo.FindSystemTimeZoneById(string.IsNullOrEmpty(this.TimeZoneId) ? System.TimeZone.CurrentTimeZone.StandardName : this.TimeZoneId);
                }
                return mTimezone;
            }
        }

        /// <summary>
        ///     Gets or sets the user associate identifier.
        /// </summary>
        /// <value>
        ///     The user associate identifier.
        /// </value>
        //public long UserAssociateId
        //{
        //    get
        //    {
        //        return CurrentDashboardType == ApplicationType.Associate && CurrentSelectedAssociateId != 0
        //            ? CurrentSelectedAssociateId
        //            : mUserAssociateId;
        //    }
        //    set { mUserAssociateId = value; }
        //}

        /// <summary>
        ///     Gets or sets the name of the TimeZoneId.
        /// </summary>
        /// <value>
        ///     The name of the TimeZoneId.
        /// </value>
        public string TimeZoneId { get; set; }

        /// <summary>
        ///     Gets the internal user identifier.
        /// </summary>
        /// <value>
        ///     The internal user identifier.
        /// </value>
        public Guid InternalUserId { get; private set; }

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; private set; }

        /// <summary>
        ///     Gets the user admin identifier.
        /// </summary>
        /// <value>
        ///     The user admin identifier.
        /// </value>
        public long UserAdminId { get; private set; }

        /// <summary>
        ///     Gets the session timeout minutes.
        /// </summary>
        /// <value>
        ///     The session timeout minutes.
        /// </value>
        public int SessionTimeoutMinutes { get; private set; }
        
        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; private set; }

        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public string FirstName { get; private set; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public string LastName { get; private set; }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", string.IsNullOrEmpty(LastName) ? "" : LastName,
                    string.IsNullOrEmpty(FirstName) ? "" : FirstName);
            }
        }

        /// <summary>
        ///     Gets or sets the logged in.
        /// </summary>
        /// <value>
        ///     The logged in.
        /// </value>
        public DateTime LoggedIn { get; set; }

        /// <summary>
        ///     Gets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; private set; }

        public string UserType { get;  set; }
        

        /// <summary>
        /// Gets or sets the menus.
        /// </summary>
        /// <value>
        /// The menus.
        /// </value>
        public Dictionary<string, string> Menus
        {
            get
            {
                mMenus = mMenus ?? new Dictionary<string, string>();
                return mMenus;
            }
            set { mMenus = value; }
        }

        #region IExtendedPrincipal interface members

        #endregion

        public static string GetAssessmblyVersion()
        {
            var assembly = Assembly.GetCallingAssembly();
            var aName = assembly.GetName();
            return aName.Version.ToString();
        }

        #region IIdentity Members

        /// <summary>
        ///     Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the name of the current user.
        /// </summary>
        public string Name
        {
            get { return UserName; }
        }

        /// <summary>
        ///     Gets the type of authentication used.
        /// </summary>
        public string AuthenticationType
        {
            get { return "CoreAppAuthentication"; }
        }

        #endregion

        #region IPrincipal Implementation

        /// <summary>
        ///     We only implement this so that we can attach our object to ride on Thread.CurrentPrincipal
        /// </summary>                
        public IIdentity Identity
        {
            get
            {
                //Otherwise return this implementation                
                return this;
            }
        }

        /// <summary>
        ///     Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        ///     true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion IPrincipal Implementation

        #region Member variables

        //private long mCurrentSelectedUserId;
        private string mCurrentSelectedUserName;
        //private long mCurrentSelectedAssociateId;
        private string mCurrentSelectedUserLastName;
        private string mCurrentSelectedUserFristName;
        private DateTime mCurrentSelectedUserLoggedIn;
        public long mUserAssociateId;
        private string mRole;



        public string Role
        {
            get { return mRole; }
            set { mRole = value; }
        }

  

        /// <summary>
        ///     Gets or sets the current selected user logged in.
        /// </summary>
        /// <value>
        ///     The current selected user logged in.
        /// </value>
        public DateTime CurrentSelectedUserLoggedIn
        {
            get { return mCurrentSelectedUserLoggedIn; }
            set { mCurrentSelectedUserLoggedIn = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the current selected user.
        /// </summary>
        /// <value>
        ///     The name of the current selected user.
        /// </value>
        public string CurrentSelectedUserName
        {
            get { return mCurrentSelectedUserName; }
            set { mCurrentSelectedUserName = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the current selected user frist.
        /// </summary>
        /// <value>
        ///     The name of the current selected user frist.
        /// </value>
        public string CurrentSelectedUserFristName
        {
            get { return mCurrentSelectedUserFristName ?? FirstName; }
            set { mCurrentSelectedUserFristName = value; }
        }

        /// <summary>
        ///     Gets or sets the last name of the current selected user.
        /// </summary>
        /// <value>
        ///     The last name of the current selected user.
        /// </value>
        public string CurrentSelectedUserLastName
        {
            get { return mCurrentSelectedUserLastName ?? LastName; }
            set { mCurrentSelectedUserLastName = value; }
        }

        /// <summary>
        ///     Gets the full name of the current selected user.
        /// </summary>
        /// <value>
        ///     The full name of the current selected user.
        /// </value>
        public string CurrentSelectedUserFullName
        {
            get { return string.Format("{0}, {1}", CurrentSelectedUserLastName, CurrentSelectedUserFristName); }
        }

     

        #endregion

        #region Constructors

        public UserSession()
        {
        }

        public UserSession(long clubId, string UserName, Guid clubInternalDataId, 
            long userId, string email,
             string firstName, string lastName, Guid internalUserId, long userClubId,
            
            int sessionTimeoutMinutes,
             string usertype)
        {
           // TimeZoneId = timeZoneId;

            UserId = userId;
            Email = email;
            UserName = UserName;
            FirstName = firstName;
            LastName = lastName;
            InternalUserId = internalUserId;

           // UserAdminId = userAdminId;

            SessionTimeoutMinutes = sessionTimeoutMinutes;
            UserType = usertype;
        }

        #endregion
    }
}
