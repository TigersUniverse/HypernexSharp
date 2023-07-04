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

        private HypernexSettings s;
        private GetSocketInfoResult g;
        private bool isClosing;

        public SocketInstance(HypernexSettings settings, GetSocketInfoResult socketInfo)
        {
            s = settings;
            g = socketInfo;
            CreateSocket(settings, socketInfo);
        }

        private void d(bool error)
        {
            OnDisconnect -= d;
            CreateSocket(s, g, true);
        }

        private void CreateSocket(HypernexSettings settings, GetSocketInfoResult socketInfo, bool open = false)
        {
            string c = settings.TargetDomain + ":" + socketInfo.Port;
            _socket = new WebSocket(socketInfo.IsWSS ? "wss://" + c : "ws://" + c);
            if(socketInfo.IsWSS)
                _socket.SslConfiguration.EnabledSslProtocols =
                    (System.Security.Authentication.SslProtocols) (SslProtocols.Tls12 | SslProtocols.Tls11 |
                                                                   SslProtocols.Tls);
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
            OnDisconnect += d;
            if (open && !isClosing && settings.AutoReconnectSocket)
                Open();
        }

        public bool Open()
        {
            isClosing = false;
            _socket.Connect();
            return IsOpen;
        }
        public void SendMessage(JSONNode node) => _socket.Send(node.ToString());

        public void Close()
        {
            isClosing = true;
            _socket.Close();
        }
        
        [Flags]
        private enum SslProtocols
        {
            Tls = 192,
            Tls11 = 768,
            Tls12 = 3072
        }
    }
}