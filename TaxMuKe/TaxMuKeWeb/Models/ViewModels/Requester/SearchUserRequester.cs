using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    public class SearchUserRequester : PagedRequesterBase
    {
        public MuKeUserTypes UserType { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public string Name { get; set; }
        public string LogicName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }

        public DateTime? txtStartTime { get; set; }
        public DateTime? txtEndTime { get; set; }
    }
}