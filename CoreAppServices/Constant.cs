using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreAppServices
{
    public class Constant
    {
        [Serializable]
        public enum ResponseCode
        {
            Success = 100,
            Failed = 200,
            Error = 300,
            Loaded = 400,
            Saved = 500
        }

        public static class CSS
        {
            /// <summary>
            ///     The home CSS
            /// </summary>
            public const string HomeCSS = "";
        }
    }
}
