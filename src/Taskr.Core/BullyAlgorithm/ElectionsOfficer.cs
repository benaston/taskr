using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroMQ;

namespace Taskr.Core.BullyAlgorithm
{
    public class ElectionsOfficer : IElectionsOfficer
    {
        public  void HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates)
        {
            if (!localCandidate.IsLocal) { throw new Exception("localCandidate must be local (i.e. associated with the current process)"); }

            if (!localCandidate.MoreAuthoritativeCandidateIds.Any())
            {
                CoordinatorElectionScheduler.IsCoordinatorProcess = true;

                return;
            }

            foreach (var id in localCandidate.MoreAuthoritativeCandidateIds.OrderByDescending(i => i))
            {
                if (SendElectionNotification(allCandidates[id]) == CandidateStatus.Online)
                {
                    CoordinatorElectionScheduler.IsCoordinatorProcess = false;

                    return;
                }
            }

            CoordinatorElectionScheduler.IsCoordinatorProcess = true;
        }

        public static CandidateStatus SendElectionNotification(ICandidate candidate)
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

                return reply == CoordinatorElectionScheduler.OnlineMessage ? CandidateStatus.Online : CandidateStatus.Offline;
            }
        }
    }
}