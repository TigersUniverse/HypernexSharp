using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Token
    {
        public string content { get; set; }
        public int dateCreated { get; set; }
        public int dateExpire { get; set; }
        public string app { get; set; }

        public static Token FromJSON(JSONNode node)
        {
            Token token = new Token
            {
                content = node["content"].Value,
                dateCreated = node["dateCreated"].AsInt,
                dateExpire = node["dateExpire"].AsInt
            };
            if (node.HasKey("app"))
                token.app = node["app"].Value;
            return token;
        }
    }
}