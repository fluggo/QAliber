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
