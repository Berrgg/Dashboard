using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;

namespace MyApp.View
{
    public class NewTabForm : XtraUserControl
    {
        public string FilePath { get; private set; }
        public string PageName { get; private set; }
        public TextEdit textEditPath = new TextEdit();
        public TextEdit textEditName = new TextEdit();

        public NewTabForm()
        {
            textEditPath.Click += new EventHandler(textEditPath_Click);
            textEditName.Leave += new EventHandler(textEdit_Leave);
            textEditName.Validating += new CancelEventHandler(textEditName_Validating);
            textEditPath.Validating += new CancelEventHandler(textEditPath_Validating);

            var lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            lc.AddItem("Page name:", textEditName).TextVisible = true;
            lc.AddItem("Dashboard file:", textEditPath).TextVisible = true;

            Controls.Add(lc);
            Dock = DockStyle.None;
            Width = 500;
            Height = 200;
        }

        private void textEditName_Validating(object sender, CancelEventArgs e)
        {
            var textName = (sender as TextEdit);
            textName.ErrorText = "Name cannot be blank or contains '-'";
            var pageName = textName.Text;

            if (pageName == string.Empty || pageName.ToLower().Contains('-'))
                e.Cancel = true;
            else
                textName.ErrorText = string.Empty;
        }

        private void textEditPath_Validating(object sender, CancelEventArgs e)
        {
            var textPath = (sender as TextEdit);
            textPath.ErrorText = "Field cannot be blank";

            var pathName = textPath.Text;

            if (pathName == string.Empty)
                e.Cancel = true;
            else
                textPath.ErrorText = string.Empty;
        }

        private void textEdit_Leave(object sender, EventArgs e)
        {
            (sender as TextEdit).DoValidate();
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
