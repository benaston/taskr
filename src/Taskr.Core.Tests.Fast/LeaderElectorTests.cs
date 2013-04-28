using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;

namespace Taskr.Core.Tests.Fast
{
    public class LeaderElectorTests
    {
        public static object ElectionLock = new object();
        private IAppSettings _appSettings;

        public void Elect_AllNodesOK_SelectsEvenly()
        {
            var candidateFactory = new CandidateFactory(_appSettings);
            new LeadershipElectionScheduler(candidateFactory.Create(), ElectionLock);
        }
    }
}