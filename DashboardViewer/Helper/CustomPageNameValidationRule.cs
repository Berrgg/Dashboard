using DevExpress.XtraEditors.DXErrorProvider;
using System.Linq;
using System.Windows.Forms;

namespace DashboardViewer.Helper
{
    public class CustomPageNameValidationRule : ValidationRule
    {
        public CustomPageNameValidationRule()
        {
            CaseSensitive = false;
            ErrorText = "Page name cannot be blank or contains '-'";
            ErrorType = ErrorType.Critical;
        }
        public override bool Validate(Control control, object value)
        {
            var pageName = value.ToString();
            bool result = pageName != string.Empty && !pageName.ToLower().Contains('-');

            return result;
        }
    }
}
