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
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;
using QAliber.TestModel.Variables;

namespace QAliber.TestModel
{
	/// <summary>
	/// Sets a variable in run-time
	/// </summary>
	[Serializable]
	[VisualPath(@"Variables")]
	[XmlType("SetVariable", Namespace=Util.XmlNamespace)]
	public class SetVariableTestCase : TestCase
	{
		public SetVariableTestCase() : base( "Set Variable" )
		{
			Icon = null;
		}

		private string cSharpExpression;

		/// <summary>
		/// A C# expression that should return the variables value (you can include variables as well)
		/// <example>If you have a variable named 'myVar' you can set an expression which increases it by 1, like this : $myVar$++</example>
		/// <remarks>To learn more about C# syntax in expressions click &lt;a href="http://msdn.microsoft.com/en-us/library/aa691304(VS.71).aspx"&gt;here&lt;/a&gt;</remarks>
		/// </summary>
		[Category(" Variables")]
		[DisplayName("C# Expression")]
		[Description("An expression written in C# that will set the variable")]
		public string CSharpExpression
		{
			get { return cSharpExpression; }
			set { cSharpExpression = value; }
		}

		private Eval.ReturnCodeType retType;

		/// <summary>
		/// How to treat the variable returned from the C# expression
		/// </summary>
		[Category(" Variables")]
		[DisplayName("Treat As")]
		[Description("What would you like to return from your C# expression")]
		public Eval.ReturnCodeType ReturnType
		{
			get { return retType; }
			set { retType = value; }
		}

		private string varName = string.Empty;

		/// <summary>
		/// The variable name
		/// </summary>
		[Category(" Variables")]
		[DisplayName("Variable Name")]
		[TypeConverter(typeof(StringVariableNameTypeConverter))]
		public string VariableName
		{
			get { return varName; }
			set { varName = value; }
		}
	
		public override void Body( TestRun run )
		{
			object retVal = null;
			ActualResult = TestCaseResult.Passed;
			Eval.CodeEvaluator.Evaluate(cSharpExpression, retType, out retVal);
			run.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>(
				varName, retVal.ToString(), this));

		}

		public override string Description
		{
			get
			{
				return string.Format("Setting variable '{0}' to '{1}'", varName, cSharpExpression);
			}
		}
	
	
	}
	
}
