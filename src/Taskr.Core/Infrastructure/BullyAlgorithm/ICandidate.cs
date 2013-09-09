using System.Collections.Generic;

namespace Taskr.Core.Infrastructure.BullyAlgorithm
{
    public interface ICandidate
    {
        int Id { get; set; }

        string Uri { get; set; }

        bool IsLocal { get; set; }

        /// <summary>
        /// The collection of "bigger bullies" than this candidate.
        /// </summary>
        IEnumerable<int> MoreAuthoritativeCandidateIds { get; set; }
    }
}