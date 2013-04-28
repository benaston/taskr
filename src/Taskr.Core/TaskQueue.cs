using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Taskr.Core
{
    public class TaskQueue
    {
        public void Start()
        {
            // ZMQ Context, server socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket server = context.CreateSocket(SocketType.REP))
            {
                server.Bind("tcp://*:5555");

                for (;;)
                {
                    // Wait for next request from client
                    string message = server.Receive(Encoding.Unicode);
                    Console.WriteLine("Received request: {0}", message);

                    // Do Some 'work'
                    Thread.Sleep(1000);

                    // Send reply back to client
                    server.Send("World", Encoding.Unicode);
                }
            }
        }
    }
}
