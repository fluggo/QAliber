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