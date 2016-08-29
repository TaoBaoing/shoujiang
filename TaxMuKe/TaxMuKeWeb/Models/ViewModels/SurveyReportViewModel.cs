using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BasicUPMS.Models
{
    public class SurveyReportViewModel
    {
        public SurveyReportViewModel()
        {
            Ratings = new Dictionary<int, int>();
            Ratings.Add(0, 0);
            Ratings.Add(1, 0);
            Ratings.Add(2, 0);
            Ratings.Add(3, 0);
            Ratings.Add(4, 0);
            Ratings.Add(5, 0);
        }

        public int QuestionNo { get; set; }
        public string QuestionName { get; set; }
        public Dictionary<int, int> Ratings { get; set; }

    }
}
