using CoreApp.Models.CoreModels.SharedEntities;
using CoreApp.Models.Shared;
using System;
using System.Collections.Generic;

namespace CoreApp.Business.Interfaces
{
    public interface IUserProcessor
    {

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        //UserModel GetById(long userId);

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        // string GetUserName(long userId);

        /// <summary>
        ///     Authenticates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordCheckNeeded">if set to <c>true</c> [password check needed].</param>
        /// <returns></returns>
        //   AuthenticationInfo Authenticate(string userName, string password, bool passwordCheckNeeded = true);

        /// <summary>
        ///     Authenticates the administrator.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isNotwebRequest">if set to <c>true</c> [is notweb request].</param>
        // void AuthenticateAdministrator(AuthenticationInfo user, bool isNotwebRequest = false);




        /// <summary>
        ///     Usps the get associate accessible clubs.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        //  List<SignInformation> GetSignInformation(Guid clubId, Guid userId);

        /// <summary>
        /// Gets the user session.
        /// </summary>
        /// <param name="applicationType">Type of the application.</param>
        /// <param name="clubInternalId">The club internal identifier.</param>
        /// <param name="userInteralId">The user interal identifier.</param>
        /// <returns></returns>
        // UserSession GetUserSession(ApplicationType applicationType, Guid clubInternalId, Guid userInteralId);

        /// <summary>
        /// Sends the password email.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        // void SendPasswordEmail(string emailID, string username);
        /// <summary>
        /// Sends the username to email.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        //  void SendUserNameEmail(string emailID);
        /// <summary>
        /// Creates the user activation.
        /// </summary>
        /// <param name="userActivation">The user activation.</param>
        /// 
        UserModel CreateUser(UserModel usermodel);

        /// <summary>
        /// Gets the user activation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
       // UserActivationModel GetUserActivation(long userId);

        /// <summary>
        /// Saves the user activation.
        /// </summary>
        /// <param name="userActivation">The user activation.</param>
      //  void SaveUserActivation(UserModel user);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="entity">The entity.</param>
        //void UpdatePassword(UserModel entity);

        AuthenticationInfo GetUserAuth(Credentials credentilas, bool passwordcheck);
    }
}
