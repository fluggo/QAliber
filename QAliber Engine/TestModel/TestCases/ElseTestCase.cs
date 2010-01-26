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
	/// Executes its children if the last evaluated if was false
	/// <preconditions>A previous if was evaluated</preconditions>
	/// <seealso cref="T:QAliber.TestModel.IfTestCase"/>
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Conditions")]
	public class ElseTestCase : FolderTestCase
	{
		public ElseTestCase()
		{
			name = "Else";
			icon = Properties.Resources.If;
			
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			Log.Default.Info("Last If result = " + IfTestCase.ifConditionValue);
			if (!IfTestCase.ifConditionValue)
			{
				base.Body();
			}
		}

		public override string Description
		{
			get
			{
				return "Performs else on last if's result";
			}
			set
			{
				base.Description = value;
			}
		}
	}
	
}
