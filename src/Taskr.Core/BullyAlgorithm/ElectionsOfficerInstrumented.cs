using System;
using System.Collections.Generic;

namespace Taskr.Core.BullyAlgorithm
{
    public class ElectionsOfficerInstrumented : IElectionsOfficer
    {
        private readonly ElectionsOfficer _decoratedElectionsOfficer;

        public ElectionsOfficerInstrumented(ElectionsOfficer decoratedElectionsOfficer)
        {
            _decoratedElectionsOfficer = decoratedElectionsOfficer;
        }

        public void HoldElection(ICandidate localCandidate, IList<ICandidate> allCandidates)
        {
            Console.WriteLine("Election instigated by '{0}'.", localCandidate.Uri);

            _decoratedElectionsOfficer.HoldElection(localCandidate, allCandidates);

            Console.WriteLine("Election instigated by '{0}' COMPLETED.", localCandidate.Uri);
        }
    }
}