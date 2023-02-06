namespace HypernexSharp.API.APIMessages
{
    public class Search : APIMessage
    {
        private string endpoint = "search/";
        protected override string Endpoint => endpoint;

        public Search(SearchType t, string searchTerms)
        {
            switch (t)
            {
                case SearchType.User:
                    endpoint += "user/";
                    break;
                case SearchType.Avatar:
                    endpoint += "avatar/";
                    break;
                case SearchType.World:
                    endpoint += "world/";
                    break;
            }
            endpoint += searchTerms;
        }
    }

    public enum SearchType
    {
        User,
        Avatar,
        World
    }
}