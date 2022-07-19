namespace Savillis.Library.Result
{
    public class SuccessfulResult<T> : BaseResult<T>
    {
        public SuccessfulResult(T value) => Value = value;
        public override ErrorMessage Error => null!;
        public override int StatusCode => 200;
        public override T Value { get; protected set; }
    }
}