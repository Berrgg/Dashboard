using System;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace DashboardViewer.Model
{
    public class RefreshTimer : BaseTimer, IDashboardTimer
    {
        private DevExpress.DashboardWin.DashboardViewer _viewer;

        public RefreshTimer(TabFormControl tabForm)
        {
            TabFormControl = tabForm;

            SetTimer();
        }

        public void Execute()
        {
            var settings = new TabFormSettings("GeneralAppSettings");
            var isEnabled = bool.Parse(settings.GetValue("AutoRefresh"));

            if (isEnabled)
            {
                var reloadTime = int.Parse(settings.GetValue("RefreshTime"))*1000;
                DashboardTimer.Interval = reloadTime;
                DashboardTimer.Start();
            }
        }

        public override void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
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
