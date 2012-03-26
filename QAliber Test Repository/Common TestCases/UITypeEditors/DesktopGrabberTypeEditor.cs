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
using QAliber.TestModel;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public class DesktopGrabberTypeEditor : UITypeEditor
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
			TestCase testCase = context.Instance as TestCase;

			if (value is string)
			{
				IntPtr mainHandle = UIControlLocatorForm.FindWindowByCaption(IntPtr.Zero, "QAliber Test Builder");
				Form form = (Form)Form.FromHandle(mainHandle);

				DesktopMaskForm dialog = new DesktopMaskForm();
				form.Visible = false;
				dialog.ImageFile = (string) value;

				try {
					if( dialog.ShowDialog() != DialogResult.OK )
						return value;
				}
				finally {
					form.Visible = true;
				}

				string path = dialog.ImageFile;

				if( testCase != null && testCase.Scenario.Filename != null ) {
					// Make a relative path if we can
					path = NativeMethods.MakeRelativePath( testCase.Scenario.Filename, false, path, false ) ?? path;

					if( path.StartsWith( ".\\" ) )
						path = path.Substring( 2 );
				}

				return path;
			}
			return base.EditValue(context, provider, value);
		}
	}

	
}
