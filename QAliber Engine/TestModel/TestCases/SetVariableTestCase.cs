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
	/// Sets a variable in run-time
	/// </summary>
	[Serializable]
	[VisualPath(@"Variables")]
	public class SetVariableTestCase : TestCase
	{
		public SetVariableTestCase()
		{
			name = "Set Variable";
			icon = null;
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

		private string varName;

		/// <summary>
		/// The variable name
		/// </summary>
		[Category(" Variables")]
		[DisplayName("Variable Name")]
		public string VariableName
		{
			get { return varName; }
			set { varName = value; }
		}
	
		public override void Body()
		{
			object retVal = null;
			Eval.CodeEvaluator.Evaluate(cSharpExpression, retType, out retVal);
			Scenario.Variables.AddOrReplaceByName(new QAliber.TestModel.Variables.ScenarioVariable(
				varName, retVal.ToString(), null));

		}

		public override string Description
		{
			get
			{
				return string.Format("Setting variable '{0}' to '{1}'", varName, cSharpExpression);
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	}
	
}
