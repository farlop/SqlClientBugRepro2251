using System.Threading.Tasks;

namespace Workers.Abstractions
{
    public interface IWorker
    {
        Task<string> RunAsync();
    }
}
