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
        private TextEdit textEditPath;
        private TextEdit textEditName;

        public NewTabForm()
        {
            textEditPath.Click += new EventHandler(textEditPath_Click);

            var lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            lc.AddItem(string.Empty, textEditName).TextVisible = false;
            lc.AddItem(string.Empty, textEditPath).TextVisible = false;

            Controls.Add(lc);
            Dock = DockStyle.Fill;
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
