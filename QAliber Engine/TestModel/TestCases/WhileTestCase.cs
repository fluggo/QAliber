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
	/// Continues to execute all the test case's children over and over until the specified expression returns true
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	public class WhileTestCase : FolderTestCase
	{
		public WhileTestCase()
		{
			name = "While";
			icon = Properties.Resources.Loop;
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

		private long timeout = 0;

		/// <summary>
		/// The longest time to wait for the C# expression to return true, after that an error will be posted and the run will continue, for infinte time enter 0
		/// </summary>
		[Category("Test Case Flow Control")]
		[Description("If the timeout(in milliseconds) reached, the while loop will exit\nFor no timeout enter 0")]
		public long Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}
	

		public override void Body()
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Start();
			object conditionResult = null;
			Eval.CodeEvaluator.Evaluate(cSharpExpression, QAliber.TestModel.Eval.ReturnCodeType.Boolean, out conditionResult);
			while ((bool)conditionResult)
			{
				if (timeout > 0 && watch.ElapsedMilliseconds > timeout)
				{
					Log.Default.Warning("While loop exited after timeout of " + timeout + " milliseconds");
					break;
				}
				base.Body();
				if (branchesToBreak > 0)
				{
					branchesToBreak--;
					break;
				}
				Eval.CodeEvaluator.Evaluate(cSharpExpression, QAliber.TestModel.Eval.ReturnCodeType.Boolean, out conditionResult);
			}
			
		}

		public override string Description
		{
			get
			{
				return "While '" + cSharpExpression + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	}
	
}