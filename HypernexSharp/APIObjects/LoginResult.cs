namespace HypernexSharp.APIObjects
{
    public enum LoginResult
    {
        Incorrect = 0,
        Missing2FA = 1,
        Banned = 2,
        Warned = 3,
        Correct = 4
    }
}