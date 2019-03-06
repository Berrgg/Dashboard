using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace DashboardViewer.Model
{
    public class TabFormSettings
    {
        private readonly NameValueCollection _valueCollection;
        private static Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private readonly KeyValueConfigurationCollection _settings;
        private string _sectionName;

        public TabFormSettings(NameValueCollection nameValueCollection)
        {
            _valueCollection = nameValueCollection;
        }
        public TabFormSettings(string sectionName)
        {
            _sectionName = sectionName;
            _valueCollection = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            _settings = ((AppSettingsSection)_configFile.GetSection(sectionName)).Settings;
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

                //ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
                ConfigurationManager.RefreshSection(_sectionName);
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

        public int GiveMaxKeyValue()
        {
            var listOfKeys = new List<int>();
            int x = 0;

            foreach (string key in _valueCollection.Keys)
            {
                if (int.TryParse(key, out x))
                    listOfKeys.Add(x);
            }

            if (listOfKeys.Count == 0)
                return 0;
            else
                return listOfKeys.Max();
        }
    }
}
