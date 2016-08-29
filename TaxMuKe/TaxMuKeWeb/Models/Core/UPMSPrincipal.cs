using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BasicUPMS.Models
{
    public class UPMSPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        private long _roleId;

        public long RoleId
        {
            get { return _roleId; }
        }

        public UPMSPrincipal(IIdentity identity, long roleId)
        {
            Identity = identity;
            _roleId = roleId;
        }

        public bool IsInRole(string roleId)
        {
            return _roleId.ToString() == roleId;
        }
    }
}