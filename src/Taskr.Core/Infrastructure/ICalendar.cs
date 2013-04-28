using System;

namespace Taskr.Core.Infrastructure
{
    public interface ICalendar
    {
        IClock Clock { get; }
        
        DateTime ApplicationDate { get; }
    }
}