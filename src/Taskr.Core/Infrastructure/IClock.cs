using System;

namespace Taskr.Core.Infrastructure
{
    public interface IClock
    {
        DateTime ApplicationNow { get; }
    }
}