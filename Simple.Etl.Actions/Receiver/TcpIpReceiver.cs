using Simple.Etl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Etl.Actions.Receiver
{
    public class TcpIpReceiver : IReceiver<string>
    {
        private readonly Socket _listener;
        private readonly BackgroundWorker _worker;

        private  bool _isCanceled;
        public TcpIpReceiver()
        {
            _worker = new BackgroundWorker();
            IPEndPoint ipEndPoint = new(IPAddress.Any,1302);
            _listener = new(
                  ipEndPoint.AddressFamily,
                  SocketType.Stream,
                  ProtocolType.Tcp);
        }
        public Action<string> ReceivedData { set; private get; }

        public Task ShutDown()
        {
            _listener.Stop();
            _isCanceled = true ;
            return Task.CompletedTask;
        }
     
        public async Task Startup()
        {
            _listener.Start();
            _worker.DoWork += Listen;
            _worker.RunWorkerAsync();
            var client = await _listener.AcceptTcpClientAsync();
        }
        private string ReadStream(NetworkStream stream)
        {
            string str;
            byte[] data = new byte[1024];
            using MemoryStream ms = new MemoryStream();
            int numBytesRead;
            while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
            {
                ms.Write(data, 0, numBytesRead);
            }
            str = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
            return str;
        }
        private void Listen(object? sender, DoWorkEventArgs e)
        {
            Task.Run(async () =>
            {

                IPEndPoint ipEndPoint = new(IPAddress.Any, 1302);
                _listener.Bind(ipEndPoint);
                _listener.Listen(100);

                var handler = await _listener.AcceptAsync();
                while (true)
                {
                    handler.s

                }
            });

    }
}
