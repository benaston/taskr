using System;
using System.Text;
using System.Threading;
using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;
using ZeroMQ;

namespace Taskr.Core
{
    public class Foo
    {
        public static void StartMessageServer()
        {
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket server = context.CreateSocket(SocketType.REP))
            {
                server.ReceiveTimeout = TimeSpan.FromSeconds(4);
                server.Bind(new AppSettings()["ClusterMemberEndpoint"]);

                for (; ; )
                {
                    // Wait for next request from client
                    string message = server.Receive(Encoding.Unicode);
                    Console.WriteLine(@"Message received: {0}", message ?? "<no reply>");
                    Console.WriteLine("Is Leader: {0}", LeadershipElectionScheduler.IsLeaderProcess);

                    // Do Some 'work'
                    Thread.Sleep(2000);

                    // Send reply back to client
                    if (message == "ELECTION")
                    {
                        server.Send("CONCLUSIVE", Encoding.Unicode);
                    }
                }
            }
        }
    }
}