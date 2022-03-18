using CoreApp.Models.CoreModels.SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.IRepositories
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public interface IUserRepository :IRepository<User>
#pragma warning restore CS0436 // Type conflicts with imported type
    {
#pragma warning disable CS0436 // Type conflicts with imported type
                              /// <summary>
                              /// CreateProfile will create new user profile
                              /// </summary>
                              /// <param name="profile"></param>
                              /// <returns></returns>
        void CreateUserProfile(User profile);
#pragma warning restore CS0436 // Type conflicts with imported type

        string Createname(long profile);

        AuthenticationInfo Authenticate(string username, string password);
    }
}
