using System;
using System.Collections.Generic;
using System.Reflection;
using HypernexSharp.Libs;
using HypernexSharp.Socketing.SocketMessages;
using HypernexSharp.Socketing.SocketResponses;

namespace HypernexSharp.Socketing
{
    public class GameServerSocket
    {
        private HypernexObject _hypernexObject;
        private FromGameServerMessage _fromGameServerMessage;
        private SocketInstance _socketInstance;

        public Action OnOpen = () => { };
        public Action<ISocketResponse> OnSocketEvent = response => { };
        public Action<bool> OnClose = hasError => { };

        public GameServerSocket(HypernexObject hypernexObject, string serverTokenContent)
        {
            _hypernexObject = hypernexObject;
            _fromGameServerMessage = new FromGameServerMessage(serverTokenContent);
            _hypernexObject.GetSocketInfo(result =>
            {
                if (result.success)
                {
                    _socketInstance = new SocketInstance(hypernexObject.Settings, result.result);
                    _socketInstance.OnConnect += OnOpen;
                    _socketInstance.OnMessage += node =>
                    {
                        try
                        {
                            string message = node["message"].Value;
                            switch (message.ToLower())
                            {
                                case "addedmoderator":
                                {
                                    AddedModerator addedModerator = new AddedModerator(node["result"]);
                                    OnSocketEvent.Invoke(addedModerator);
                                    break;
                                }
                                case "removedmoderator":
                                {
                                    RemovedModerator removedModerator = new RemovedModerator(node["result"]);
                                    OnSocketEvent.Invoke(removedModerator);
                                    break;
                                }
                                case "kickeduser":
                                {
                                    KickedUser kickedUser = new KickedUser(node["result"]);
                                    OnSocketEvent.Invoke(kickedUser);
                                    break;
                                }
                                case "banneduser":
                                {
                                    BannedUser bannedUser = new BannedUser(node["result"]);
                                    OnSocketEvent.Invoke(bannedUser);
                                    break;
                                }
                                case "unbanneduser":
                                {
                                    UnbannedUser unbannedUser = new UnbannedUser(node["result"]);
                                    OnSocketEvent.Invoke(unbannedUser);
                                    break;
                                }
                                case "selectedgameserver":
                                {
                                    SelectedGameServer selectedGameServer = new SelectedGameServer(node["result"]);
                                    OnSocketEvent.Invoke(selectedGameServer);
                                    break;
                                }
                                case "notselectedgameserver":
                                {
                                    NotSelectedGameServer notSelectedGameServer =
                                        new NotSelectedGameServer(node["result"]);
                                    OnSocketEvent.Invoke(notSelectedGameServer);
                                    break;
                                }
                                case "userleft":
                                {
                                    UserLeft userLeft = new UserLeft(node["result"]);
                                    OnSocketEvent.Invoke(userLeft);
                                    break;
                                }
                                case "updateinstance":
                                {
                                    UpdatedInstance updatedInstance = new UpdatedInstance(node["result"]);
                                    OnSocketEvent.Invoke(updatedInstance);
                                    break;
                                }
                                case "requestedinstancecreated":
                                {
                                    RequestedInstanceCreated requestedInstanceCreated =
                                        new RequestedInstanceCreated(node["result"]);
                                    OnSocketEvent.Invoke(requestedInstanceCreated);
                                    break;
                                }
                            }
                        }
                        catch (Exception) {}
                    };
                    _socketInstance.OnDisconnect += OnClose;
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

        public void AddModerator(string instanceId, string userId)
        {
            AddModerator addModerator = new AddModerator
            {
                InstanceId = instanceId,
                UserId = userId
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(addModerator).GetJSON());
        }
        
        public void RemoveModerator(string instanceId, string userId)
        {
            RemoveModerator removeModerator = new RemoveModerator
            {
                InstanceId = instanceId,
                UserId = userId
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(removeModerator).GetJSON());
        }
        
        public void KickUser(string instanceId, string userId)
        {
            KickUser kickUser = new KickUser
            {
                InstanceId = instanceId,
                UserId = userId
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(kickUser).GetJSON());
        }

        public void BanUser(string instanceId, string userId)
        {
            BanUser banUser = new BanUser
            {
                InstanceId = instanceId,
                UserId = userId
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(banUser).GetJSON());
        }
        
        public void UnbanUser(string instanceId, string userId)
        {
            UnbanUser unbanUser = new UnbanUser
            {
                InstanceId = instanceId,
                UserId = userId
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(unbanUser).GetJSON());
        }

        public void ClaimInstanceRequest(string temporaryId, Uri uri)
        {
            ClaimInstanceRequest claimInstanceRequest = new ClaimInstanceRequest
            {
                TemporaryId = temporaryId,
                Uri = uri
            };
            _socketInstance.SendMessage(_fromGameServerMessage.CreateMessage(claimInstanceRequest).GetJSON());
        }

        public bool Open() => _socketInstance.Open();
        public void Close() => _socketInstance.Close();
    }
}