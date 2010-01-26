using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QAliber.Builder.Presentation
{
	public partial class ListItemsForm : Form
	{
		public ListItemsForm(ICollection items)
		{
			this.items = items;
			DialogResult = DialogResult.Cancel;
			InitializeComponent();
			FillTextBox();
		}

		public string[] Strings
		{
			get
			{
				return strings;
			}
		}

		private void FillTextBox()
		{
			richTextBox.Text = "";
			if (items == null)
				items = new List<string>();
			foreach (object item in items)
			{
				richTextBox.Text += item.ToString() + "\r\n";
			}
		}

		private void StoreList()
		{
			string[] lines = richTextBox.Text.Split('\n');
			List<string> lStrings = new List<string>();
			lStrings.Clear();
			foreach (string line in lines)
			{
				string item = line.Trim('\r', ' ');
				if (!string.IsNullOrEmpty(item))
					lStrings.Add(item);
			}
			strings = lStrings.ToArray();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			StoreList();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private string[] strings;
		private ICollection items;
	}
}