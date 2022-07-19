namespace Savillis.Library.Result
{
    public class FailureResult<T> : BaseResult<T>
    {
        public FailureResult(ErrorMessage message) => Error = message;
        public override ErrorMessage Error { get; }
        public override int StatusCode => 500;
    }
}