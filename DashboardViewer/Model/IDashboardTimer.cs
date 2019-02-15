using System.Timers;

namespace DashboardViewer.Model
{
    public interface IDashboardTimer
    {
        void Execute();

        void DashboardTimerElapsed(object sender, ElapsedEventArgs e);
    }
}
