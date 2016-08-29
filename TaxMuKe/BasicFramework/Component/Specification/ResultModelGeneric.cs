using System.Runtime.Serialization;

namespace BasicFramework.Component
{
    public class ResultModel<T> : ResultModel
    {

        public T Data { get; set; }
 
        public ResultModel()
        {

        }

        public ResultModel(bool isSuccess, int code, string message, T data)
            : base(isSuccess, code, message)
        {
            Data = data;
        }

        public ResultModel(bool isSucess)
            : this(isSucess, 0, string.Empty, default(T))
        {

        }

        public override string ToString()
        {
            return string.Format(
                "{{\"IsSuccess\":{0},\"Code\":{1},\"Message\":\"{2}\",\"Data\":\"{3}\"}}",
                IsSuccess, Code, Message, Data);
        }
    }
}
