using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace BasicData
{
    [Validator(typeof(PermissionResourceValidator))]
    public partial class PermissionResource
    {
        public PermissionResource()
        {
            this.Permissions = new List<Permissions>();
            //this.Children = new List<PermissionResource>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }

        //public virtual PermissionResource Parent { get; set; }
        //public virtual ICollection<PermissionResource> Children { get; set; }

        public int Depth { get; set; }
        public PermissionTypes PermissionType { get; set; }
        public string LinkUrl { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ElementSelector { get; set; }
        public string Icon { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public int Sort { get; set; }
        public string Comment { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
