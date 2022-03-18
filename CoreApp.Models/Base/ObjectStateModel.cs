using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreApp.Models.Base
{
    public enum ObjectStateModel
    {
        Current = 0,
        New = 1,
        Deleted = 2,
        Modified = 4
    }
}