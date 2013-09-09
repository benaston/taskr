using System.ServiceProcess;
using System.Threading;
using Taskr.Core.Infrastructure;
using Taskr.Core.Infrastructure.BullyAlgorithm;

namespace Taskr
{
    public partial class TaskrService : ServiceBase
    {
        public static object ElectionLock = new object();

        public TaskrService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var t1 = new Thread(ElectionNotificationReception.Run);
            var t2 = new Thread(new CoordinatorElectionScheduler(new ElectionsOfficerInstrumented(), new CandidateFactory(new AppSettings()).Create(), ElectionLock).Run);
            var t3 = new Thread(ElectionNotificationReception.Run);

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        protected override void OnStop()
        {
        }
    }
}
