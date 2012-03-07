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
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace QAliber.TestModel.Variables
{
	/// <summary>
	/// Provides a drop-down list for string variable names.
	/// </summary>
	public class StringVariableNameTypeConverter : TypeConverter {
		public override bool GetStandardValuesSupported( ITypeDescriptorContext context ) {
			TestCase test = context.Instance as TestCase;
			return test != null && test.Scenario != null;
		}

		public override bool GetStandardValuesExclusive( ITypeDescriptorContext context ) {
			return false;
		}

		public override StandardValuesCollection GetStandardValues( ITypeDescriptorContext context ) {
			TestCase test = (TestCase) context.Instance;
			return new StandardValuesCollection( test.Scenario.Variables.Select( v => v.Name ).ToArray() );
		}
	}

	/// <summary>
	/// Provides a drop-down list for list variable names.
	/// </summary>
	public class ListVariableNameTypeConverter : TypeConverter {
		public override bool GetStandardValuesSupported( ITypeDescriptorContext context ) {
			TestCase test = context.Instance as TestCase;
			return test != null && test.Scenario != null;
		}

		public override bool GetStandardValuesExclusive( ITypeDescriptorContext context ) {
			return false;
		}

		public override StandardValuesCollection GetStandardValues( ITypeDescriptorContext context ) {
			TestCase test = (TestCase) context.Instance;
			return new StandardValuesCollection( test.Scenario.Lists.Select( v => v.Name ).ToArray() );
		}
	}

	/// <summary>
	/// Provides a drop-down list for table variable names.
	/// </summary>
	public class TableVariableNameTypeConverter : TypeConverter {
		public override bool GetStandardValuesSupported( ITypeDescriptorContext context ) {
			TestCase test = context.Instance as TestCase;
			return test != null && test.Scenario != null;
		}

		public override bool GetStandardValuesExclusive( ITypeDescriptorContext context ) {
			return false;
		}

		public override StandardValuesCollection GetStandardValues( ITypeDescriptorContext context ) {
			TestCase test = (TestCase) context.Instance;
			return new StandardValuesCollection( test.Scenario.Tables.Select( v => v.Name ).ToArray() );
		}
	}
}
