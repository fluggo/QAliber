using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;
using QAliber.RemotingModel;

namespace QAliber.TestModel
{
	[Serializable]
	[VisualPath(@"Flow Control\Recovery")]
	public class RecoverTestCase : FolderTestCase
	{
		/// <summary>
		/// Try to recover from a general/specific error
		/// <preconditions>A previous 'Try' test case was performed</preconditions>
		/// </summary>
		public RecoverTestCase()
		{
			name = "Recover";
			icon = Properties.Resources.Recover;
		}

		private string errorToCatch = string.Empty;

		/// <summary>
		/// If the errors in the log inside a 'Try' folder contains this parameter, this folder will be executed. To execute on any error, leave it blank
		/// </summary>
		[Category(" Recovery")]
		[DisplayName("Error To Catch")]
		[Description("If the errors in the log inside a 'Try' folder contains this parameter, this folder will be executed.\nTo execute on any error, leave it blank")]
		public string ErrorToCatch
		{
			get { return errorToCatch; }
			set { errorToCatch = value; }
		}

		public override void Body()
		{
			actualResult = TestCaseResult.Passed;
			if (!string.IsNullOrEmpty(TryTestCase.lastError) && TryTestCase.lastError.Contains(errorToCatch))
				base.Body();
			else
				Log.Default.Info("Recovery is not needed");
		}
		
	}
	
}
