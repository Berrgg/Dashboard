using DashboardViewer.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.View
{
    public class SettingsForm : XtraUserControl, ISettingsForm
    {
        private TextEdit textEditRefresh = new TextEdit() { Name = "textEditRefresh" };
        private ToggleSwitch toggleSwitchAutoRefresh = new ToggleSwitch() { Name = "toggleSwitchAutoRefresh"};
        private ToggleSwitch toggleSwitchAutoRotate = new ToggleSwitch() { Name = "toggleSwitchAutoRefresh" };

        public SettingsForm()
        {

            var lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            lc.AddItem("Dashboard refresh time (in seconds):", textEditRefresh).TextVisible=true;
            lc.AddItem("Dashboard auto refresh:", toggleSwitchAutoRefresh).TextVisible = true;
            lc.AddItem("Auto rotate between dashboards:", toggleSwitchAutoRotate).TextVisible = true;

            Controls.Add(lc);
            Dock = DockStyle.None;
            Width = 500;
        }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool IsFormValid()
        {
            throw new NotImplementedException();
        }

        public NameValueCollection KeyValueCollection()
        {
            throw new NotImplementedException();
        }

        public void ValidForm()
        {
            throw new NotImplementedException();
        }
    }
}
