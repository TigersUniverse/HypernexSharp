namespace HypernexSharp.API.APIMessages
{
    public class EmptyGet : APIMessage
    {
        protected override string Endpoint { get; }

        public EmptyGet(string endpoint) => Endpoint = endpoint;
    }
}