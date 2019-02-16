using System.Timers;

namespace DashboardViewer.Model.Timers
{
    public interface IDashboardTimer
    {
        void Execute();

        void DashboardTimerElapsed(object sender, ElapsedEventArgs e);
    }
}
