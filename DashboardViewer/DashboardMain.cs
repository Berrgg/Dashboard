using DashboardViewer.Model;
using DashboardViewer.View;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace DashboardViewer
{
    public partial class DashboardMain : DevExpress.XtraBars.TabForm
    {
        static int OpenFormCount = 1;

        public DashboardMain()
        {
            InitializeComponent();
        }
        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            DashboardMain form = new DashboardMain();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CreatePopUpForm();
        }

        private void CreatePopUpForm()
        {
            NewTabForm newTab = new NewTabForm();

            if(XtraDialog.Show(newTab, "Add new dashboard", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var settingsValue = new TabFormSettingsValue(newTab.PageName, newTab.FilePath);

                var keyName = "page" + OpenFormCount++;
                var settings = new TabFormSettings();
                settings.AddUpdateKey(keyName, settingsValue);
            }
        }
    }
}
