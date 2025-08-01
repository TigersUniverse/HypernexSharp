namespace HypernexSharp.API.APIMessages
{
    public class GetPublicWorldInstances : APIMessage
    {
        private string endpoint;
        protected override string Endpoint => endpoint;

        public GetPublicWorldInstances(string worldid) => endpoint += "instances/" + worldid;
    }
}