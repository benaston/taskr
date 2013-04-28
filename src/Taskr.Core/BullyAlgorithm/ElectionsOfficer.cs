using System;
using System.Collections.Generic;
using System.Linq;

namespace Taskr.Core.BullyAlgorithm
{
    public class ElectionsOfficer : IElectionsOfficer
    {
        public  ElectionResult HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates)
        {
            if (!localCandidate.IsLocal) { throw new Exception("localCandidate must be local"); }

            if (!localCandidate.MoreAuthoritativeCandidateIds.Any())
            {
                CoordinatorElectionScheduler.IsCoordinatorProcess = true;

                return ElectionResult.Conclusive;
            }

            foreach (var id in localCandidate.MoreAuthoritativeCandidateIds.OrderByDescending(i => i))
            {
                if (allCandidates[id].SendElectionRequest() == ElectionResult.Conclusive)
                {
                    CoordinatorElectionScheduler.IsCoordinatorProcess = false;

                    return ElectionResult.Conclusive;
                }
            }

            CoordinatorElectionScheduler.IsCoordinatorProcess = true;

            return ElectionResult.Conclusive;
        }
    }
}