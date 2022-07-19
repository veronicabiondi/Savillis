namespace Savillis.Library.Result
{
    public abstract class BaseResult
    {
        public abstract ErrorMessage Error { get; }
        public abstract int StatusCode { get; }
        public bool IsSuccessful => StatusCode is >= 200 and < 300;
    }

    public abstract class BaseResult<T> : BaseResult
    {
        public virtual T Value { get; protected set; }
    }
}