using System.Collections.Generic;

namespace DashboardViewer.Model.Timers
{
    public interface ITimerWorkflow
    {
        void Add(IDashboardTimer dashboardTimer);
        void Remove(IDashboardTimer dashboardTimer);
        IEnumerable<IDashboardTimer> GetTimers();
    }
}
