using System;
using System.IO;
using HypernexSharp.API;
using HypernexSharp.API.APIMessages;
using HypernexSharp.API.APIResults;
using HypernexSharp.APIObjects;
using HypernexSharp.Socketing;
using LoginResult = HypernexSharp.APIObjects.LoginResult;

namespace HypernexSharp
{
    public class HypernexObject
    {
        public HypernexSettings Settings { get; }

        public HypernexObject(HypernexSettings settings) => Settings = settings;

        private UserSocket _userSocket;
        private GameServerSocket _gameServerSocket;

        public void CreateUser(Action<CallbackResult<SignupResult>> callback)
        {
            CreateUser createUser =
                new CreateUser(Settings.Username, Settings.Password, Settings.Email, Settings.InviteCode);
            createUser.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    SignupResult signupResult = new SignupResult {UserData = User.FromJSON(result.result["UserData"])};
                    callback.Invoke(new CallbackResult<SignupResult>(true, result.message, signupResult));
                }
                else
                    callback.Invoke(new CallbackResult<SignupResult>(false, result.message, null));
            });
        }

        public void Login(Action<CallbackResult<API.APIResults.LoginResult>> callback)
        {
            if (Settings.isFromToken)
            {
                ValidateToken(validTokenResult =>
                {
                    if (validTokenResult.success && validTokenResult.result.isValidToken)
                    {
                        GetUser(new Token{content = Settings.TokenContent}, r =>
                        {
                            if (r.success)
                            {
                                API.APIResults.LoginResult loginResult = new API.APIResults.LoginResult
                                {
                                    BanStatus = r.result.UserData.BanStatus,
                                    WarnStatus = r.result.UserData.WarnStatus,
                                    Result = LoginResult.Correct
                                };
                                foreach (Token token in r.result.UserData.AccountTokens)
                                {
                                    if (token.content.Equals(Settings.TokenContent))
                                        loginResult.Token = token;
                                }
                                callback.Invoke(new CallbackResult<API.APIResults.LoginResult>(true, r.message, loginResult));
                            }
                            else
                                callback.Invoke(new CallbackResult<API.APIResults.LoginResult>(false, r.message, null));
                        });
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(validTokenResult.message))
                        {
                            API.APIResults.LoginResult loginResult = new API.APIResults.LoginResult
                                {Result = LoginResult.Incorrect};
                            callback.Invoke(new CallbackResult<API.APIResults.LoginResult>(false,
                                validTokenResult.message, loginResult));
                        }
                        else
                            callback.Invoke(
                                new CallbackResult<API.APIResults.LoginResult>(false, validTokenResult.message, null));
                    }
                }, Settings.UserId, Settings.TokenContent);
            }
            else
            {
                Login login = new Login(Settings.Username, Settings.Password, Settings.TwoFACode);
                login.SendRequest(Settings, result =>
                {
                    if (result.success)
                    {
                        API.APIResults.LoginResult loginResult = new API.APIResults.LoginResult
                            {Result = (LoginResult) result.result["LoginResult"].AsInt};
                        if (result.result.HasKey("WarnStatus"))
                            loginResult.WarnStatus = WarnStatus.FromJSON(result.result["WarnStatus"]);
                        if (result.result.HasKey("BanStatus"))
                            loginResult.BanStatus = BanStatus.FromJSON(result.result["BanStatus"]);
                        if (result.result.HasKey("token"))
                            loginResult.Token = Token.FromJSON(result.result["token"]);
                        callback.Invoke(new CallbackResult<API.APIResults.LoginResult>(true, result.message, loginResult));
                    }
                    else
                        callback.Invoke(new CallbackResult<API.APIResults.LoginResult>(false, result.message, null));
                });
            }
        }

        public void GetUser(Token token, Action<CallbackResult<GetUserResult>> callback)
        {
            GetUser getUser;
            if(Settings.isFromToken)
                getUser = new GetUser(token.content){userid = Settings.UserId};
            else
                getUser = new GetUser(token.content){username = Settings.Username};
            getUser.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    GetUserResult getUserResult = new GetUserResult
                        {UserData = User.FromJSON(result.result["UserData"])};
                    callback.Invoke(new CallbackResult<GetUserResult>(true, result.message, getUserResult));
                }
                else
                    callback.Invoke(new CallbackResult<GetUserResult>(false, result.message, null));
            });
        }

        public void GetUser(Action<CallbackResult<GetUserResult>> callback, string user, Token token = null,
            bool isUserId = false)
        {
            GetUser getUser = new GetUser(token?.content ?? "");
            if (isUserId)
                getUser.userid = user;
            else
                getUser.username = user;
            getUser.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    GetUserResult getUserResult = new GetUserResult
                        {UserData = User.FromJSON(result.result["UserData"])};
                    callback.Invoke(new CallbackResult<GetUserResult>(true, result.message, getUserResult));
                }
                else
                    callback.Invoke(new CallbackResult<GetUserResult>(false, result.message, null));
            });
        }

        public void IsInviteCodeRequired(Action<CallbackResult<InviteCodeRequiredResult>> callback)
        {
            IsInviteCodeRequired isInviteCodeRequired = new IsInviteCodeRequired();
            isInviteCodeRequired.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    InviteCodeRequiredResult inviteCodeRequiredResult = new InviteCodeRequiredResult
                        {inviteCodeRequired = result.result["inviteCodeRequired"].AsBool};
                    callback.Invoke(new CallbackResult<InviteCodeRequiredResult>(true, result.message, inviteCodeRequiredResult));
                }
                else
                    callback.Invoke(new CallbackResult<InviteCodeRequiredResult>(false, result.message, null));
            });
        }
        
        public void GetSocketInfo(Action<CallbackResult<GetSocketInfoResult>> callback)
        {
            GetSocketInfo getSocketInfo = new GetSocketInfo();
            getSocketInfo.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    GetSocketInfoResult getSocketInfoResult = new GetSocketInfoResult
                    {
                        IsWSS = result.result["IsWSS"].AsBool,
                        Port = result.result["Port"].AsInt
                    };
                    callback.Invoke(new CallbackResult<GetSocketInfoResult>(true, result.message, getSocketInfoResult));
                }
                else
                    callback.Invoke(new CallbackResult<GetSocketInfoResult>(false, result.message, null));
            });
        }

        public void DoesUserExist(Action<CallbackResult<DoesUserExistResult>> callback, string userid)
        {
            DoesUserExist doesUserExist = new DoesUserExist {userid = userid};
            doesUserExist.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    DoesUserExistResult doesUserExistResult = new DoesUserExistResult
                        {doesUserExist = result.result["doesUserExist"].AsBool};
                    callback.Invoke(new CallbackResult<DoesUserExistResult>(true, result.message, doesUserExistResult));
                }
                else
                    callback.Invoke(new CallbackResult<DoesUserExistResult>(false, result.message, null));
            });
        }

        public void Logout(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token deAuthToken) =>
            Logout(callback, CurrentUser.Id, deAuthToken);
        
        public void Logout(Action<CallbackResult<EmptyResult>> callback, string userId, Token deAuthToken)
        {
            SimpleUserIdToken logout = new SimpleUserIdToken("logout", userId, deAuthToken.content);
            logout.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void ValidateToken(Action<CallbackResult<IsValidTokenResult>> callback, User CurrentUser, Token token)
        {
            IsValidToken isValidToken = new IsValidToken(token.content);
            if (!string.IsNullOrEmpty(CurrentUser.Id))
                isValidToken.userid = CurrentUser.Id;
            else if (!string.IsNullOrEmpty(CurrentUser.Username))
                isValidToken.username = CurrentUser.Username;
            isValidToken.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    IsValidTokenResult isValidTokenResult = new IsValidTokenResult
                        {isValidToken = result.result["isValidToken"].AsBool};
                    callback.Invoke(new CallbackResult<IsValidTokenResult>(true, result.message, isValidTokenResult));
                }
                else
                    callback.Invoke(new CallbackResult<IsValidTokenResult>(false, result.message, null));
            });
        }
        
        public void ValidateToken(Action<CallbackResult<IsValidTokenResult>> callback, string userId, string tokenContent)
        {
            IsValidToken isValidToken = new IsValidToken(tokenContent){userid = userId};
            isValidToken.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    IsValidTokenResult isValidTokenResult = new IsValidTokenResult
                        {isValidToken = result.result["isValidToken"].AsBool};
                    callback.Invoke(new CallbackResult<IsValidTokenResult>(true, result.message, isValidTokenResult));
                }
                else
                    callback.Invoke(new CallbackResult<IsValidTokenResult>(false, result.message, null));
            });
        }

        public void SendVerificationEmail(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token)
        {
            SimpleUserIdToken sendVerificationEmail =
                new SimpleUserIdToken("sendVerificationEmail", CurrentUser.Id, token.content);
            sendVerificationEmail.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void VerifyEmailToken(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string emailToken)
        {
            VerifyEmailToken verifyEmailToken = new VerifyEmailToken(CurrentUser.Id, token.content, emailToken);
            verifyEmailToken.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void ChangeEmail(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string newEmail)
        {
            ChangeEmail changeEmail = new ChangeEmail(CurrentUser.Id, token.content, newEmail);
            changeEmail.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void Enable2FA(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token)
        {
            SimpleUserIdToken enable2fa = new SimpleUserIdToken("enable2fa", CurrentUser.Id, token.content);
            enable2fa.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void Verify2FA(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string code)
        {
            Verify2FA verify2FA = new Verify2FA(CurrentUser.Id, token.content, code);
            verify2FA.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void Remove2FA(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token)
        {
            SimpleUserIdToken remove2fa = new SimpleUserIdToken("remove2fa", CurrentUser.Id, token.content);
            remove2fa.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void RequestPasswordReset(Action<CallbackResult<EmptyResult>> callback, string email)
        {
            RequestPasswordReset requestPasswordReset = new RequestPasswordReset(email);
            requestPasswordReset.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void ResetPassword(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string newPassword)
        {
            ResetPassword resetPassword = new ResetPassword(CurrentUser.Id, newPassword, token.content);
            resetPassword.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void ResetPassword(Action<CallbackResult<EmptyResult>> callback, string userid,
            string passwordResetContent, string newPassword)
        {
            ResetPassword resetPassword =
                new ResetPassword(userid, newPassword, passwordResetContent: passwordResetContent);
            resetPassword.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void UpdateBio(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token, Bio bio)
        {
            UpdateBio updateBio = new UpdateBio(CurrentUser.Id, token.content, bio);
            updateBio.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void BlockUser(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId blockUser =
                new SimpleTargetUserId("blockUser", CurrentUser.Id, token.content, targetUserId);
            blockUser.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void UnblockUser(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId unblockUser =
                new SimpleTargetUserId("unblockUser", CurrentUser.Id, token.content, targetUserId);
            unblockUser.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void FollowUser(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId followUser =
                new SimpleTargetUserId("followUser", CurrentUser.Id, token.content, targetUserId);
            followUser.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void UnfollowUser(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId unfollowUser =
                new SimpleTargetUserId("unfollowUser", CurrentUser.Id, token.content, targetUserId);
            unfollowUser.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void SendFriendRequest(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId friendRequest =
                new SimpleTargetUserId("sendFriendRequest", CurrentUser.Id, token.content, targetUserId);
            friendRequest.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void AcceptFriendRequest(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId friendRequest =
                new SimpleTargetUserId("acceptFriendRequest", CurrentUser.Id, token.content, targetUserId);
            friendRequest.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void DeclineFriendRequest(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId friendRequest =
                new SimpleTargetUserId("declineFriendRequest", CurrentUser.Id, token.content, targetUserId);
            friendRequest.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void RemoveFriend(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string targetUserId)
        {
            SimpleTargetUserId removeFriend =
                new SimpleTargetUserId("removeFriend", CurrentUser.Id, token.content, targetUserId);
            removeFriend.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        private void Upload(Action<CallbackResult<UploadResult>> callback, Upload upload)
        {
            upload.SendForm(Settings, result =>
            {
                if (result.success)
                {
                    UploadResult uploadResult = new UploadResult
                        {UploadData = FileData.FromJSON(result.result["UploadData"])};
                    if (result.result.HasKey("AvatarId"))
                        uploadResult.AvatarId = result.result["AvatarId"].Value;
                    if (result.result.HasKey("WorldId"))
                        uploadResult.AvatarId = result.result["WorldId"].Value;
                    callback.Invoke(new CallbackResult<UploadResult>(true, result.message, uploadResult));
                }
                else
                    callback.Invoke(new CallbackResult<UploadResult>(false, result.message, null));
            });
        }

        public void UploadFile(Action<CallbackResult<UploadResult>> callback, User CurrentUser, Token token,
            FileStream stream)
        {
            Upload upload = new Upload(CurrentUser.Id, token.content, stream);
            Upload(callback, upload);
        }
        
        public void UploadAvatar(Action<CallbackResult<UploadResult>> callback, User CurrentUser, Token token,
            FileStream stream, AvatarMeta avatarMeta)
        {
            Upload upload = new Upload(CurrentUser.Id, token.content, stream, avatarMeta);
            Upload(callback, upload);
        }
        
        public void UploadWorld(Action<CallbackResult<UploadResult>> callback, User CurrentUser, Token token,
            FileStream stream, WorldMeta worldMeta)
        {
            Upload upload = new Upload(CurrentUser.Id, token.content, stream, worldMeta);
            Upload(callback, upload);
        }

        public void RemoveAvatar(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string avatarId)
        {
            Remove remove = new Remove(UploadType.Avatar, CurrentUser.Id, token.content, avatarId);
            remove.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void RemoveWorld(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string worldId)
        {
            Remove remove = new Remove(UploadType.World, CurrentUser.Id, token.content, worldId);
            remove.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }
        
        public void RemoveFile(Action<CallbackResult<EmptyResult>> callback, User CurrentUser, Token token,
            string fileId)
        {
            Remove remove = new Remove(UploadType.Media, CurrentUser.Id, token.content, fileId);
            remove.SendRequest(Settings, result =>
            {
                if (result.success)
                    callback.Invoke(new CallbackResult<EmptyResult>(true, result.message, new EmptyResult()));
                else
                    callback.Invoke(new CallbackResult<EmptyResult>(false, result.message, null));
            });
        }

        public void Search(Action<CallbackResult<SearchResult>> callback, SearchType searchType, string searchTerm)
        {
            Search search = new Search(searchType, searchTerm);
            search.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    SearchResult searchResult = new SearchResult(result.result["Candidates"].AsArray);
                    callback.Invoke(new CallbackResult<SearchResult>(true, result.message, searchResult));
                }
                else
                    callback.Invoke(new CallbackResult<SearchResult>(false, result.message, null));
            });
        }

        public void GetFile(Action<Stream> callback, string uploaderUserId, string fileId)
        {
            GetFile getFile = new GetFile(uploaderUserId, fileId);
            getFile.GetAttachment(Settings, callback);
        }
        
        public void GetFile(Action<Stream> callback, string uploaderUserId, string fileId, string fileToken)
        {
            GetFile getFile = new GetFile(uploaderUserId, fileId, fileToken);
            getFile.GetAttachment(Settings, callback);
        }
        
        public void GetAvatarMeta(Action<CallbackResult<MetaCallback<AvatarMeta>>> callback, string avatarId)
        {
            GetMeta getMeta = new GetMeta(UploadType.Avatar, avatarId);
            getMeta.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    MetaCallback<AvatarMeta> metaCallback = new MetaCallback<AvatarMeta>
                        {Meta = AvatarMeta.FromJSON(result.result["Meta"])};
                    callback.Invoke(new CallbackResult<MetaCallback<AvatarMeta>>(true, result.message, metaCallback));
                }
                else
                    callback.Invoke(new CallbackResult<MetaCallback<AvatarMeta>>(false, result.message, null));
            });
        }

        public void GetWorldMeta(Action<CallbackResult<MetaCallback<WorldMeta>>> callback, string worldId)
        {
            GetMeta getMeta = new GetMeta(UploadType.World, worldId);
            getMeta.GetRequest(Settings, result =>
            {
                if (result.success)
                {
                    MetaCallback<WorldMeta> metaCallback = new MetaCallback<WorldMeta>
                        {Meta = WorldMeta.FromJSON(result.result["Meta"])};
                    callback.Invoke(new CallbackResult<MetaCallback<WorldMeta>>(true, result.message, metaCallback));
                }
                else
                    callback.Invoke(new CallbackResult<MetaCallback<WorldMeta>>(false, result.message, null));
            });
        }

        public void AddAssetToken(Action<CallbackResult<ManageAssetTokenResult>> callback, User CurrentUser,
            Token token, string assetId)
        {
            ManageAssetToken manageAssetToken =
                new ManageAssetToken(CurrentUser.Id, token.content, ManageAssetTokenAction.Add, assetId);
            manageAssetToken.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    ManageAssetTokenResult manageAssetTokenResult = new ManageAssetTokenResult();
                    if (result.result["token"] != null)
                        manageAssetTokenResult.token = result.result["token"].Value;
                    callback.Invoke(
                        new CallbackResult<ManageAssetTokenResult>(true, result.message, manageAssetTokenResult));
                }
                else
                    callback.Invoke(new CallbackResult<ManageAssetTokenResult>(false, result.message, null));
            });
        }
        
        public void RemoveAssetToken(Action<CallbackResult<ManageAssetTokenResult>> callback, User CurrentUser,
            Token token, string assetId, string removeAssetToken)
        {
            ManageAssetToken manageAssetToken =
                new ManageAssetToken(CurrentUser.Id, token.content, ManageAssetTokenAction.Remove, assetId)
                    {removeAssetToken = removeAssetToken};
            manageAssetToken.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    ManageAssetTokenResult manageAssetTokenResult = new ManageAssetTokenResult
                        {token = removeAssetToken};
                    callback.Invoke(
                        new CallbackResult<ManageAssetTokenResult>(true, result.message, manageAssetTokenResult));
                }
                else
                    callback.Invoke(new CallbackResult<ManageAssetTokenResult>(false, result.message, null));
            });
        }

        public void GetInstances(Action<CallbackResult<InstancesResult>> callback, User CurrentUser, Token token)
        {
            SimpleUserIdToken instances = new SimpleUserIdToken("instances", CurrentUser.Id, token.content);
            instances.SendRequest(Settings, result =>
            {
                if (result.success)
                {
                    InstancesResult instancesResult = new InstancesResult(result.result);
                    callback.Invoke(new CallbackResult<InstancesResult>(true, result.message, instancesResult));
                }
                else
                    callback.Invoke(new CallbackResult<InstancesResult>(false, result.message, null));
            });
        }

        private bool canOpenSocket() => _userSocket == null && _gameServerSocket == null;

        public UserSocket OpenUserSocket(Action readyToOpen = null, bool openWhenReady = true)
        {
            if (canOpenSocket())
            {
                _userSocket = new UserSocket(this, () =>
                {
                    if (openWhenReady)
                        _userSocket.Open();
                    readyToOpen?.Invoke();
                });
                return _userSocket;
            }
            return null;
        }

        public void CloseUserSocket()
        {
            _userSocket?.Close();
            _userSocket = null;
        }

        public GameServerSocket OpenGameServerSocket(string serverTokenContent, Action readyToOpen = null,
            bool openWhenReady = true)
        {
            if (canOpenSocket())
            {
                _gameServerSocket = new GameServerSocket(this, serverTokenContent, () =>
                {
                    if (openWhenReady)
                        _gameServerSocket.Open();
                    readyToOpen?.Invoke();
                });
                return _gameServerSocket;
            }
            return null;
        }

        public void CloseGameServerSocket()
        {
            _gameServerSocket?.Close();
            _gameServerSocket = null;
        }
    }
}