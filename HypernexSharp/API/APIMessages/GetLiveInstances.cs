namespace HypernexSharp.API.APIMessages
{
    public class GetLiveInstances : APIMessage
    {
        private string endpoint = "instances/live/";
        protected override string Endpoint => endpoint;

        public GetLiveInstances(int itemsPerPage, int page) => endpoint += itemsPerPage + "/" + page;
    }
}