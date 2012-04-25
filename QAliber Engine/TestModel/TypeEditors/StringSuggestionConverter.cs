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
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using QAliber.TestModel;
using QAliber.TestModel.Variables;
using System.Reflection;
using System.ComponentModel;

namespace QAliber.TestModel.TypeEditors
{
	public class StringSuggestionConverter : StringConverter {
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
			MethodInfo optionsMethod = context.Instance.GetType().GetMethod( "Get" + context.PropertyDescriptor.Name + "Options" );
			return optionsMethod != null;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
			MethodInfo exclusiveMethod = context.Instance.GetType().GetMethod( context.PropertyDescriptor.Name + "Exclusive" );

			if( exclusiveMethod == null )
				return false;

			return (bool) exclusiveMethod.Invoke( context.Instance, new object[0] );
		}

		public override TypeConverter.StandardValuesCollection GetStandardValues( ITypeDescriptorContext context ) {
			MethodInfo optionsMethod = context.Instance.GetType().GetMethod( "Get" + context.PropertyDescriptor.Name + "Options" );
			string[] options = (string[]) optionsMethod.Invoke( context.Instance, new object[0] );

			return new TypeConverter.StandardValuesCollection( options );
		}
	}

}
