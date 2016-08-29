using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicData
{
    [Flags]
    public enum PermissionTypes
    {
        None = 0,
        Menu = 1,
        Feature = 2,
        PageElements = 4
    }
}