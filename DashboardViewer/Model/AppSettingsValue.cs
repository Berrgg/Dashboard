
namespace DashboardViewer.Model
{
    public class AppSettingsValue : ISettingsValue
    {
        private string _settingsValue;

        public AppSettingsValue(string value)
        {
            _settingsValue = value;
        }
        public string GetSettingsValue()
        {
            return _settingsValue;
        }
    }
}
