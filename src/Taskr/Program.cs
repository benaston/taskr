using System.ServiceProcess;

namespace Taskr
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TaskrService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}