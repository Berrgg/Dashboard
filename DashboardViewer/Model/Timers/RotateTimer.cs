using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace DashboardViewer.Model.Timers
{
    public class RotateTimer : BaseTimer, IDashboardTimer
    {
        public RotateTimer(TabFormControl tabFormControl)
        {
            TabFormControl = tabFormControl;

            AppSettingsSectionName = "GeneralAppSettings";
            IsTimerEnabledKey = "AutoRotate";
            TimerIntervalKey = "RotateTime";
        }

        public override void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Debug.Print("Rotate timer is working. Interval: " + this.DashboardTimer.Interval / 1000 + "s");

            if (IsTimerEnabled)
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
                    if (TabFormControl.Pages.Count > 0)
                        TabFormControl.SelectedPage = TabFormControl.Pages[index];
                    else
                        TimerStop();
                }));
            }
        }
    }
}
