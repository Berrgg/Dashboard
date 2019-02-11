using System.Timers;

namespace DashboardViewer.Model
{
    public interface IDashboardTimer
    {
        Timer DashboardTimer { get;  }

        void Execute();
    }
}
