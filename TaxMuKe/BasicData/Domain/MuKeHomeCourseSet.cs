using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
    public  class MuKeHomeCourseSet
    {
        public long Id { get; set; }

        public long HomeReCourse1Id { get; set; }//首页推荐课程1
        public virtual MuKeCourse HomeReCourse1 { get; set; }
        public long HomeReCourse2Id { get; set; }
        public virtual MuKeCourse HomeReCourse2 { get; set; }
        public long HomeReCourse3Id { get; set; }
        public virtual MuKeCourse HomeReCourse3 { get; set; }
        public long HomeReCourse4Id { get; set; }
        public virtual MuKeCourse HomeReCourse4 { get; set; }
        public long HomeHotCourse1Id { get; set; }//首页热门课程1
        public virtual MuKeCourse HomeHotCourse1 { get; set; }//首页热门课程1
        public long HomeHotCourse2Id { get; set; }
        public virtual MuKeCourse HomeHotCourse2 { get; set; }
        public long HomeHotCourse3Id { get; set; }
        public virtual MuKeCourse HomeHotCourse3 { get; set; }
        public long HomeHotCourse4Id { get; set; }
        public virtual MuKeCourse HomeHotCourse4 { get; set; }
        public long PersonCourse1Id { get; set; }//个人中心课程
        public virtual MuKeCourse PersonCourse1 { get; set; }//个人中心课程
        public long PersonCourse2Id { get; set; }
        public virtual MuKeCourse PersonCourse2 { get; set; }
        public long PersonCourse3Id { get; set; }
        public virtual MuKeCourse PersonCourse3 { get; set; }
        public long PersonCourse4Id { get; set; }
        public virtual MuKeCourse PersonCourse4 { get; set; }

    }
}
