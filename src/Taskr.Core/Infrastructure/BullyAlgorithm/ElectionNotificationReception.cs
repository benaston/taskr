using System;
using System.Text;
using ZeroMQ;

namespace Taskr.Core.Infrastructure.BullyAlgorithm
{
    /// <summary>
    /// Responsible for dealing with in-bound election notifications.
    /// </summary>
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
                    Console.WriteLine("Is Coordinator: {0}", CoordinatorElectionScheduler.IsCoordinatorProcess);

                    if (message == CoordinatorElectionScheduler.ElectionMessage)
                    {
                        server.Send(CoordinatorElectionScheduler.OkMessage, Encoding.Unicode);
                    }
                }
            }
        }
    }
}