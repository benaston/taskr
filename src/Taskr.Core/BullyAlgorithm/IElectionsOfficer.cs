using System.Collections.Generic;

namespace Taskr.Core.BullyAlgorithm
{
    public interface IElectionsOfficer
    {
        ElectionResult HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates);
    }
}