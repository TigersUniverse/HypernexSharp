namespace HypernexSharp.API
{
    public class CallbackResult<T>
    {
        public bool success { get; }
        public string message { get; }
        public T result { get; }

        public CallbackResult(bool s, string m, T r)
        {
            success = s;
            message = m;
            result = r;
        }
    }
}