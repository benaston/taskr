using System;
using System.Collections.Generic;

namespace Taskr.Core.Infrastructure.BullyAlgorithm
{
    public class ElectionsOfficerInstrumented : ElectionsOfficer
    {
        public override void HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates)
        {
            Console.WriteLine("Election instigated by '{0}'.", localCandidate.Uri);

            base.HoldElection(localCandidate, allCandidates);

            Console.WriteLine("Election instigated by '{0}' COMPLETED.", localCandidate.Uri);
        }

        public override CandidateStatus SendElectionNotification(ICandidate candidate) 
        {
            Console.WriteLine("Sending election notification to '{0}'.", candidate.Uri);

            return base.SendElectionNotification(candidate);
        }
    }
}