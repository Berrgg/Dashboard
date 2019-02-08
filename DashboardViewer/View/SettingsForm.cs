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
using MyApp.Model;
using System.ComponentModel;

namespace DashboardViewer.View
{
    public class SettingsForm : XtraUserControl, ISettingsForm
    {
        public string FormName { get; private set; } = "Settings";
        public MessageBoxButtons FormButtons { get; private set; } = MessageBoxButtons.OKCancel;
        public bool IsFormValid { get; private set; }
        public NameValueCollection KeyValueCollection { get; private set; }
        private uint _refreshTime;
        private uint _rotateTime;

        private TextEdit textEditRefresh = new TextEdit() { Name = "textEditRefresh" };
        private TextEdit textEditRotate = new TextEdit() { Name = "textEditRotate" };
        private ToggleSwitch toggleSwitchAutoRefresh = new ToggleSwitch() { Name = "toggleSwitchAutoRefresh"};
        private ToggleSwitch toggleSwitchAutoRotate = new ToggleSwitch() { Name = "toggleSwitchAutoRefresh" };

        public SettingsForm()
        {
            var settings = new TabFormSettings("GeneralAppSettings");
            textEditRefresh.Text = settings.GetValue("RefreshTime");
            textEditRotate.Text = settings.GetValue("RotateTime");
            toggleSwitchAutoRefresh.EditValue = bool.Parse(settings.GetValue("AutoRefresh"));
            toggleSwitchAutoRotate.EditValue = bool.Parse(settings.GetValue("AutoRotate"));

            textEditRefresh.Validating += new CancelEventHandler(TextBox_Validating);
            textEditRotate.Validating += new CancelEventHandler(TextBox_Validating);

            var lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            lc.AddItem("Dashboard refresh time (in seconds):", textEditRefresh).TextVisible=true;
            lc.AddItem("Dashboard auto refresh:", toggleSwitchAutoRefresh).TextVisible = true;
            lc.AddItem("Rotate time between dashboards (in seconds):", textEditRotate).TextVisible = true;
            lc.AddItem("Auto rotate between dashboards:", toggleSwitchAutoRotate).TextVisible = true;

            Controls.Add(lc);
            Dock = DockStyle.None;
            Width = 500;
        }

        public void Execute()
        {
            ValidForm();

            if (IsFormValid == false)
            {
                XtraMessageBox.Show("Fields cannot be empty or below 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var refreshValue = new AppSettingsValue(textEditRefresh.Text);
                var rotateValue = new AppSettingsValue(textEditRotate.Text);
                var autoRefreshValue = new AppSettingsValue(toggleSwitchAutoRefresh.EditValue.ToString());
                var autoRotateValue = new AppSettingsValue(toggleSwitchAutoRotate.EditValue.ToString());

                TabFormSettings settings = new TabFormSettings("GeneralAppSettings");
                settings.AddUpdateKey("RefreshTime", refreshValue);
                settings.AddUpdateKey("RotateTime", rotateValue);
                settings.AddUpdateKey("AutoRotate", autoRotateValue);
                settings.AddUpdateKey("AutoRefresh", autoRefreshValue);
            }
        }

        public void ValidForm()
        {
            if (uint.TryParse(textEditRefresh.Text, out _refreshTime) && uint.TryParse(textEditRotate.Text,out _rotateTime))
                IsFormValid = true;
            else
                IsFormValid = false;
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (sender as TextEdit);
            textBox.ErrorText = "Field cannot be empty or value less than 0";

            var value = textBox.Text;

            if (value == string.Empty || !uint.TryParse(value, out uint valueOut))
                e.Cancel = true;
            else
                textBox.ErrorText = string.Empty;

        }
    }
}
