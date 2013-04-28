using System.Threading;
using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;

namespace Taskr.Core.Tests.Slow.Process1
{
    class Program
    {
        public static object ElectionLock = new object();

        public static void Main(string[] args)
        {
            var t1 = new Thread(ElectionNotificationReception.Run);
            var t2 = new Thread(new CoordinatorElectionScheduler(new ElectionsOfficerInstrumented(), new CandidateFactory(new AppSettings()).Create(), ElectionLock).Run);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }
}
