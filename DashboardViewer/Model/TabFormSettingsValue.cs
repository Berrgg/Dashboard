using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public class TabFormSettingsValue : ISettingsValue
    {
        private string _pageName;
        private string _filePath;

        public TabFormSettingsValue(string pageName, string filePath)
        {
            _pageName = pageName;
            _filePath = filePath;
        }
        public string GetSettingsValue()
        {
            var settingsValue = _pageName + '-' + _filePath;

            return settingsValue;
        }
    }
}
