using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Taskr.Core.BullyAlgorithm
{
    public class LeadershipElectionScheduler
    {
        public static bool IsLeaderProcess;
        public static Random RandomNumberGenerator = new Random();
        private readonly IList<ICandidate> _candidates;
        private readonly object _lockObject;
        private readonly TimeSpan _electionInterval = TimeSpan.FromSeconds(5);
        private ICandidate _thisCandidate;

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
//                        foreach (var i in thisCandidate.AuthoritativeCandidateIds)
//                        {
//                            _candidates[i].HoldElection(_candidates);
//                        }
                        _thisCandidate.HoldElection(_candidates);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(_electionInterval);
            }
        }
    }
}