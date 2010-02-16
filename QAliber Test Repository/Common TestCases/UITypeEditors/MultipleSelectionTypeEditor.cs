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
	public class MultipleSelectionTypeEditor : UITypeEditor
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
			list = value as MultipleSelectionList;
			if (provider != null && list != null)
			{
				service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				ListBox listBox = new ListBox();
				listBox.SelectionMode = SelectionMode.MultiSimple;
				foreach (string item in list.SelectedItems)
				{
					listBox.SelectedItems.Add(item);
				}
				listBox.Leave += new EventHandler(ListBoxLeave);
				listBox.Items.AddRange(list.Items.ToArray());
				listBox.Dock = DockStyle.Fill;

				service.DropDownControl(listBox);
				return value;
			}
			return base.EditValue(context, provider, value);
		}

		private void ListBoxLeave(object sender, EventArgs e)
		{
			list.SelectedItems.Clear();
			foreach (string item in ((ListBox)sender).SelectedItems)
			{
				list.SelectedItems.Add(item);
			}
		}

		

		private MultipleSelectionList list;
		private IWindowsFormsEditorService service;

	}

	[Serializable]
	public class MultipleSelectionList
	{
		private List<string> items = new List<string>();

		public List<string> Items 
		{
			get { return items; }
			set { items = value; }
		}

		private List<string> selectedItems = new List<string>();

		public List<string> SelectedItems
		{
			get { return selectedItems; }
			set { selectedItems = value; }
		}

		public override string ToString()
		{
			return string.Join(",", selectedItems.ToArray());
		}

		
	}


	
}
