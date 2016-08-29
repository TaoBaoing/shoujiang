using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BasicData
{
    public enum TaxonomyTypes
    {
        /// <summary>
        ///  全部
        /// </summary>
        [Description("全部")]
        None = 0,

        /// <summary>
        ///   课程分类
        /// </summary>
        [Description("公开课分类")]
        Course = 1,
        /// <summary>
        ///   大学活动分类
        /// </summary>
        [Description("大学活动分类")]
        UniversityActivities = 2,
        /// <summary>
        ///   学习项目分类
        /// </summary>
        [Description("学习项目分类")]
        LearningProjects = 3,


        /// <summary>
        ///  图片分类
        /// </summary>
        [Description("图片分类")]
        Images = 5,

        /// <summary>
        ///语音分类 
        /// </summary>
        [Description("语音分类")]
        Audio = 6,

        /// <summary>
        ///   视频分类
        /// </summary>
        [Description("视频分类")]
        Video = 7,

        /// <summary>
        ///  其他文件分类
        /// </summary>
        [Description("其他文件分类")]
        OtherFile = 8,

        /// <summary>
        ///  杂志分类
        /// </summary>
        [Description("杂志分类")]
        Magazine = 9
    }
}
