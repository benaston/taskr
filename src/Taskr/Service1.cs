using System.ServiceProcess;

namespace Taskr
{
    public partial class TaskrService : ServiceBase
    {
        public TaskrService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
