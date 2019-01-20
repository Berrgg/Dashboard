using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public class SettingsFormEngine
    {
        public void Run(ISettingsForm settingsForm)
        {
            settingsForm.Execute();
        }
    }
}
