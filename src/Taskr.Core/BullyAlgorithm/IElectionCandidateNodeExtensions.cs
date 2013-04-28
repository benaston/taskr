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
            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket client = context.CreateSocket(SocketType.REQ))
            {
                client.Connect(candidate.Uri);
                client.ReceiveTimeout = TimeSpan.FromSeconds(4);

                Console.WriteLine("Message sent: {0}", candidate.Uri);
                var status = client.Send("ELECTION", Encoding.Unicode);
                string reply = client.Receive(Encoding.Unicode);
                Console.WriteLine(@"Message received: {0} ({1}\)", reply ?? "<no reply>", candidate.Uri);
                client.Linger = TimeSpan.FromSeconds(1); //required
                client.Close(); //required

                return reply == "CONCLUSIVE" ? ElectionResult.Conclusive : ElectionResult.Inconclusive;
            }
        }

        //consider when election is already in progress
        public static ElectionResult HoldElection(this ICandidate candidate, IList<ICandidate> allCandidates)
        {
            if(!candidate.IsLocal)
            {
                throw new Exception("candidate must be local");
            }

            if (!candidate.AuthoritativeCandidateIds.Any())
            {
                LeadershipElectionScheduler.IsLeaderProcess = true;

                return ElectionResult.Conclusive;
            }

            foreach (var id in candidate.AuthoritativeCandidateIds.OrderByDescending(i => i))
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