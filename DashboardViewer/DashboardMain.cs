using DevExpress.XtraBars;
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
    }
}
