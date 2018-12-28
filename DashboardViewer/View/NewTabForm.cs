using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardViewer.View
{
    public class NewTabForm : XtraUserControl
    {
        public string FilePath { get; private set; }
        public string PageName { get; private set; }
        private TextEdit textEditPath = new TextEdit();
        private TextEdit textEditName = new TextEdit();

        public NewTabForm()
        {
            textEditPath.Click += new EventHandler(textEditPath_Click);

            var lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            lc.AddItem("Page name:", textEditName).TextVisible = true;
            lc.AddItem("Dashboard file:", textEditPath).TextVisible = true;

            Controls.Add(lc);
            Dock = DockStyle.None;
            Width = 500;
            Height = 200;
        }

        private void textEditPath_Click(object sender, EventArgs e)
        {
            using (var dialog = new XtraOpenFileDialog())
            {
                dialog.Title = "New dashboard page.";
                dialog.Filter = "XML files(*.xml)|*.xml";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    FilePath = dialog.InitialDirectory + dialog.FileName;
                    PageName = textEditName.Text;

                    textEditPath.Text = FilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when selected XML file. Error message: " + ex.Message);
                }
            }
        }
    }
}
