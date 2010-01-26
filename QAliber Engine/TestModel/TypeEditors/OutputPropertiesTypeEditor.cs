using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel.Variables;

namespace QAliber.TestModel.TypeEditors
{
	public class OutputPropertiesTypeEditor : System.Drawing.Design.UITypeEditor
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
			TestCase testcase = context.Instance as TestCase;
			if (value == null)
				value = new Dictionary<string, string>();
			Dictionary<string, string> output = value as Dictionary<string, string>;
			if (testcase != null)
			{
				OutputPropertiesForm form = new OutputPropertiesForm(testcase, output);
				form.ShowDialog();
			}
			return base.EditValue(context, provider, value);
		}
	}
}
