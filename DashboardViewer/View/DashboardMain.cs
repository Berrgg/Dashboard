using MyApp.Model;
using MyApp.View;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.DashboardWin;
using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DashboardViewer.Model;

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
            SettingsFormEngine formEngine = new SettingsFormEngine(newTab);

            if(XtraDialog.Show(newTab, "Add new dashboard", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                formEngine.Run();

                if (formEngine.IsFormValid() == true)
                    AddPageSettings(newTab.KeyName, newTab.FilePath, newTab.PageName);
                else
                    _canAddNewPage = false;
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
            var tabSettings = new TabFormSettings("TabFormsConfiguration");
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

                var viewer = new DevExpress.DashboardWin.DashboardViewer();
                viewer.Dock = DockStyle.Fill;
                viewer.DataLoadingError += new DataLoadingErrorEventHandler(DashboardLoadingError);
                viewer.DashboardSource = @"" + _filePath;

                e.Page.ContentContainer.Controls.Add(viewer);
            }
            else
            {
                var tabFormControl = sender as TabFormControl;
                BeginInvoke(new Action(() =>
                {
                    tabFormControl.Pages.Remove(e.Page);
                }
                ));
            }
            _canAddNewPage = true;
        }
        private void TabFormControl_Main_PageClosed(object sender, PageClosedEventArgs e)
        {
            DeleteSettingsKeyForClosePage(e.Page.Tag.ToString());
        }

        private void DeleteSettingsKeyForClosePage(string keyName)
        {
            var tabSettings = new TabFormSettings("TabFormsConfiguration");
            tabSettings.RemoveKey(keyName);
        }

        private void DashboardLoadingError(object sender, DataLoadingErrorEventArgs e)
        {
            e.Handled = true;
        }

    }
}
