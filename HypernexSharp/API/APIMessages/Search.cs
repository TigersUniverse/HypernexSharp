namespace HypernexSharp.API.APIMessages
{
    public class Search : APIMessage
    {
        private string endpoint = "search/";
        protected override string Endpoint => endpoint;

        public Search(SearchType t, string searchTerms, int itemsPerPage = 50, int page = 0, bool isTag = false)
        {
            if (isTag)
                endpoint = "tag/";
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
            endpoint += searchTerms + "/";
            endpoint += itemsPerPage + "/";
            endpoint += page;
        }
    }

    public enum SearchType
    {
        User,
        Avatar,
        World
    }
}