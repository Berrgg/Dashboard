using MyApp.Model;
using System.Timers;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public class RefreshTimer : IDashboardTimer
    {
        private DevExpress.DashboardWin.DashboardViewer _viewer;
        public Timer DashboardTimer { get; private set; }

        public RefreshTimer(DevExpress.DashboardWin.DashboardViewer viewer)
        {
            _viewer = viewer;
            Timer DashboardTimer = new Timer();
            DashboardTimer.Elapsed += async (sender, e) => await DashboardTimerElapsedAsync();
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

        public async Task DashboardTimerElapsedAsync()
        {
            await Task.Run(() =>
            {
                _viewer.ReloadData(true);
            });
        }
    }
}
