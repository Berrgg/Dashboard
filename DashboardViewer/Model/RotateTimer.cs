using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DevExpress.XtraBars;
using MyApp.Model;

namespace DashboardViewer.Model
{
    public class RotateTimer : IDashboardTimer
    {
        private TabFormControl _tabFormControl;
        public Timer DashboardTimer { get; private set; }
        public DevExpress.DashboardWin.DashboardViewer Viewer { get; set; }

        public RotateTimer(TabFormControl tabFormControl, DevExpress.DashboardWin.DashboardViewer viewer)
        {
            _tabFormControl = tabFormControl;
            Viewer = viewer;

            Timer DashboardTimer = new Timer();
            DashboardTimer.Elapsed += async (sender, e) => await DashboardTimerElapsedAsync();
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

        public async Task DashboardTimerElapsedAsync()
        {
            await Task.Run(() =>
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
                    index = 0;

                _tabFormControl.SelectedPage = _tabFormControl.Pages[index];

            });
        }
    }
}
