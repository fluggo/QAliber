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
using System.Linq;
using System.ComponentModel;
using System.Windows;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public class UIControlTypeEditor : UITypeEditor
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
				IWindowsFormsEditorService editor = (IWindowsFormsEditorService) provider.GetService( typeof(IWindowsFormsEditorService) );

				if( editor == null )
					return null;

				CoordinatePropertyAttribute attrib = context.PropertyDescriptor.Attributes
					.OfType<CoordinatePropertyAttribute>().FirstOrDefault();

				PropertyDescriptor coordProp = null;

				if( attrib != null ) {
					PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties( context.Instance );
					coordProp = propCollection.Find( attrib.PropertyName, false );
				}

				UIControlLocatorForm dialog = new UIControlLocatorForm();
				dialog.ControlPath = (string) value;

				if( coordProp != null )
					dialog.Coordinate = (Point) coordProp.GetValue( context.Instance );

				if( editor.ShowDialog( dialog ) == DialogResult.OK ) {
					if( coordProp != null )
						coordProp.SetValue( context.Instance, dialog.Coordinate );

					return dialog.ControlPath;
				}

				return value;
			}
			return base.EditValue(context, provider, value);
		}
	}

	/// <summary>
	/// Marks the property related to a control property that holds coordinates on the control.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	sealed class CoordinatePropertyAttribute : Attribute {
		readonly string _propertyName;

		// This is a positional argument
		public CoordinatePropertyAttribute( string propertyName ) {
			_propertyName = propertyName;
		}

		public string PropertyName {
			get { return _propertyName; }
		}
	}
}
