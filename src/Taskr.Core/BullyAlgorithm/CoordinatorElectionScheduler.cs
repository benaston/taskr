using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Taskr.Core.BullyAlgorithm
{
    /// <summary>
    /// Schedules the election of the "coordinator".
    /// </summary>
    public class CoordinatorElectionScheduler
    {
        public const string ElectionMessage = "ELECTION";
        public const string ElectionConclusiveMessage = "CONCLUSIVE";
        public const double ElectionMessageReceiveTimeoutSeconds = .5;
        public const double ElectionMessageReceiveSocketLingerSeconds = 1;
        public const double ElectionIntervalSeconds = 2.5;

        public static bool IsCoordinatorProcess;
        public static Random RandomNumberGenerator = new Random();

        private readonly IElectionsOfficer _electionsOfficer;
        private readonly IList<ICandidate> _candidates;
        private readonly object _lockObject;
        private readonly ICandidate _localCandidate;

        public CoordinatorElectionScheduler(IElectionsOfficer electionsOfficer, 
                                            IList<ICandidate> candidates, 
                                            object lockObject)
        {
            _electionsOfficer = electionsOfficer;
            _candidates = candidates;
            _lockObject = lockObject;
            _localCandidate = _candidates.First(c => c.IsLocal);
        }

        public void Run()
        {
            for(;;)
            {
                lock (_lockObject)
                {
                    try
                    {
                        _electionsOfficer.HoldElection(_localCandidate, _candidates);
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