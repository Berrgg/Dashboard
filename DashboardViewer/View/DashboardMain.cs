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
using DevExpress.DashboardCommon;

namespace MyApp
{
    public partial class DashboardMain : DevExpress.XtraBars.TabForm
    {
        static int OpenFormCount = 1;
        private string _pageName;
        private string _filePath;
        private string _key;
        private bool _isNewPage =  true;
        private bool _canAddNewPage = true;

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

                    var settings = new TabFormSettings();
                    var keyName = (settings.GiveMaxKeyValue() + 1).ToString();
                    settings.AddUpdateKey(keyName, settingsValue);

                    AddPageSettings(keyName, newTab.FilePath, newTab.PageName);
                  //  tabFormControl_Main.AddNewPage();
                }
            }
            else
            {
                _canAddNewPage = false;
            }
        }

        private void AddPageSettings(string keyName, string filePath, string pageName)
        {
            _key = keyName;
            _pageName = pageName;
            _filePath = filePath;
        }

        private void AddTabFormPages()
        {
            var tabSettings = new TabFormSettings();
            var settingsKeys = tabSettings.GetKeys();

            _isNewPage = false;

            foreach (string key in settingsKeys.Keys)
            {
                _pageName = tabSettings.GetValueTabName(key);
                _filePath = tabSettings.GetValueDashboardPath(key);
                _key = key;

                tabFormControl_Main.AddNewPage();
            }

            _isNewPage = true;
        }

        private void tabFormControl_Main_PageCreated(object sender, PageCreatedEventArgs e)
        {
            if (_isNewPage)
                CreatePopUpForm();

            if (_canAddNewPage)
            {
                e.Page.Text = _pageName;
                e.Page.Tag = _key;

                DashboardViewer viewer = new DashboardViewer();
                viewer.Dock = DockStyle.Fill;
                viewer.DataLoadingError += new DataLoadingErrorEventHandler(DashboardLoadingError);
                viewer.DashboardSource = @"" + _filePath;

                e.Page.ContentContainer.Controls.Add(viewer);
            }
            _canAddNewPage = true;
        }

        private void DashboardLoadingError(object sender, DataLoadingErrorEventArgs e)
        {
            e.Handled = true;
        }
    }
}
