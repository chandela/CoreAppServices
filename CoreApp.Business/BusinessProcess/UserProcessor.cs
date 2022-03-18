using AutoMapper;
using CoreApp.Business.Interfaces;
using CoreApp.Models.CoreModels.IRepositories;
using CoreApp.Models.CoreModels.SharedEntities;
using CoreApp.Models.Shared;
using CoreApp.SharedAssets;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Business.BusinessProcess
{
    public class UserProcessor : IUserProcessor
    {
        public UserModel CreateUser(UserModel user)
        {
            var destination = new User();
            Mapper.Map(user, destination);
            ServiceLocator.Current.GetInstance<IUserRepository>().CreateUserProfile(destination); 
            Mapper.Map(destination, user);
            user.SetCurrent();

            ServiceLocator.Current.GetInstance<IUserRepository>().CreateUserProfile(destination);
            return user;
        }

        public AuthenticationInfo GetUserAuth(Credentials cred, bool passwordCheckNeeded = true)
        {
            //var AuthenticationInfo = new AuthenticationInfo();
            var AuthenticationDetails = ServiceLocator.Current.GetInstance<IUserRepository>().Authenticate(cred.UserName,cred.Password);
            if (AuthenticationDetails == null || string.IsNullOrEmpty(AuthenticationDetails.Password))
                throw new CoreAppException("The User not found in the Branch");
            if (!AuthenticationDetails.IsBranchActivated)
                throw new CoreAppException("The Branch is not active in CoreApp.");
            if (passwordCheckNeeded && string.Compare(cred.Password, "-1") != 0 &&
                AuthenticationDetails.Password != cred.Password)
                throw new CoreAppException("Incorrect password.");
            if (!AuthenticationDetails.IsActivated)
                throw new CoreAppException("Please activate your account, via the email sent to your account.");
            if (!AuthenticationDetails.IsActive)
                throw new CoreAppException("User Account is in Not Active mode. Please contact club associate.");
            return AuthenticationDetails;

        }


    }
}
