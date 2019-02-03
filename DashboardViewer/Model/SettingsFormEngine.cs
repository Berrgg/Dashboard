using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public class SettingsFormEngine
    {
        private ISettingsForm _settingsForm;

        public SettingsFormEngine(ISettingsForm settingsForm)
        {
            _settingsForm = settingsForm;
        }
        public void Run()
        {
            _settingsForm.Execute();
        }

        public bool IsFormValid()
        {
            return _settingsForm.IsFormValid;
        }

        public NameValueCollection KeyValueCollection()
        {
            return _settingsForm.KeyValueCollection();
        }
    }
}
