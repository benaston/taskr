using System;
using System.Text;
using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;
using ZeroMQ;

namespace Taskr.Core
{
    public class ExampleElectionMessageServer
    {
        private const string ClusterMemberEndpointConfigurationKey = "ClusterMemberEndpoint";

        public static void StartMessageServer()
        {
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket server = context.CreateSocket(SocketType.REP))
            {
                server.ReceiveTimeout = TimeSpan.FromSeconds(LeadershipElectionScheduler.ElectionMessageReceiveTimeoutSeconds);
                server.Bind(new AppSettings()[ClusterMemberEndpointConfigurationKey]);

                for (;;)
                {
                    string message = server.Receive(Encoding.Unicode);
                    Console.WriteLine("Is Leader: {0}", LeadershipElectionScheduler.IsLeaderProcess);

                    if (message == LeadershipElectionScheduler.ElectionMessage)
                    {
                        server.Send(LeadershipElectionScheduler.ElectionConclusiveMessage, Encoding.Unicode);
                    }
                }
            }
        }
    }
}