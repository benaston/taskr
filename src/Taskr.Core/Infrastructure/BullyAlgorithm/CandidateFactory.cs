using System;
using System.Collections.Generic;
using System.Linq;

namespace Taskr.Core.Infrastructure.BullyAlgorithm
{
    public class CandidateFactory
    {
        private readonly IAppSettings _appSettings;
        private const string ClusterMemberIdConfigurationKey = "ClusterMemberId";
        private const string ClusterMembersConfigurationKey = "ClusterMembers";
        private const char ClusterMemberDelimiter = ',';

        public CandidateFactory(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IList<ICandidate> Create()
        {
            var candidateUris = _appSettings[ClusterMembersConfigurationKey].Split(ClusterMemberDelimiter);
            var thisCandidateId = int.Parse(_appSettings[ClusterMemberIdConfigurationKey]);
            var candidates = new List<ICandidate>();

            var index = 0;
            foreach (var uri in candidateUris)
            {
                candidates.Add(new Candidate(index,
                                             uri,
                                             index == thisCandidateId,
                                             GetMoreAuthoritativeCandidateIds(index, candidateUris)
                                             .Where(i => i >= 0)));
                index++;
            }

            return candidates;
        }

        private IEnumerable<int> GetMoreAuthoritativeCandidateIds(int index, IEnumerable<string> candidateUris)
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