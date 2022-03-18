using CoreApp.Models.CoreModels.SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreApp.Models.Shared
{
    public class UserModel : Base.EntityModel
    {


        public UserModel()
        {
            UserId = CurrentUserSession == null ? 1 : CurrentUserSession.UserId;
            IsActive = true;
        }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get; set; }

        public long UniqueNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
       // public string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>1
        /// 
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName
        {
            get
            {
                return string.Format("{0} , {1}", LastName, FirstName);
            }
        }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age.</value>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>The sex.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is activated.
        /// </summary>
        /// <value><c>null</c> if [is activated] contains no value, <c>true</c> if [is activated]; otherwise, <c>false</c>.</value>
        public bool? IsActivated { get; set; }

        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>The address identifier.</value>
        public long AddressId { get; set; }

#pragma warning disable CS0436 // Type conflicts with imported type
                              /// <summary>
                              /// Gets or sets the address.
                              /// </summary>
                              /// <value>
                              /// The address.
                              /// </value>
        public virtual Address Address { get; set; }
#pragma warning restore CS0436 // Type conflicts with imported type

        //  public  string Address { get; set; }

        //public string City { get; set; }

        //public string State { get; set; }

        //public string Country { get; set; }

        //public long PostalCode { get; set; }

        public long MobileNumber { get; set; }

        public long SecondaryMobileNumber { get; set; }

        public string UserType { get; set; }

       // public UserRoleType UserRoleType { get; set; }

    }
}