using System.Collections.Generic;

namespace DashboardViewer.Model.Timers
{
    public class TimerWorkflow : ITimerWorkflow
    {
        private readonly List<IDashboardTimer> _timersList;

        public TimerWorkflow()
        {
            _timersList = new List<IDashboardTimer>();
        }

        public void Add(IDashboardTimer dashboardTimer)
        {
            _timersList.Add(dashboardTimer);
        }

        public void Remove(IDashboardTimer dashboardTimer)
        {
            _timersList.Remove(dashboardTimer);
        }

        public IEnumerable<IDashboardTimer> GetTimers()
        {
            return _timersList;
        }
    }
}
