using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace MyApp.Model
{
    public class TabFormSettings
    {
        private readonly NameValueCollection _valueCollection = ConfigurationManager.GetSection("TabFormsConfiguration") as NameValueCollection;
        private readonly List<NameValueCollection> _keysList;
        private static Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private readonly KeyValueConfigurationCollection _settings = ((AppSettingsSection)_configFile.GetSection("TabFormsConfiguration")).Settings;


        public TabFormSettings()
        {
            _keysList = new List<NameValueCollection>();
        }
        public void AddUpdateKey(string keyName, ISettingsValue value)
        {
            try
            {
                if (_settings[keyName] == null)
                {
                    _settings.Add(keyName, value.GetSettingsValue());
                }
                else
                {
                    _settings[keyName].Value = value.GetSettingsValue();
                }
                _configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                throw;
            }
        }

        public void RemoveKey(string keyName)
        {
            try
            {
                if (_settings[keyName] != null)
                    _settings.Remove(keyName);

                _configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                throw;
            }
        }

        public string GetValue(string keyName)
        {
            return _valueCollection[keyName];
        }

        public NameValueCollection GetKeys()
        {
            return _valueCollection;
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

        public int CountKeys()
        {
            return _valueCollection.Count;
        }
    }
}
