using System.Timers;

namespace DashboardViewer.Model
{
    public interface IDashboardTimer
    {
        Timer DashboardTimer { get;  }

        void Execute();

        void DashboardTimerElapsed(object sender, ElapsedEventArgs e);
    }
}
