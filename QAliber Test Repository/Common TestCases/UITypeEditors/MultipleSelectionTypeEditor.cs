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
