using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
   public  class MuKeCourseCapterTestPagper
    {
        public long Id { get; set; }

       public long MuKeCourseCapterTestPagperManagerId { get; set; }    
       public virtual MuKeCourseCapterTestPagperManager MuKeCourseCapterTestPagperManager { get; set; }    

       public int PagperId { get; set; }//题目编号

       public MuKeTestPagperType MuKeTestPagperType { get; set; }

       public string Title { get; set; }//题目

       public string AnswerA { get; set; } //A答案选项
       public bool AnswerAIsRight { get; set; }//A是否正确答案

       // [NotMapped]
        public bool AnswerAIsSelected { get; set; }//由前端输入用户是否选中该选项 不映射到数据库

        public string AnswerB { get; set; }//
        public bool AnswerBIsRight { get; set; }

       // [NotMapped]
        public bool AnswerBIsSelected { get; set; }//不映射到数据库

        public string AnswerC { get; set; }//
        public bool AnswerCIsRight { get; set; }

       // [NotMapped]
        public bool AnswerCIsSelected { get; set; }//不映射到数据库

        public string AnswerD { get; set; }//
        public bool AnswerDIsRight { get; set; }

        //[NotMapped]
        public bool AnswerDIsSelected { get; set; }//不映射到数据库

        public int Score { get; set; }//分数
    


    }

    public enum MuKeTestPagperType
    {
        [Description("单选")]
        Single =1,

        [Description("多选")]
        Multiple =2,

        [Description("判断")]
        Judge =3,
    }
}
