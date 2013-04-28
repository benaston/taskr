// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
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
                client.ReceiveTimeout = TimeSpan.FromSeconds(LeadershipElectionScheduler.ElectionMessageReceiveTimeoutSeconds);
                client.Send(LeadershipElectionScheduler.ElectionMessage, Encoding.Unicode);
                var reply = client.Receive(Encoding.Unicode);
                client.Linger = TimeSpan.FromSeconds(LeadershipElectionScheduler.ElectionMessageReceiveSocketLingerSeconds); //required
                client.Close(); //required

                return reply == LeadershipElectionScheduler.ElectionConclusiveMessage ? ElectionResult.Conclusive : ElectionResult.Inconclusive;
            }
        }

        public static ElectionResult HoldElection(this ICandidate candidate, IList<ICandidate> allCandidates)
        {
            if(!candidate.IsLocal) { throw new Exception("candidate must be local"); }

            if (!candidate.MoreAuthoritativeCandidateIds.Any())
            {
                LeadershipElectionScheduler.IsLeaderProcess = true;

                return ElectionResult.Conclusive;
            }

            foreach (var id in candidate.MoreAuthoritativeCandidateIds.OrderByDescending(i => i))
            {
                if (allCandidates[id].SendElectionRequest() == ElectionResult.Conclusive)
                {
                    LeadershipElectionScheduler.IsLeaderProcess = false;

                    return ElectionResult.Conclusive;
                }
            }

            LeadershipElectionScheduler.IsLeaderProcess = true;

            return ElectionResult.Conclusive;
        }
    }
}
// ReSharper restore InconsistentNaming