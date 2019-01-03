using MyApp.Model;
using MyApp.View;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.DashboardWin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace MyApp
{
    public partial class DashboardMain : DevExpress.XtraBars.TabForm
    {
        static int OpenFormCount = 1;
        private string _pageName;
        private string _filePath;

        public DashboardMain()
        {
            InitializeComponent();
            AddTabFormPages();
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
                if (newTab.PageName == null || newTab.FilePath == null)
                {
                    MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var settingsValue = new TabFormSettingsValue(newTab.PageName, newTab.FilePath);

                    var keyName = "page" + OpenFormCount++;
                    var settings = new TabFormSettings();
                    settings.AddUpdateKey(keyName, settingsValue);
                }
            }
        }

        private void AddTabFormPages()
        {
            var tabSettings = new TabFormSettings();
            var settingsKeys = tabSettings.GetKeys();

            foreach (string key in settingsKeys.Keys)
            {
                _pageName = tabSettings.GetValueTabName(key);
                _filePath = tabSettings.GetValueDashboardPath(key);

                tabFormControl_Main.AddNewPage();
            }
        }

        private void tabFormControl_Main_PageCreated(object sender, PageCreatedEventArgs e)
        {
            e.Page.Text = _pageName;
        }
    }
}
