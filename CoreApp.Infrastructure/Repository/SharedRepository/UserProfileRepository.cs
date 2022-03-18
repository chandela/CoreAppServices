using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApp.Infrastructure.DBContext;
using System.Transactions;
using CoreApp.Models.CoreModels.IRepositories;
using CoreApp.Models.CoreModels.SharedEntities;
using CoreApp.Models.CoreModels.BaseEntities;
using System.Data.SqlClient;
using System.Data;
using CoreApp.SharedAssets;
using static CoreApp.SharedAssets.Constants;

namespace CoreApp.Infrastructure.Repository.SharedRepository
{
    public class UserProfileRepository :IUserRepository
    {


        public void CreateUserProfile(User profile)
        {
            using (var scope = new TransactionScope())
            {
                using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
                {

                    profile.ObjectState = ObjectState.New;
                    dbContext.Persist(profile);

                    //var userActivation = dbContext.Table<UserActivation>()
                    //    .Where(x => x.IsActive && x.UserId == source.UserId)
                    //    .OrderByDescending(x => x.CreateDateTime)
                    //    .FirstOrDefault();

                    //userActivation.ActivationDate = DateTime.Now.UtcNowByTimeZone();
                    //userActivation.IsActive = false;
                    //userActivation.ObjectState = ObjectState.Modified;
                    //dbContext.Persist(userActivation);

                    dbContext.SaveChanges();

                    scope.Complete();
                }
            }
        }


        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GetUserName(long userId)
        {
            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                var user =
                    dbContext.Table<User>()
                        .Where(x => x.UserId == userId)
                        .Select(x => new { x.FirstName, x.LastName })
                        .FirstOrDefault();
                return string.Format("{0} {1}", user.FirstName, user.LastName);
            }
        }



