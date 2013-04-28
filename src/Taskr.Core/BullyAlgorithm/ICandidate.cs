using System.Collections.Generic;

namespace Taskr.Core.BullyAlgorithm
{
    public interface ICandidate
    {
        int Id { get; set; }

        string Uri { get; set; }

        bool IsLocal { get; set; }

        /// <summary>
        /// These identify the "bigger bullies" than this candidate.
        /// </summary>
        IEnumerable<int> MoreAuthoritativeCandidateIds { get; set; }
    }
}