using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DashboardViewer.Model
{
    public interface ISettingsForm
    {
        string FormName { get; }
        MessageBoxButtons FormButtons { get; }
        bool IsFormValid { get;}
        NameValueCollection KeyValueCollection { get; }
        void ValidForm();
        void Execute();
    }
}
