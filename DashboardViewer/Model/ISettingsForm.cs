using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public interface ISettingsForm
    {
        bool IsFormValid();
        string GetSettingsKey();
        string GetSettingsValue();
        void ValidForm();
        void Execute();
    }
}
