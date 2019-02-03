using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;
using DashboardViewer.View;
using DashboardViewer.Model;
using MyApp.Model;
using System.Collections.Specialized;

namespace MyApp.View
{
    public class NewTabForm : XtraUserControl, ISettingsForm
    {
        private bool _isFormValid;
        public bool IsFormValid
        {
            get { return _isFormValid; }
            set { _isFormValid = value; }
        }

        private NameValueCollection _keyCollection;
        public string FilePath { get; private set; }
        public string PageName { get; private set; }
        public string KeyName { get; private set; }

        public TextEdit textEditPath = new TextEdit();
        public TextEdit textEditName = new TextEdit();

        public NewTabForm()
        {
            _keyCollection = new NameValueCollection();

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
            PageName = textEditName.Text;
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

        public void ValidForm()
        {
            if (PageName == null || FilePath == null)
                _isFormValid = false;
            else
                _isFormValid = true;
        }

        public void Execute()
        {
            ValidForm();

            if (_isFormValid == false)
            {
                XtraMessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var settingsValue = new TabFormSettingsValue(PageName, FilePath);

                var settings = new TabFormSettings("TabFormsConfiguration");
                var keyName = (settings.GiveMaxKeyValue() + 1).ToString();
                KeyName = keyName;
                settings.AddUpdateKey(keyName, settingsValue);

                _keyCollection.Add(keyName, settingsValue.GetSettingsValue());
            }
        }

        public NameValueCollection KeyValueCollection()
        {
            return _keyCollection;
        }

    }
}
