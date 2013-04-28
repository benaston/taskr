using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Taskr.Core.BullyAlgorithm
{
    public class LeadershipElectionScheduler
    {
        public const string ElectionMessage = "ELECTION";
        public const string ElectionConclusiveMessage = "CONCLUSIVE";
        public const double ElectionMessageReceiveTimeoutSeconds = 4;
        public const double ElectionMessageReceiveSocketLingerSeconds = 1;
        public const double ElectionIntervalSeconds = 5;

        public static bool IsLeaderProcess;
        public static Random RandomNumberGenerator = new Random();

        private readonly IList<ICandidate> _candidates;
        private readonly object _lockObject;
        private readonly ICandidate _thisCandidate;

        public LeadershipElectionScheduler(IList<ICandidate> candidates, object lockObject)
        {
            _candidates = candidates;
            _lockObject = lockObject;
            _thisCandidate = _candidates.First(c => c.IsLocal);
        }

        public void Run()
        {
            for(;;)
            {
                lock (_lockObject)
                {
                    try
                    {
                        _thisCandidate.HoldElection(_candidates);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(ElectionIntervalSeconds));
            }
        }
    }
}