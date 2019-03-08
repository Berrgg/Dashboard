using System;
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

            SetTimer();
        }

        public override void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            MessageBox.Show("Refresh timer is working");
            if (IsTimerEnabled)
            {
                TabFormControl.BeginInvoke(new Action(() =>
                {
                    foreach (Control c in TabFormControl.SelectedPage.ContentContainer.Controls)
                    {
                        if (c is DevExpress.DashboardWin.DashboardViewer)
                            _viewer = (c as DevExpress.DashboardWin.DashboardViewer);
                    }

                    _viewer.ReloadData(true);
                }));
            }
        }
    }
}
