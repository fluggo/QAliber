/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
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
