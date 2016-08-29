using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class UserHistory
    {
      
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ParentObjectId { get; set; }
        public long ObjectId { get; set; }
         
        public int IntegerValue { get; set; }
        public string StringValue { get; set; }
        public string Description { get; set; }

        public DateTime CreateTime { get; set; } 

        public UserHistoryTypes HistoryType { get; set; }
        public virtual MuKeUsers User { get; set; }
    }
}
