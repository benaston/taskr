using System;
using System.Collections.Generic;
using System.Linq;
using Taskr.Core.Infrastructure;

namespace Taskr.Core.BullyAlgorithm
{
    public class CandidateFactory
    {
        private readonly IAppSettings _appSettings;

        public CandidateFactory(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IList<ICandidate> Create()
        {
            var candidateUris = _appSettings["ClusterMembers"].Split(',');
            var thisCandidateId = int.Parse(_appSettings["ClusterMemberId"]);
            var candidates = new List<ICandidate>();

            var index = 0;
            foreach (var uri in candidateUris)
            {
                candidates.Add(new Candidate(index,
                                             uri,
                                             index == thisCandidateId,
                                             GetAuthoritativeCandidateIds(index, candidateUris)
                                             .Where(i => i >= 0)));
                index++;
            }

            return candidates;
        }

        public IEnumerable<int> GetAuthoritativeCandidateIds(int index, IEnumerable<string> candidateUris)
        {
            return candidateUris.Select(n => {
                var i = Array.IndexOf<string>(candidateUris.ToArray(), n);
                if (i > index)
                {
                    return i;
                }

                return -1;
            });
        }
    }
}