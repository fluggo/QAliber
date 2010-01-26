using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public class FileBrowseTypeEditor : UITypeEditor
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
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.Title = "Choose File";
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					return dialog.FileName;
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
