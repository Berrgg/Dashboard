using System;
using System.Timers;
using DevExpress.XtraBars;
using MyApp.Model;

namespace DashboardViewer.Model
{
    public class RotateTimer : IDashboardTimer
    {
        public TabFormControl _tabFormControl { get; set; }

        public Timer DashboardTimer { get; private set; }

        public RotateTimer(TabFormControl tabFormControl)
        {
            _tabFormControl = tabFormControl;

            DashboardTimer = new Timer();
            DashboardTimer.Elapsed += DashboardTimerElapsed;
            DashboardTimer.Enabled = true;
        }
        public void Execute()
        {
            var settings = new TabFormSettings("GeneralAppSettings");
            var isEnabled = bool.Parse(settings.GetValue("AutoRotate"));

            if (isEnabled)
            {
                var reloadTime = int.Parse(settings.GetValue("RotateTime")) * 1000;
                DashboardTimer.Interval = reloadTime;
                DashboardTimer.Start();
            }
        }

        public void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var index = 0;
            var pageIndex = _tabFormControl.Pages.IndexOf(_tabFormControl.SelectedPage);
            var maxIndex = _tabFormControl.Pages.Count - 1;

            if (maxIndex > 0)
            {
                if (pageIndex == maxIndex)
                    index = 0;
                else
                    index += 1;
            }
            else
            {
                index = 0;
            }

            _tabFormControl.BeginInvoke(new Action(() =>
            {
                _tabFormControl.SelectedPage = _tabFormControl.Pages[index];
            }));
        }
    }
}
