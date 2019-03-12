using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace DashboardViewer.Model.Timers
{
    public class RefreshTimer : BaseTimer, IDashboardTimer
    {
        private DevExpress.DashboardWin.DashboardViewer _viewer;

        public RefreshTimer(TabFormControl tabForm)
        {
            TabFormControl = tabForm;

            AppSettingsSectionName = "GeneralAppSettings";
            IsTimerEnabledKey = "AutoRefresh";
            TimerIntervalKey = "RefreshTime";
        }

        public override void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Debug.Print("Refresh timer is working. Interval: " + this.DashboardTimer.Interval/1000 + "s.");

            if (IsTimerEnabled)
            {
                TabFormControl.BeginInvoke(new Action(() =>
                {
                    if (TabFormControl.SelectedPage != null)
                    {
                            foreach (Control c in TabFormControl.SelectedPage.ContentContainer.Controls)
                        {
                            if (c is DevExpress.DashboardWin.DashboardViewer)
                                _viewer = (c as DevExpress.DashboardWin.DashboardViewer);
                        }

                        _viewer.ReloadData(true);
                        Debug.Print("   Dashboard refreshed: " + _viewer.DashboardSource);
                    }
                    else
                    {
                        TimerStop();
                    }
                }));
            }
        }
    }
}
