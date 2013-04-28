// ReSharper disable InconsistentNaming

using System;
using System.Text;
using ZeroMQ;

namespace Taskr.Core.BullyAlgorithm
{
    public static class ICandidateExtensions
    {
        public static ElectionResult SendElectionRequest(this ICandidate candidate)
        {
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket client = context.CreateSocket(SocketType.REQ))
            {
                client.Connect(candidate.Uri);
                client.ReceiveTimeout = TimeSpan.FromSeconds(CoordinatorElectionScheduler.ElectionMessageReceiveTimeoutSeconds);
                client.Send(CoordinatorElectionScheduler.ElectionMessage, Encoding.Unicode);
                var reply = client.Receive(Encoding.Unicode);
                client.Linger = TimeSpan.FromSeconds(CoordinatorElectionScheduler.ElectionMessageReceiveSocketLingerSeconds); //required
                client.Close(); //required

                return reply == CoordinatorElectionScheduler.ElectionConclusiveMessage ? ElectionResult.Conclusive : ElectionResult.Inconclusive;
            }
        }

        
    }
}
// ReSharper restore InconsistentNaming