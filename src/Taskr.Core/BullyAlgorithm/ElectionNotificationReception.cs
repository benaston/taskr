using System;
using System.Text;
using Taskr.Core.Infrastructure;
using ZeroMQ;

namespace Taskr.Core.BullyAlgorithm
{
    public class ElectionNotificationReception
    {
        private const string ClusterMemberEndpointConfigurationKey = "ClusterMemberEndpoint";

        public static void Run()
        {
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket server = context.CreateSocket(SocketType.REP))
            {
                server.ReceiveTimeout = TimeSpan.FromSeconds(CoordinatorElectionScheduler.ElectionMessageReceiveTimeoutSeconds);
                server.Bind(new AppSettings()[ClusterMemberEndpointConfigurationKey]);

                for (;;)
                {
                    var message = server.Receive(Encoding.Unicode);
                    Console.WriteLine("Is Leader: {0}", CoordinatorElectionScheduler.IsCoordinatorProcess);

                    if (message == CoordinatorElectionScheduler.ElectionMessage)
                    {
                        server.Send(CoordinatorElectionScheduler.OnlineMessage, Encoding.Unicode);
                    }
                }
            }
        }
    }
}