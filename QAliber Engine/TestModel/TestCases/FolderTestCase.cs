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
	/// <summary>
	/// A simple folder for more organized and hierarchic test scenario, you can add test cases for this folder
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	public class FolderTestCase : TestCase
	{
		public FolderTestCase()
		{
			name = "Folder";
			expectedResult = TestCaseResult.Passed;
			icon = Properties.Resources.Folder;
			
		}

		protected List<TestCase> children = new List<TestCase>();

		/// <summary>
		/// The children of this test case
		/// </summary>
		[Browsable(false)]
		public List<TestCase> Children
		{
			get { return children; }
			set { children = value; }
		}
		
		public override void Body()
		{
			actualResult = TestCaseResult.Passed;
			foreach (TestCase child in children)
			{
				if (child.MarkedForExecution)
				{
					child.Run();
					
					if (child.ExpectedResult != TestCaseResult.None && child.ActualResult != child.ExpectedResult)
					{
						actualResult = TestCaseResult.Failed;
						if (child.ExitOnError)
						{
							Log.Default.Error("Stop on error was requested for this step, exiting");
							exitTotally = true;
							break;
						}
						if (child.ExitBranchOnError)
						{
							Log.Default.Info("Exiting from branch on error as requested");
							break;
						}
					}
					if (branchesToBreak > 0)
					{
						branchesToBreak--;
						break;
					}
					if (exitTotally)
						break;
					
				}
				
				
			}
		}
	}
	
}
