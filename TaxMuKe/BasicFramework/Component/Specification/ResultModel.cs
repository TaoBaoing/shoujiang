using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BasicFramework.Component
{
    [Serializable]
    public class ResultModel
    {
        public ResultModel()
        {

        }
        public ResultModel(bool isSuccess, int code, string message)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
        }
        public ResultModel(bool isSucess)
            : this(isSucess, 0, string.Empty)
        {

        }
        public bool IsSuccess { get; set; }


        public int Code { get; set; }


        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{{\"IsSuccess\":{0},\"Code\":{1},\"Message\":\"{2}\"}}",
                                 IsSuccess, Code, Message);
        }
    }
}
