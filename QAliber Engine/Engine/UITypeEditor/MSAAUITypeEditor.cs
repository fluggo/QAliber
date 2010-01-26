using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using QAliber.Engine.Controls;
using ManagedWinapi.Accessibility;
using QAliber.Engine.Controls.UIA;

namespace QAliber.Engine.UITypeEditor
{
	public class MSAAUITypeEditor : System.Drawing.Design.UITypeEditor
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
			if (context.Instance is UIAControl)
			{
				UIAControl control = context.Instance as UIAControl;
				if (control.MSAA != null)
				{
					MSAAViewer viewer = new MSAAViewer(control.CodePath + ".MSAA", control.MSAA);
					viewer.ShowDialog();
					viewer.Activate();
				}
				return value;
			}
			return base.EditValue(context, provider, value);
		}
	}
}
