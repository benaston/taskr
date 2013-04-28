using System;

namespace Taskr.Core.Infrastructure
{
    public class Calendar : ICalendar
    {
        public Calendar(IClock clock)
        {
            Clock = clock;
        }

        public virtual IClock Clock { get; private set; }

        public virtual DateTime ApplicationDate
        {
            get { return DateTime.UtcNow.Date; }
        }
    }
}