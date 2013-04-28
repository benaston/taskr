namespace Taskr.Core.Infrastructure
{
    public interface IAppSettings { string this[string key] { get; } }
}