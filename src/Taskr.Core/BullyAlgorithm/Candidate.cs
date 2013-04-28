using System.Collections.Generic;

namespace Taskr.Core.BullyAlgorithm
{
    /// <summary>
    /// Models a candidate in the election for the coordinator.
    /// </summary>
    public class Candidate : ICandidate
    {
        public Candidate(int id, 
                         string uri, 
                         bool isLocal, 
                         IEnumerable<int> authoritativeCandidates)
        {
            Id = id;
            Uri = uri;
            IsLocal = isLocal;
            MoreAuthoritativeCandidateIds = authoritativeCandidates;
        }

        public int Id { get; set; }

        public string Uri { get; set; }

        public bool IsLocal { get; set; }

        public IEnumerable<int> MoreAuthoritativeCandidateIds { get; set; }
    }
}