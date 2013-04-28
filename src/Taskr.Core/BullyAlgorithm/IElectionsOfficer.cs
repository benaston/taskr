using System.Collections.Generic;

namespace Taskr.Core.BullyAlgorithm
{
    public interface IElectionsOfficer
    {
        void HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates);
    }
}