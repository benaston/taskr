using System;
using System.Text;
using System.Threading;
using Taskr.Core.BullyAlgorithm;
using Taskr.Core.Infrastructure;
using ZeroMQ;

namespace Taskr.Core.Tests.Slow.Process2
{
    class Program
    {
        public static object ElectionLock = new object();

        static void Main(string[] args)
        {
            var t1 = new Thread(Foo.StartMessageServer);
            var t2 = new Thread(new LeadershipElectionScheduler(new CandidateFactory(new AppSettings()).Create(), ElectionLock).Run);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }
}
