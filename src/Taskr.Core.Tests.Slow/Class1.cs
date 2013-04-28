using NUnit.Framework;
using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;

namespace Taskr.Core.Tests.Slow
{
    [TestFixture]
    public class LeaderElectorTests
    {
        public static object ElectionLock = new object();
        private IAppSettings _appSettings;

        [SetUp]
        public void SetUp()
        {
            var candidateFactory = new CandidateFactory(_appSettings);
            new LeadershipElectionScheduler(candidateFactory.Create(), ElectionLock);
        }

        [Test]
        public void Elect_AllNodesOK_SelectsEvenly()
        {
            
        }
    }
}
