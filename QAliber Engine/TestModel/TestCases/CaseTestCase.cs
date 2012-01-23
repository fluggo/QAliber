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
	/// Executes its children if the evaluated switch equals this case
	/// <preconditions>Must be a children of a 'Switch' test csae</preconditions>
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Conditions")]
	public class CaseTestCase : FolderTestCase
	{
		public CaseTestCase()
		{
			name = "Case";
			icon = Properties.Resources.Case;

		}

		public override void Body()
		{
			if (!(Parent is SwitchTestCase))
			{
				throw new InvalidOperationException("Parent must be switch");
			}

			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		   
			if (expectedCase == SwitchTestCase.switchConditionValue)
			{
				Log.Default.Info("Last switch = " + SwitchTestCase.switchConditionValue);
				base.Body();
			}
		}

		private string expectedCase;

		/// <summary>
		/// A text represnting the case to execute the children, if matching the evaluatred switch
		/// </summary>
		[Category("Case")]
		[Description("The expected case matching the parent switch")]
		[DisplayName("Expected Case")]
		public string ExpectedCase
		{
			get { return expectedCase; }
			set { expectedCase = value; }
		}

		public override string Description
		{
			get
			{
				return "Evaluating switch on '" + expectedCase + "'";
			}
		}
	}
	
}
