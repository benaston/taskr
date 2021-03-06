using System.Collections.Generic;

namespace Taskr.Core.Infrastructure.BullyAlgorithm
{
    public interface IElectionsOfficer
    {
        void HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates);

        CandidateStatus SendElectionNotification(ICandidate candidate);
    }
}