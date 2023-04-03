using System;
using System.Collections.Generic;
using System.Reflection;
using HypernexSharp.APIObjects;
using SimpleJSON;
using HypernexSharp.Socketing.SocketMessages;
using HypernexSharp.Socketing.SocketResponses;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing
{
    public class UserSocket
    {
        private HypernexObject _hypernexObject;
        private FromUserMessage _fromUserMessage;
        private SocketInstance _socketInstance;
        
        public bool IsOpen => _socketInstance?.IsOpen ?? false;
        public Action OnOpen = () => { };
        public Action<ISocketResponse> OnSocketEvent = response => { };
        public Action<bool> OnClose = hasError => { };

        internal UserSocket(HypernexObject hypernexObject, Action onReady = null)
        {
            _hypernexObject = hypernexObject;
            _fromUserMessage =
                new FromUserMessage(hypernexObject.Settings.UserId, _hypernexObject.Settings.TokenContent);
            _hypernexObject.GetSocketInfo(result =>
            {
                if (result.success)
                {
                    _socketInstance = new SocketInstance(hypernexObject.Settings, result.result);
                    _socketInstance.OnConnect += () =>
                    {
                        _socketInstance.SendMessage(_fromUserMessage.CreateMessage(new EmptyAuth()).GetJSON());
                        OnOpen.Invoke();
                    };
                    _socketInstance.OnMessage += node =>
                    {
                        try
                        {
                            string message = node["message"].Value;
                            switch (message.ToLower())
                            {
                                case "joinedinstance":
                                {
                                    JoinedInstance joinedInstance = new JoinedInstance(node["result"]);
                                    OnSocketEvent.Invoke(joinedInstance);
                                    break;
                                }
                                case "failedtojoininstance":
                                {
                                    FailedToJoinInstance failedToJoinInstance =
                                        new FailedToJoinInstance(node["result"]);
                                    OnSocketEvent.Invoke(failedToJoinInstance);
                                    break;
                                }
                                case "leftinstance":
                                {
                                    LeftInstance leftInstance = new LeftInstance(node["result"]);
                                    OnSocketEvent.Invoke(leftInstance);
                                    break;
                                }
                                case "failedtoleaveinstance":
                                {
                                    FailedToLeaveInstance failedToLeaveInstance =
                                        new FailedToLeaveInstance(node["result"]);
                                    OnSocketEvent.Invoke(failedToLeaveInstance);
                                    break;
                                }
                                case "gotinvite":
                                {
                                    GotInvite gotInvite = new GotInvite(node["result"]);
                                    OnSocketEvent.Invoke(gotInvite);
                                    break;
                                }
                                case "sharedavatartoken":
                                {
                                    SharedAvatarToken sharedAvatarToken = new SharedAvatarToken(node["result"]);
                                    OnSocketEvent.Invoke(sharedAvatarToken);
                                    break;
                                }
                                case "failedtoshareavatartoken":
                                {
                                    FailedToShareAvatarToken failedToShareAvatarToken =
                                        new FailedToShareAvatarToken(node["result"]);
                                    OnSocketEvent.Invoke(failedToShareAvatarToken);
                                    break;
                                }
                                case "instanceopened":
                                {
                                    InstanceOpened instanceOpened = new InstanceOpened(node["result"]);
                                    OnSocketEvent.Invoke(instanceOpened);
                                    break;
                                }
                                case "createdtemporaryinstance":
                                case "failedtocreatetemporaryinstance":
                                    EmptyResult emptyResult = new EmptyResult(message);
                                    OnSocketEvent.Invoke(emptyResult);
                                    break;
                            }
                        }
                        catch (Exception) {}
                    };
                    _socketInstance.OnDisconnect += OnClose;
                    onReady?.Invoke();
                }
            });
        }
        
        public T TryParseData<T>(ISocketResponse instance) => (T) Convert.ChangeType(instance, typeof(T));

        public T TryParseData<T>(JSONNode result)
        {
            object instance = Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result)
            {
                FieldInfo fieldInfo = instance.GetType().GetField(keyValuePair.Key);
                if (fieldInfo != null)
                    fieldInfo.SetValue(instance, Convert.ChangeType(keyValuePair.Value, fieldInfo.FieldType));
            }
            return (T) instance;
        }

        public void JoinInstance(string gameServerId, string instanceId)
        {
            JoinInstance joinInstance = new JoinInstance
            {
                gameServerId = gameServerId,
                instanceId = instanceId
            };
            _socketInstance.SendMessage(_fromUserMessage.CreateMessage(joinInstance).GetJSON());
        }

        public void LeaveInstance(string gameServerId, string instanceId)
        {
            LeaveInstance leaveInstance = new LeaveInstance
            {
                gameServerId = gameServerId,
                instanceId = instanceId
            };
            _socketInstance.SendMessage(_fromUserMessage.CreateMessage(leaveInstance).GetJSON());
        }

        public void SendInvite(User targetUser, string gameServerId, string toInstanceId)
        {
            SendInvite sendInvite = new SendInvite
            {
                targetUserId = targetUser.Id,
                gameServerId = gameServerId,
                toInstanceId = toInstanceId
            };
            _socketInstance.SendMessage(_fromUserMessage.CreateMessage(sendInvite).GetJSON());
        }

        public void ShareAvatarToken(User targetUser, string avatarId, string avatarToken)
        {
            ShareAvatarToken shareAvatarToken = new ShareAvatarToken
            {
                targetUserId = targetUser.Id,
                avatarId = avatarId,
                avatarToken = avatarToken
            };
            _socketInstance.SendMessage(_fromUserMessage.CreateMessage(shareAvatarToken).GetJSON());
        }

        public void RequestNewInstance(WorldMeta worldMeta, InstancePublicity instancePublicity, InstanceProtocol instanceProtocol)
        {
            RequestNewInstance requestNewInstance = new RequestNewInstance
            {
                worldId = worldMeta.Id,
                instancePublicity = instancePublicity,
                instanceProtocol = instanceProtocol
            };
            _socketInstance.SendMessage(_fromUserMessage.CreateMessage(requestNewInstance).GetJSON());
        }

        public bool Open() => _socketInstance.Open();
        public void Close() => _socketInstance.Close();
    }
}