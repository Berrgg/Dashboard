using System;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraBars;
using MyApp.Model;

namespace DashboardViewer.Model
{
    public class RefreshTimer : IDashboardTimer
    {
        private DevExpress.DashboardWin.DashboardViewer _viewer;
        public System.Timers.Timer DashboardTimer { get; private set; }
        public TabFormControl _tabFormControl { get; set; }

        public RefreshTimer(TabFormControl tabForm)
        {
            _tabFormControl = tabForm;

            DashboardTimer = new System.Timers.Timer();
            DashboardTimer.Elapsed += DashboardTimerElapsed;
            DashboardTimer.Enabled = true;
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

        public void DashboardTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _tabFormControl.BeginInvoke(new Action(() =>
            {
                foreach (Control c in _tabFormControl.SelectedPage.ContentContainer.Controls)
                {
                    if (c is DevExpress.DashboardWin.DashboardViewer)
                        _viewer = (c as DevExpress.DashboardWin.DashboardViewer);
                }

                _viewer.ReloadData(true);
            }));
        }
    }
}
