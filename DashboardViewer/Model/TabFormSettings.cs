using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace DashboardViewer.Model
{
    public class TabFormSettings
    {
        private readonly NameValueCollection _settings = ConfigurationManager.GetSection("TabFormsConfiguration") as NameValueCollection;
        private readonly List<NameValueCollection> _keysList;


        public TabFormSettings()
        {
            _keysList = new List<NameValueCollection>();
        }
        public void AddKey(string keyName, string value)
        {
            _settings.Add(keyName, value);
        }

        public void RemoveKey(string keyName)
        {
            _settings.Remove(keyName);
        }

        public string GetValue(string keyName)
        {
            return _settings[keyName];
        }

        public IEnumerable<NameValueCollection> GetKeys()
        {
            if (_settings.Count == 0)
            {
                new SettingsPropertyNotFoundException("No TabFormSettings stored in App.config file.");
            }
            else
            {
                foreach (NameValueCollection key in _settings)
                {
                    _keysList.Add(key);
                }
            }
            return _keysList;
        }

        public string GetValueTabName(string keyName)
        {
            var keyValue = GetValue(keyName);

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                return keyValue.Split('-').First();
            }

            return string.Empty;
        }

        public string GetValueDashboardPath(string keyName)
        {
            var keyValue = GetValue(keyName);

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                return keyValue.Split('-').Last();
            }

            return string.Empty;
        }
    }
}
