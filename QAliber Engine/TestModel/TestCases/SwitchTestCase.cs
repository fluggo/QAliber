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
	/// Evaluates switch case on a given expression
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Conditions")]
	[XmlType("Switch", Namespace=Util.XmlNamespace)]
	public class SwitchTestCase : FolderTestCase
	{
		public SwitchTestCase() : base( "Switch" )
		{
			Icon = Properties.Resources.Switch;
		}

		private string cSharpExpression;

		/// <summary>
		/// A C# expression that should return a value later can be parsed into string for Case evaluation (you can include variables as well)
		/// <example>DateTime.Now; Note though the returning value will be DateTime it will be parsed to string</example>
		/// <remarks>To learn more about C# syntax in expressions click &lt;a href="http://msdn.microsoft.com/en-us/library/aa691304(VS.71).aspx"&gt;here&lt;/a&gt;</remarks>
		/// </summary>
		[Category(" Switch")]
		[DisplayName("C# Expression")]
		[Description("a C# expression that return string (suround by quats or use ToString)")]
		public string CSharpExpression
		{
			get { return cSharpExpression; }
			set { cSharpExpression = value; }
		}

		public override void Body()
		{
			object conditionResult = null;
			Eval.CodeEvaluator.Evaluate(cSharpExpression, QAliber.TestModel.Eval.ReturnCodeType.Text, out conditionResult);
			switchConditionValue = conditionResult.ToString();
			Log.Default.Info("Switch value = " + switchConditionValue);
			base.Body();
		}

		public override string Description
		{
			get
			{
				return "Switch on '" + cSharpExpression + "'";
			}
		}

		internal static string switchConditionValue;
	
	}
	
}
