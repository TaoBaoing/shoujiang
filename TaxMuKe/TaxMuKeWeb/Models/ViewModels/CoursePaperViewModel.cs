using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models
{
    public class CoursePaperViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string ImageUrl { get; set; }

        public long ClassId { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }
//        public ExamTypes ExamType { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public MuKeEnabledStatus RecommendStatus { get; set; }
        public MuKeEnabledStatus HotStatus { get; set; }
        public int ClickCount { get; set; }
//        public ExamOpenTypes OpenType { get; set; }
        public int TotalScore { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }

        public long TaxonomyId { get; set; }
    }
}