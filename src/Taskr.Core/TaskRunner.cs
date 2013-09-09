using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Taskr.Core.Infrastructure.BullyAlgorithm;

namespace Taskr.Core
{
    public class TaskRunner
    {
        public static void Run(IEnumerable<ITask> tasks)
        {
            for (; ; )
            {
                if (CoordinatorElectionScheduler.IsCoordinatorProcess)
                {
                    var taskList = tasks.ToArray();

                    if (taskList.Length > 10)
                    {
                        throw new ArgumentOutOfRangeException("taskList");
                    }

                    var runningThreads = new List<Thread>();

                    foreach (var t in taskList)
                    {
                        runningThreads.Add(new Thread(t.Run));
                    }

                    foreach (var t in runningThreads)
                    {
                        t.Start();
                    }

                    foreach (var t in runningThreads)
                    {
                        t.Join();
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}