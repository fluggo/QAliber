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

namespace QAliber.TestModel
{
	/// <summary>
	/// Performs an if condition on a given expression
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Conditions")]
	public class IfTestCase : FolderTestCase
	{
		public IfTestCase()
		{
			name = "If";
			icon = Properties.Resources.If;
		}

		private string cSharpExpression;

		/// <summary>
		/// A C# expression that should return true or false (you can include variables as well)
		/// <example>If you have a variable named 'myVar' you can set an expression which tests if myVar equals 'hello world', like this : "$myVar$" == "hello world"</example>
		/// <remarks>To learn more about C# syntax in expressions click &lt;a href="http://msdn.microsoft.com/en-us/library/aa691304(VS.71).aspx"&gt;here&lt;/a&gt;</remarks>
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("C# Expression")]
		[Description("An expression written in C# that return true or false (the expression can contain variables)")]
		public string CSharpExpression
		{
			get { return cSharpExpression; }
			set { cSharpExpression = value; }
		}

		public override void Body()
		{
			object conditionResult = null;
			ifConditionValue = false;
			Eval.CodeEvaluator.Evaluate(cSharpExpression, QAliber.TestModel.Eval.ReturnCodeType.Boolean, out conditionResult);
			ifConditionValue = (bool)conditionResult;
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			if ((bool)conditionResult)
			{
				Log.Default.Info(cSharpExpression + " = true");
				base.Body();
			}
			else
			{
				Log.Default.Info(cSharpExpression + " = false");
			}
		}

		public override string Description
		{
			get
			{
				return "If '" + cSharpExpression + "'";
			}
		}

		internal static bool ifConditionValue;
	
	}
	
}
