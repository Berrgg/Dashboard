using System.Timers;
using DevExpress.XtraBars;

namespace DashboardViewer.Model.Timers
{
    public abstract class BaseTimer
    {
        protected Timer DashboardTimer { get; private set; }
        protected TabFormControl TabFormControl { get; set; }
        protected string AppSettingsSectionName { get; set; }
        protected string IsTimerEnabledKey { get; set; }
        protected string TimerIntervalKey { get; set; }
        public bool IsTimerEnabled { get; private set; } = false;

        public BaseTimer()
        {
            DashboardTimer = new Timer();
        }
        protected void SetTimer()
        {
            DashboardTimer.Enabled = true;
            DashboardTimer.Elapsed += DashboardTimerElapsed;

            var settings = new TabFormSettings(AppSettingsSectionName);

            if (TabFormControl.Pages.Count > 0)
                IsTimerEnabled = bool.Parse(settings.GetValue(IsTimerEnabledKey));

            if (IsTimerEnabled)
            {
                var reloadTime = int.Parse(settings.GetValue(TimerIntervalKey)) * 1000;
                DashboardTimer.Interval = reloadTime;
            }
        }

        public abstract void DashboardTimerElapsed(object sender, ElapsedEventArgs e);
    }
}