        /// <summary>
        ///     Authenticates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordCheckNeeded">if set to <c>true</c> [password check needed].</param>
        /// <returns></returns>
        public AuthenticationInfo Authenticate(string userName, string password /*,bool passwordCheckNeeded = true*/)
        {
            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                var dbParameter = new SqlParameter();
                dbParameter.ParameterName = "userName";
                dbParameter.SqlDbType = SqlDbType.NVarChar;
                dbParameter.Value = userName;
                return
                    dbContext.ExecuteStoredProcedureList<AuthenticationInfo>("UserLogin", dbParameter).FirstOrDefault();
            }
        }



        /// <summary>
        ///     Gets the associate accessible clubs.
        /// </summary>
        /// <param name="clubId">The club identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<SignInformation> GetSignInformation(Guid branchId, Guid userId)
        {
            var signInformation = new List<SignInformation>();

            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                var result = (from user in dbContext.Table<User>()
                              where
                                  user.InternalDataId == userId
                                  // && userMember.User.Club.InternalDataId == clubId
                                  && user.IsActive
                              select new
                              {
                                  user.UserName,
                                  user.UserId,
                                  // EntityId = user.UserId
                              }).
                    ToList();
                    

                if (result != null)
                {
                    signInformation.Add(new SignInformation
                    {
                        //UserInteralId = result.,
                        //UserId = result.UserId,
                        //ClubInternalId = result.ClubInternalId
                    });
                }

                return signInformation;
            }
        }
        public User GetById(long id)
        {
            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                return dbContext.Table<User>().FirstOrDefault(x => x.UserId == id);
            }
        }

        /// <summary>
        ///     Gets the member agreement.
        /// </summary>
        /// <param name="userMemberId">The user member identifier.</param>
        /// <param name="membershipPlanId">The membership plan identifier.</param>
        /// <returns></returns>
        //public MemberAgreement GetMemberAgreement(long userMemberId, long membershipPlanId)
        //{
        //    using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
        //    {
        //        return
        //            dbContext.Table<MemberAgreement>()
        //                .FirstOrDefault(x => x.UsermemberId == userMemberId && x.EntityId == membershipPlanId);
        //    }
        //}



        /// <summary>
        ///     Persists the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Persist(User entity)
        {
            using (var scope = new TransactionScope())
            {
                using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
                {
                    if (entity.ObjectState != ObjectState.Current)
                    {
                        dbContext.Persist(entity);
                        dbContext.SaveChanges();
                    }
                    scope.Complete();
                }
            }

        }

        public string Createname(long id)
        {
            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                return"Got method worked";
            }
        }



        public UserSession GetUserSession(ApplicationType applicationType, Guid internalUserId)
        {
            UserSession userSession = null;
           // var features = new Hashtable();
           // var featureGroupFeature = new Hashtable();

            using (var dbContext = ServiceLocator.Current.GetInstance<IDbContext>())
            {
                var dbClub = (from usr in dbContext.Table<User>()
                              where usr.InternalDataId == internalUserId
                              select new
                              {
                                  usr.InternalDataId,
                                  usr.UserName,
                                  usr.UserId,
                                  usr.UserType
                                 // usr.BranchId
                              }).First();

                switch (applicationType)
                {
                    case ApplicationType.Associate:
                        var associate = (from userAssociate in dbContext.Table<Employees>()
                                         where userAssociate.User.InternalDataId == internalUserId
                                         select new
                                         {
                                             userAssociate.User.UserId,
                                            // userAssociate.User.Features,
                                             UserClubId = userAssociate.BranchId,
                                             userBranchInternalDataId = userAssociate.Branch.InternalDataId,
                                             userAssociate.User.UserName,
                                             userAssociate.User.FirstName,
                                             userAssociate.User.LastName,
                                             userAssociate.User.Email,
                                             EntityId = userAssociate.EmployeeId,
                                             userAssociate.IsTrainer,
                                             userAssociate.User.InternalDataId,
                                             userAssociate.User.UserType,
                                         }).First();


                        //var featureGroups =
                        //    JsonConvert.DeserializeObject<List<FeatureGroupJsonExtrator>>(associate.Features).ToList();

                        //var roleFeature = new List<FeaturesJsonExtrator>();

                        //featureGroups.ForEach(
                        //    featureGroup => { featureGroup.Features.ForEach(feature => { roleFeature.Add(feature); }); });

                        //GetFeatures(roleFeature, out featureGroupFeature, out features);


                        userSession = new UserSession(
                            dbClub.UserId,
                            dbClub.UserName,
                            dbClub.InternalDataId,
                           // dbClub.TimeZoneId,
                            associate.UserId,
                            associate.Email,
                            associate.FirstName,
                            associate.LastName,
                            associate.InternalDataId,
                            associate.UserClubId,
                           // associate.userClubInternalDataId,
                            120,
                            associate.UserType
                            )
                        {
                           // UserAssociateId = associate.EntityId,
                           // CurrentDashboardType = ApplicationType.Associate,
                          //  CurrentSelectedUserId = associate.UserId,
                            UserType = associate.UserType
                        };

                        break;
                    case ApplicationType.Client:
                        var clientFuture = (from userMember in dbContext.Table<Employees>()
                                            where userMember.User.InternalDataId == internalUserId
                                            join address in dbContext.Table<Address>()
                                                on userMember.User.AddressId equals address.AddressId
                                            select new
                                            {
                                                userMember.User.UserId,
                                                userMember.CreateDateTime,
                                                //userMember.MemberNumber,
                                                UserClubId = userMember.BranchId,
                                                // userClubInternalDataId = userMember.User.br.InternalDataId,
                                                userMember.User.UserName,
                                                userMember.User.FirstName,
                                                userMember.User.LastName,
                                                userMember.User.Email,
                                                //EntityId = userMember.UserMemberId,
                                                userMember.InternalDataId,
                                                // userMember.Status,
                                            });
                                            //.DeferredFirst().FutureValue();


                        //var client = clientFuture.Value;

                        userSession = new UserSession(
                            //dbClub.,
                            //dbClub.Name,
                            //dbClub.Code,
                            //dbClub.InternalDataId,
                            //dbClub.TimeZoneId,
                            //client.UserId,
                            //client.Email,
                            //client.UserName,
                            //client.FirstName,
                            //client.LastName,
                            //false,
                            //client.InternalDataId,
                            //client.UserClubId,
                            //client.userClubInternalDataId,
                            //0,
                            //120,
                            //ApplicationType.Client,
                            //features,
                            //featureGroupFeature,
                            //"Member"
                            )
                        {
                            //UserMemberId = client.EntityId,
                            //CurrentDashboardType = ApplicationType.Client,
                            //UserMember = new ThreadUserMemberSession
                            //{
                            //    MembershipStatus = (MembershipStatus)client.Status,
                            //    MemberNumber = client.MemberNumber,
                            //    PaymentGatewayCustomerProfileId = client.PGCustomerProfileId,
                            //    Address = client.Address1,
                            //    ContactNumber = client.Phone,
                            //    Email = client.Email,
                            //    CreatedAt = client.CreateDateTime
                            //}
                        };

                        break;
                }
            }
           // userSession.LoggedIn = DateTime.Now.UtcNowByTimeZone();
            return userSession;
        }

    }
}
