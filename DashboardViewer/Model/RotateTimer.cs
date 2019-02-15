using System;
using System.Timers;
using DevExpress.XtraBars;

namespace DashboardViewer.Model
{
    public class RotateTimer : BaseTimer, IDashboardTimer
    {
        public RotateTimer(TabFormControl tabFormControl)
        {
            TabFormControl = tabFormControl;

            SetTimer();
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

        public override void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var index = 0;
            var pageIndex = TabFormControl.Pages.IndexOf(TabFormControl.SelectedPage);
            var maxIndex = TabFormControl.Pages.Count - 1;

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

            TabFormControl.BeginInvoke(new Action(() =>
            {
                TabFormControl.SelectedPage = TabFormControl.Pages[index];
            }));
        }
    }
}
