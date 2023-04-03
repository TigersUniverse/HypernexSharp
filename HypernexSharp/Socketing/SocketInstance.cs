using System;
using HypernexSharp.API.APIResults;
using SimpleJSON;
using WebSocketSharp;

namespace HypernexSharp.Socketing
{
    internal class SocketInstance
    {
        private WebSocket _socket;

        public bool IsOpen => _socket?.IsAlive ?? false;
        public Action OnConnect { get; set; } = () => { };
        public Action<JSONNode> OnMessage { get; set; } = node => { };
        public Action<bool> OnDisconnect { get; set; } = wasError => { };

        public SocketInstance(HypernexSettings settings, GetSocketInfoResult socketInfo)
        {
            string c = settings.TargetDomain + ":" + socketInfo.Port;
            using (_socket = new WebSocket(socketInfo.IsWSS ? "wss://" + c : "ws://" + c))
            {
                _socket.OnOpen += (sender, args) => OnConnect.Invoke();
                _socket.OnMessage += (sender, args) =>
                {
                    try
                    {
                        JSONNode node = JSONNode.Parse(args.Data);
                        OnMessage.Invoke(node);
                    }
                    catch (Exception){}
                };
                _socket.OnClose += (sender, args) => OnDisconnect.Invoke(false);
                _socket.OnError += (sender, args) => OnDisconnect.Invoke(true);
            }
        }

        public bool Open()
        {
            _socket.Connect();
            return IsOpen;
        }
        public void SendMessage(JSONNode node) => _socket.Send(node.ToString());
        public void Close() => _socket.Close();
    }
}