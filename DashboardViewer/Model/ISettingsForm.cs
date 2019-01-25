using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.Model
{
    public interface ISettingsForm
    {
        bool IsFormValid();
        NameValueCollection KeyValueCollection();
        void ValidForm();
        void Execute();
    }
}
