namespace BasicFramework.Component
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException
    {
        public int ExceptionCode { get; set; }
        public string ExceptionMessage { get; set; }
        public System.Exception InnerException { get; set; }
    }
}
