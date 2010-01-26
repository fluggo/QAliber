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
	public class TryTestCase : FolderTestCase
	{
		public TryTestCase()
		{
			name = "Try";
			icon = Properties.Resources.Try;
		}

		private void SetExitOnErrorRec(FolderTestCase testcase)
		{
			foreach (TestCase child in testcase.Children)
			{
				child.ExitOnError = true;
				if (child is FolderTestCase)
					SetExitOnErrorRec((FolderTestCase)child);
			}
		}

		public override void Body()
		{
			lastError = string.Empty;
			SetExitOnErrorRec(this);
			Log.Default.BeforeErrorIsPosted += new EventHandler<LogEventArgs>(BeforeErrorIsPosted);
			base.Body();
			exitTotally = false;
			if (actualResult == TestCaseResult.Failed)
				lastError = errListener;
			Log.Default.BeforeErrorIsPosted -= new EventHandler<LogEventArgs>(BeforeErrorIsPosted);
		}

		private void BeforeErrorIsPosted(object sender, LogEventArgs e)
		{
			errListener += e.LogEntryProperties.Message;
		}

		internal static string lastError = string.Empty;
		private string errListener = string.Empty;
	}
	
}
