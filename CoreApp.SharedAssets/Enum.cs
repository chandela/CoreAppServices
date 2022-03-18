using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets
{
    public sealed class Constants
    {
        public const string USER_SESSION = "UserSession";
        public const string ShortDateFormat = "MM/dd/yyyy";
        public const string ShortDateTimeFormat = "MM/dd/yyyy hh:mm tt";
        public const string ShortMonthYearFormat = "MMM/yyyy";
        public const string RegexDateFormat = @"[0-9]{1,2}[./-][0-9]{1,2}[./-][0-9]{2,4}";
        public const string DayFormat = "dd";
        public const string HourMintsFormat = "hh:mm tt";
        public const string ApiLoginId = "7BDT2h2qWrz";
        public const string ApiTransactionKey = "63n63SpF4yM2pH3j";
        public const string REQUEST_TRACE_SWITCH = "trace";
        public const long GymEvolvClubId = 1;
        public static readonly Guid SYSTEM_USER_ID = new Guid("ffffffff-ffff-ffff-ffff-000000000001");
        public static readonly Guid GYMEVOLV_INTERNAL_DATAID = new Guid("ffffffff-ffff-ffff-ffff-000000000001");
        public static readonly string UserClubInternalDataId = "UserClubInternalDataId";
        public static readonly string UserName = "UserName";
        public static readonly string UserInternalDataId = "UserInternalDataId";
        public static readonly string IsAdmin = "IsAdmin";
        public static readonly string TrainerUnavailability = "Trainer Unavailability";
        public static readonly string Cancel = "Cancel";
        public static readonly string NotApplicable = "N/A";
        public const int HTTP_REMOTE_HOST_CLOSED_CONNECTION_ERRORCODE = unchecked((int)0x80072746);


        public enum ApplicationType
        {
            None = 0,
            [Description("Administrator")]
            Administrator = 1,
            [Description("Member")]
            Client = 2,
            [Description("Associate")]
            Associate = 8
        }
    }
}
