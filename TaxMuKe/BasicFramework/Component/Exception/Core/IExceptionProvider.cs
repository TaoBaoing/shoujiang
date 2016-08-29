namespace BasicFramework.Component
{
    public interface IExceptionProvider
    {
        BusinessException Throw(int exceptionId, params object[] args);
    }
}
