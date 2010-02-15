using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public class DesktopGrabberTypeEditor : UITypeEditor
	{
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if (context != null)
			{
				return System.Drawing.Design.UITypeEditorEditStyle.Modal;
			}
			return base.GetEditStyle(context);
		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (value is string)
			{
				IntPtr mainHandle = UIControlLocatorForm.FindWindowByCaption(IntPtr.Zero, "QAliber Test Builder");
				Form form = (Form)Form.FromHandle(mainHandle);
				form.Visible = false;
				DesktopMaskForm dialog = new DesktopMaskForm();
				dialog.ShowDialog();
				form.Visible = true;
				return dialog.ImageFile;
			}
			return base.EditValue(context, provider, value);
		}
	}

	
}
