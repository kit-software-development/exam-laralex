﻿
using System.Net;
using System.Net.Sockets;
using CommonLibrary.Model.Networking;

using CommonLibrary.Implementation.Networking.Serializing;

namespace CommonLibrary.Implementation.Networking.Udp
{
    /// <summary>
    /// UdpClient connected to local UDP port to receive datagrams
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class UdpReceiver<E> : INetworkReceiver<E>
    {
        
        public IPEndPoint RemoteEndPoint => _remote_sender_end_point;
        public UdpReceiver(UdpClient client)
        {
            _udp_client = client;
        }

        public void Dispose()
        {
            _udp_client.Close();
        }

        public E Receive()
        {
            byte[] data = _udp_client.Receive(ref _remote_sender_end_point);
            return BinaryMessageFactory.Deserialize<E>(data);
        }

        private readonly UdpClient _udp_client;
        private IPEndPoint _remote_sender_end_point;

    }
}
