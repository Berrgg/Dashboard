using System.Timers;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public interface IDashboardTimer
    {
        Timer DashboardTimer { get;  }

        void Execute();

        Task DashboardTimerElapsedAsync();
    }
}
