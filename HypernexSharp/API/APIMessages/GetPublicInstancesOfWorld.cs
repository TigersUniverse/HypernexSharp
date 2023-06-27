namespace HypernexSharp.API.APIMessages
{
    public class GetPublicInstancesOfWorld : APIMessage
    {
        private string endpoint;
        protected override string Endpoint => endpoint;

        public GetPublicInstancesOfWorld(string worldid) => endpoint += "instances/" + worldid;
    }
}