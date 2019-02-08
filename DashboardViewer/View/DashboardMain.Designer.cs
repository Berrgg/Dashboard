namespace MyApp
{
    partial class DashboardMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (--OpenFormCount == 0) System.Windows.Forms.Application.Exit();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardMain));
            this.tabFormControl_Main = new DevExpress.XtraBars.TabFormControl();
            this.tabFormDefaultManager1 = new DevExpress.XtraBars.TabFormDefaultManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonSettings = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabFormControl_Main
            // 
            this.tabFormControl_Main.AllowMoveTabsToOuterForm = false;
            this.tabFormControl_Main.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonSettings});
            this.tabFormControl_Main.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl_Main.Manager = this.tabFormDefaultManager1;
            this.tabFormControl_Main.Name = "tabFormControl_Main";
            this.tabFormControl_Main.ShowTabsInTitleBar = DevExpress.XtraBars.ShowTabsInTitleBar.True;
            this.tabFormControl_Main.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
            this.tabFormControl_Main.Size = new System.Drawing.Size(821, 40);
            this.tabFormControl_Main.TabForm = this;
            this.tabFormControl_Main.TabIndex = 0;
            this.tabFormControl_Main.TabRightItemLinks.Add(this.barButtonSettings);
            this.tabFormControl_Main.TabStop = false;
            this.tabFormControl_Main.PageCreated += new DevExpress.XtraBars.PageCreatedEventHandler(this.tabFormControl_Main_PageCreated);
            this.tabFormControl_Main.PageClosed += new DevExpress.XtraBars.PageClosedEventHandler(this.TabFormControl_Main_PageClosed);
            this.tabFormControl_Main.OuterFormCreating += new DevExpress.XtraBars.OuterFormCreatingEventHandler(this.OnOuterFormCreating);
            // 
            // tabFormDefaultManager1
            // 
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlTop);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlBottom);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlLeft);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlRight);
            this.tabFormDefaultManager1.DockingEnabled = false;
            this.tabFormDefaultManager1.Form = this;
            this.tabFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonSettings});
            this.tabFormDefaultManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 40);
            this.barDockControlTop.Manager = null;
            this.barDockControlTop.Size = new System.Drawing.Size(821, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 462);
            this.barDockControlBottom.Manager = null;
            this.barDockControlBottom.Size = new System.Drawing.Size(821, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = null;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 422);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(821, 40);
            this.barDockControlRight.Manager = null;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 422);
            // 
            // barButtonSettings
            // 
            this.barButtonSettings.Caption = "Application settings";
            this.barButtonSettings.Id = 0;
            this.barButtonSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonSettings.ImageOptions.Image")));
            this.barButtonSettings.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonSettings.ImageOptions.LargeImage")));
            this.barButtonSettings.Name = "barButtonSettings";
            this.barButtonSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonSettings_ItemClick);
            // 
            // DashboardMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 462);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.tabFormControl_Main);
            this.LookAndFeel.SkinName = "Visual Studio 2013 Dark";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "DashboardMain";
            this.TabFormControl = this.tabFormControl_Main;
            this.Text = "DashboardMain";
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormDefaultManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.TabFormControl tabFormControl_Main;
        private DevExpress.XtraBars.TabFormDefaultManager tabFormDefaultManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonSettings;
    }
}

