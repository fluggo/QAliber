using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using QAliber.TestModel;
using QAliber.TestModel.Variables;

namespace QAliber.TestModel.TypeEditors
{
	public class ComboDropDownTypeEditor : UITypeEditor
	{
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if (context != null)
			{
				return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
			}
			return base.GetEditStyle(context);
		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			list = value as ComboDropDownList;
			TestScenario scenario = ((TestCase)context.Instance).Scenario;
			if (provider != null && list != null)
			{
				list.Update(scenario);
				service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				ComboBox comboBox = new ComboBox();
				
				comboBox.SelectedValueChanged += new EventHandler(ComboboxValueChanged);
				comboBox.Leave += new EventHandler(ComboBoxLeave);
				comboBox.GotFocus += new EventHandler(ComboBoxEnter);
				comboBox.Items.AddRange(list.Items);
				comboBox.Dock = DockStyle.Fill;

				service.DropDownControl(comboBox);
				
				return value;
			}
			return base.EditValue(context, provider, value);
		}

		private void ComboBoxEnter(object sender, EventArgs e)
		{
			((ComboBox)sender).DroppedDown = true;
		}

		private void ComboBoxLeave(object sender, EventArgs e)
		{
			string text = ((ComboBox)sender).Text;
			if (!string.IsNullOrEmpty(text))
				list.Selected = text;
		}

		private void ComboboxValueChanged(object sender, EventArgs e)
		{
			list.Selected = ((ComboBox)sender).Text;
			((ComboBox)sender).DroppedDown = false;
			service.CloseDropDown();
		}

		private ComboDropDownList list;
		private IWindowsFormsEditorService service;

	}

	
}
