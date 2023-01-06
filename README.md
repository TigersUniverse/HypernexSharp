# HypernexSharp
A C# Wrapper to interface with Hypernex.API

## Authenticating

To get started, first you must know the **domain of where the user wants to connect to** and whether or not they have an account already.

For our examples, our domain will be `hypernex.fortnite.lol`

### If the User already has an Account

Initialize a HypernexSettings Object with their info

```csharp
HypernexSettings settings = new HypernexSettings("username", "password")
{
    TargetDomain = "hypernex.fortnite.lol"
};
```

then, create our HypernexObject from the HypernexSettings

```csharp
HypernexObject hypernexObject = new HypernexObject(settings);
```

#### To check if the user has a 2FA code

There is no way to tell if the user has 2FA or not until they have inserted their password correctly. Have the user login, then check the result to see if they're Missing 2FA, then prompt the user to input their 2FA, then overwrite the HypernexSettings object with the new 2FA code.

```csharp
// Pass to function with Callback, Username, Password, and an optional 2FA Code
void Login(Action<string, Token> callback, string username, string password)
{
    // Create Settings without 2FA
    HypernexSettings settings = new HypernexSettings(username, password)
    {
        TargetDomain = "hypernex.fortnite.lol"
    };
    // Create the HypernexObject from Settings
    HypernexObject hypernexObject = new HypernexObject(settings);
    // Login
    hypernexObject.Login(result => {
        if(r.success && r.result.Result == LoginResult.Correct)
        {
            // Logged In, get the Token
            Token token = r.result.Token;
            callback.Invoke(username, token);
        }
        else if(r.success && r.result.Result == LoginResult.Missing2FA)
        {
            // Did not input the 2FA code
            Get2FA(callback, username, password);
        }
    });
}

// Login with 2FA
void Get2FA(Action<string, Token> fromLoginCallback, string username, string password)
{
    // Get the 2FA code however you want to
    string twofacode = "000000";
    // Create Settings with 2FA
    HypernexSettings settings = new HypernexSettings(username, password, twofacode: twofacode)
    {
        TargetDomain = "hypernex.fortnite.lol"
    };
    // Create the HypernexObject from Settings
    HypernexObject hypernexObject = new HypernexObject(settings);
    // Login
    hypernexObject.Login(result => {
        if(r.success && r.result.Result == LoginResult.Correct)
        {
            // Logged In, get the Token
            Token token = r.result.Token;
            callback.Invoke(username, token);
        }
        else
        {
            // Wrong Password or Server Error
        }
    });
}
```

### If the User does not have an Account

A user cannot interact with the majority of Hypernex.API without an account, so make them create an account.

Initialize a HypernexSettings Object with what they want their account info to be.

```csharp
HypernexSettings settings = new HypernexSettings("username", email:"email", "password")
{
    TargetDomain = "hypernex.fortnite.lol"
};
```

If you require an invite code, be sure to pass an inviteCode

```csharp
HypernexSettings settings = new HypernexSettings("username", "email", "password", inviteCode: "")
{
    TargetDomain = "hypernex.fortnite.lol"
};
```

then, create our HypernexObject from the HypernexSettings

```csharp
HypernexObject hypernexObject = new HypernexObject(settings);
```

finally, create the User

```csharp
hypernexObject.CreateUser(r => {
    if(r.success)
    {
        // Get the User
        User user = r.result.UserData;
        // Get the AccountToken
        // Since we just created this account, there will only be one token
        Token token = user.AccountTokens.FirstOrDefault();
    }
    else
    {
        // Do something when you can't signup
    }
});
```

#### How to check if an InviteCode is required

For design, you may want to know if an InviteCode is required before signup, which this can be done in a couple of lines.

```csharp
new HypernexObject(new HypernexSettings()).IsInviteCodeRequired(r => {
    if(r.success)
    {
        bool required = r.result.inviteCodeRequired;
        // Do something with required
    }
    else
    {
        // Server failed to respond, do something
    }
});
```

## Design of the Library

HypernexSharp was designed to be a [Promise](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise)-like library, where no awaiting is required, just an Action callback.

All Actions that do not get Files will will have a type object of `CallbackResult<T>`, where T is the server's response deserialized. When an Action is getting a File, most likely the type object will just be a Stream.

Doing this allows for code to be easily multithreaded (if-needed) and usable in more scenarios.
