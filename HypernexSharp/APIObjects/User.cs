using System.Collections.Generic;
using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool isEmailVerified { get; set; }
        public List<Token> AccountTokens { get; set; } = new List<Token>();
        public bool is2FAVerified { get; set; }
        public List<string> BlockedUsers { get; set; }
        public List<string> Following { get; set; }
        public List<string> Followers { get; set; }
        public List<string> OutgoingFriendRequests { get; set; }
        public List<string> FriendRequests { get; set; }
        public List<string> Friends { get; set; }
        public Bio Bio { get; set; }
        public Rank Rank { get; set; }
        public int AccountCreationDate { get; set; }
        public BanStatus BanStatus { get; set; }
        public int BanCount { get; set; }
        public WarnStatus WarnStatus { get; set; }
        public int WarnCount { get; set; }

        public static User FromJSON(JSONNode node)
        {
            User user = new User
            {
                Id = node["Id"].Value,
                Username = node["Username"].Value,
                Bio = Bio.FromJSON(node["Bio"]),
                Rank = (Rank) node["Rank"].AsInt
            };
            if (node.HasKey("Email"))
                user.Email = node["Email"].Value;
            if (node.HasKey("isEmailVerified"))
                user.isEmailVerified = node["isEmailVerified"].AsBool;
            if (node.HasKey("AccountTokens"))
            {
                user.AccountTokens = new List<Token>();
                foreach (KeyValuePair<string, JSONNode> keyValuePair in node["AccountTokens"].AsArray)
                {
                    Token t = Token.FromJSON(keyValuePair.Value);
                    user.AccountTokens.Add(t);
                }
            }
            if (node.HasKey("is2FAVerified"))
                user.is2FAVerified = node["is2FAVerified"].AsBool;
            if (node.HasKey("BlockedUsers"))
            {
                user.BlockedUsers = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["BlockedUsers"].AsArray)
                    user.BlockedUsers.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("Following"))
            {
                user.Following = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Following"].AsArray)
                    user.Following.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("Followers"))
            {
                user.Followers = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Followers"].AsArray)
                    user.Followers.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("OutgoingFriendRequests"))
            {
                user.OutgoingFriendRequests = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["OutgoingFriendRequests"].AsArray)
                    user.OutgoingFriendRequests.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("FriendRequests"))
            {
                user.FriendRequests = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["FriendRequests"].AsArray)
                    user.FriendRequests.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("Friends"))
            {
                user.Friends = new List<string>();
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Friends"].AsArray)
                    user.Friends.Add(keyValuePair.Value.Value);
            }
            if (node.HasKey("AccountCreationDate"))
                user.AccountCreationDate = node["AccountCreationDate"].AsInt;
            if(node.HasKey("BanStatus"))
                user.BanStatus = BanStatus.FromJSON(node["BanStatus"]);
            if (node.HasKey("BanCount"))
                user.BanCount = node["BanCount"].AsInt;
            if(node.HasKey("WarnStatus"))
                user.WarnStatus = WarnStatus.FromJSON(node["WarnStatus"]);
            if (node.HasKey("WarnCount"))
                user.WarnCount = node["WarnCount"].AsInt;
            return user;
        }
    }
}