using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CoreApp.SharedAssets
{
    public class UserSessionManagement
    {

        public interface IUserSessionManagement
        {
            UserSession CurrentUserSession { get; set; }
        }

        [Serializable]
        public class ThreadUserSessionManagement : IUserSessionManagement
        {
            #region Constructor

            #endregion

            #region IUserSessionManagement Members

            public UserSession CurrentUserSession
            {
                get { return Thread.CurrentPrincipal as UserSession; }
                set
                {
                    Thread.CurrentPrincipal = value;
                    if (HttpContext.Current == null || HttpContext.Current.Session == null) return;
                    HttpContext.Current.Session[Constants.USER_SESSION] = value;
                }
            }

            #endregion
        }
    }
}
