using System;
using HypernexSharp.APIObjects;
using LoginResult = HypernexSharp.APIObjects.LoginResult;

namespace HypernexSharp.Tests
{
    internal class Program
    {
        private const string DOMAIN = "hypernex.fortnite.lol";
        private static HypernexObject HypernexObject;
        private static User CurrentUser;
        
        public static void Main(string[] args)
        {
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
            HypernexSettings settings = new HypernexSettings(username, password, twofa){TargetDomain = DOMAIN};
            new HypernexSettings("h", email:"e", "e");
            HypernexObject = new HypernexObject(settings);
            HypernexObject.Login(r =>
            {
                if (r.success && r.result.Result == LoginResult.Correct)
                {
                    Console.WriteLine("Logged In! Getting Login User...");
                    HypernexObject.GetUser(r.result.Token, userR =>
                    {
                        if (userR.success)
                        {
                            CurrentUser = userR.result.UserData;
                            Console.WriteLine("Got LoginUser! Welcome, " + CurrentUser.Username);
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