using System.Timers;
using DevExpress.XtraBars;

namespace DashboardViewer.Model
{
    public abstract class BaseTimer
    {
        protected Timer DashboardTimer { get; private set; }
        protected TabFormControl TabFormControl { get; set; }

        protected void SetTimer()
        {
            DashboardTimer = new Timer();
            DashboardTimer.Enabled = true;
            DashboardTimer.Elapsed += DashboardTimerElapsed;
        }

        public abstract void DashboardTimerElapsed(object sender, ElapsedEventArgs e);
    }
}
