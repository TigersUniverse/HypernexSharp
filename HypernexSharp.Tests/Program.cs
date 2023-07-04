using System;
using HypernexSharp.APIObjects;
using HypernexSharp.Socketing;
using LoginResult = HypernexSharp.APIObjects.LoginResult;

namespace HypernexSharp.Tests
{
    internal class Program
    {
        private const string DOMAIN = "localhost";
        private const bool IS_HTTP = true;
        private static HypernexObject HypernexObject;
        private static User CurrentUser;
        private static Token CurrentToken;
        
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
            {
                if(CurrentUser != null)
                    HypernexObject?.Logout(result => { Console.WriteLine("Logged Out!"); }, CurrentUser, CurrentToken);
            };
            AttemptLogin();
            Console.ReadKey(true);
        }

        private static void AttemptLogin(bool is2FA = false)
        {
            Console.WriteLine("Enter your Username");
            string username = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Enter your Password");
            string password = Console.ReadLine() ?? String.Empty;
            string twofa = String.Empty;
            if (is2FA)
                twofa = Console.ReadLine() ?? String.Empty;
            HypernexSettings settings = new HypernexSettings(username, password, twofa)
                {TargetDomain = DOMAIN, IsHTTP = IS_HTTP};
            HypernexObject = new HypernexObject(settings);
            HypernexObject.Login(r =>
            {
                if (r.success && r.result.Result == LoginResult.Correct)
                {
                    Console.WriteLine("Logged In! Getting Login User...");
                    CurrentToken = r.result.Token;
                    HypernexObject.GetUser(r.result.Token, userR =>
                    {
                        if (userR.success)
                        {
                            CurrentUser = userR.result.UserData;
                            Console.WriteLine("Got LoginUser! Welcome, " + CurrentUser.Username);
                            UserSocket socket =
                                HypernexObject.OpenUserSocket(CurrentUser, CurrentToken);
                            socket.OnOpen += () => Console.WriteLine("Socket Opened!");
                            socket.OnClose += b => Console.WriteLine("Socket Closed! DidError: " + b);
                        }
                        else
                            Console.Write("Failed to get Login User!");
                    });
                }
                else if (r.success && r.result.Result == LoginResult.Missing2FA)
                {
                    Console.WriteLine("Missing 2FA");
                    AttemptLogin(true);
                }
                else
                    Console.WriteLine("Failed to Login!");
            });
        }
    }
}