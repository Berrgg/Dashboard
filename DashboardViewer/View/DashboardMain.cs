using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using DevExpress.DashboardCommon;
using DevExpress.XtraBars;
using DashboardViewer.View;
using DashboardViewer.Model;
using DashboardViewer.Model.Timers;

namespace DashboardViewer
{
    public partial class DashboardMain : TabForm
    {
        static int OpenFormCount = 1;
        private string _pageName;
        private string _filePath;
        private string _key;
        private bool _isNewPage =  true;
        private bool _canAddNewPage = true;
        private DevExpress.DashboardWin.DashboardViewer _viewer;
        public RotateTimer RotateTimer { get; set; }
        public RefreshTimer RefreshTimer { get; set; }
        private TimerWorkflow timerWorkflow;

        public DashboardMain()
        {
            InitializeComponent();

            var tabSettings = new TabFormSettings("TabFormsConfiguration").GetKeys();
            AddTabFormPages(tabSettings);

            SetTimers();
        }

        private void SetTimers()
        {
            timerWorkflow = new TimerWorkflow();

            RefreshTimer = new RefreshTimer(tabFormControl_Main);
            timerWorkflow.Add(RefreshTimer);

            RotateTimer = new RotateTimer(tabFormControl_Main);
            timerWorkflow.Add(RotateTimer);

            RunTimerWorkflowEngine();
        }

        private void RunTimerWorkflowEngine()
        {
            var workflowEngine = new TimerWorkflowEngine();
            workflowEngine.Run(timerWorkflow);
        }

        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            DashboardMain form = new DashboardMain();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }

        private void CreatePopUpForm()
        {
            NewTabForm newTab = new NewTabForm();
            SettingsFormEngine formEngine = new SettingsFormEngine(newTab);

            formEngine.Run();

            if (formEngine.IsFormValid() == true)
            {
                var tabSettings = new TabFormSettings(formEngine.KeyValueCollection());

                foreach (string key in formEngine.KeyValueCollection().Keys)
                {
                    _pageName = tabSettings.GetValueTabName(key);
                    _filePath = tabSettings.GetValueDashboardPath(key);
                    _key = key;
                }
            }
            else
                _canAddNewPage = false;
        }

        private void AddTabFormPages(NameValueCollection keyCollection)
        {
            var tabSettings = new TabFormSettings(keyCollection);

            _isNewPage = false;

            foreach (string key in keyCollection.Keys)
            {
                _pageName = tabSettings.GetValueTabName(key);
                _filePath = tabSettings.GetValueDashboardPath(key);
                _key = key;

                tabFormControl_Main.AddNewPage();
            }

            _isNewPage = true;
        }

        private void DeleteSettingsKeyForClosePage(string keyName)
        {
            var tabSettings = new TabFormSettings("TabFormsConfiguration");
            tabSettings.RemoveKey(keyName);
        }

        private void RefreshDashboard(DevExpress.DashboardWin.DashboardViewer dashboardViewer)
        {
            if (_viewer != null)
                dashboardViewer.ReloadData(true);
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
                _viewer = viewer;
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

        private void DashboardLoadingError(object sender, DataLoadingErrorEventArgs e)
        {
            e.Handled = true;
        }

        private void BarButtonSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            SettingsFormEngine engine = new SettingsFormEngine(settingsForm);
            engine.Run();
            RunTimerWorkflowEngine();
        }

        private void TabFormControl_Main_SelectedPageChanged(object sender, TabFormSelectedPageChangedEventArgs e)
        {
            foreach (Control c in tabFormControl_Main.SelectedPage.ContentContainer.Controls)
            {
                if (c is DevExpress.DashboardWin.DashboardViewer)
                    _viewer = (c as DevExpress.DashboardWin.DashboardViewer);
            }
        }

        private void BarButtonRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshDashboard(_viewer);
        }
    }
}
