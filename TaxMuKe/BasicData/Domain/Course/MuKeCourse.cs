using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class MuKeCourse
    {
        public MuKeCourse()
        {
            MuKeCourseCapterTestPagperManagers=new List<MuKeCourseCapterTestPagperManager>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }//内容->详情
        public string ImageUrl { get; set; }
        public string Teacher { get; set; }
        public long TeacherId { get; set; }
        public SeriesType SeriesType { get; set; }
        public string CourseCategory { get; set; }
        public MuKeEnabledStatus RecommendStatus { get; set; }
        public CourseType CourseType { get; set; }
        public MuKeEnabledStatus HotStatus { get; set; }
        public MuKeEnabledStatus NewStatus { get; set; }
        public int Rating { get; set; }
        public int TimeLength { get; set; }
        public int Integral { get; set; }
        public int TotalScore { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public int ClickCount { get; set; }

        public int ManualClickCount { get; set; }//手动点击次数
        
        public int Sort { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public ICollection<MuKeCourseCapter> MetaData { get; set; }

        public ICollection<MuKeCourseCapterTestPagperManager> MuKeCourseCapterTestPagperManagers { get; set; }


        public MuKeCourseDragType MuKeCourseDragType { get; set; }//允许拖拽 播放
        public MuKeCourseCacheType MuKeCourseCacheType { get; set; }//允许缓存

        public MuKeCourseNewNoticeType MuKeCourseNewNoticeType { get; set; }

//        public bool IsNotSet  { get; set; }
//        public bool IsFree { get; set; } 
        public bool IsGlod  { get; set; }
        public bool IsWhiteGlod  { get; set; }
        public bool IsDelete { get; set; }//是否删除

    }

}
