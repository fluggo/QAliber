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

	public class FileSaveTypeEditor : UITypeEditor
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
				SaveFileDialog dialog = new SaveFileDialog();
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
