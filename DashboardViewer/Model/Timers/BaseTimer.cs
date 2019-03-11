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
            DashboardTimer.Elapsed += DashboardTimerElapsed;
        }

        public abstract void DashboardTimerElapsed(object sender, ElapsedEventArgs e);

        private void EnableTimer()
        {
            if (TabFormControl.Pages.Count > 0)
            {
                var settings = new TabFormSettings(AppSettingsSectionName);
                IsTimerEnabled = bool.Parse(settings.GetValue(IsTimerEnabledKey));
            }
        }

        private void TimerStart()
        {
            IsTimerEnabled = true;

            var settings = new TabFormSettings(AppSettingsSectionName);
            DashboardTimer.Enabled = true;

            var reloadTime = int.Parse(settings.GetValue(TimerIntervalKey)) * 1000;
            DashboardTimer.Interval = reloadTime;
        }

        private void TimerStop()
        {
            IsTimerEnabled = false;
            DashboardTimer.Enabled = false;
        }

        public void Execute()
        {
            EnableTimer();

            if (IsTimerEnabled)
                TimerStart();
            else
                TimerStop();
        }
    }
}
