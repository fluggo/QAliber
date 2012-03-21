/*
 * Copyright (C) 2012 Anonymous
 *
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
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using QAliber.TestModel;
using QAliber.TestModel.Variables;
using System.Reflection;
using System.ComponentModel;

namespace QAliber.TestModel.TypeEditors {
	/// <summary>
	/// Provides a drop-down list box that allows multiple options to be selected,
	/// then reports the indexes of those selections to the property, which should be of type int[].
	/// </summary>
	/// <remarks>The options displayed in the list box come from calling a GetPropertyOptions
	///   method on the component, where "Property" is the name of the property being edited.</remarks>
	public class MultipleSelectionIndexTypeEditor : UITypeEditor {
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context ) {
			if( context != null )
				return System.Drawing.Design.UITypeEditorEditStyle.DropDown;

			return base.GetEditStyle( context );
		}

		public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value ) {
			int[] list = value as int[];

			if( provider != null && list != null ) {
				IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService( typeof(IWindowsFormsEditorService) );

				ListBox listBox = new ListBox();
				listBox.SelectionMode = SelectionMode.MultiSimple;
				listBox.Dock = DockStyle.Fill;

				MethodInfo optionsMethod = context.Instance.GetType().GetMethod( "Get" + context.PropertyDescriptor.Name + "Options" );
				string[] options = (string[]) optionsMethod.Invoke( context.Instance, new object[0] );
				listBox.Items.AddRange( options );

				foreach( int index in list ) {
					if( index < listBox.Items.Count )
						listBox.SelectedIndices.Add( index );
				}

				service.DropDownControl( listBox );

				return listBox.SelectedIndices.Cast<int>().ToArray();
			}

			return base.EditValue(context, provider, value);
		}

	}

}
