using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

using QAliber.Logger;
using QAliber.TestModel.Attributes;

namespace QAliber.TestModel
{
	/// <summary>
	/// Breaks N levels up from folders (including loops, if's and any test case that as children)
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	public class BreakLoopTestCase : TestCase
	{
		public BreakLoopTestCase()
		{
			name = "Cut Branches";
			icon = Properties.Resources.BreakFolder;
			
		}

		protected uint numOfLoopsToBreak = 1;

		/// <summary>
		/// The number of levels up the tree you want to go back
		/// <example>1 - will execute the next sibling's of the test case's parent</example>
		/// <example>2 - will execute the next sibling's of the test case's grand parent, and so on</example>
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Number Of Branches To Cut")]
		[Description("How many levels up the tree do you want to break to ?\n e.g. '1' will exit the current branch")]
		public uint NumOfLoopsToBreak
		{
			get { return numOfLoopsToBreak; }
			set { numOfLoopsToBreak = value; }
		}

		public override void Body()
		{
			branchesToBreak = numOfLoopsToBreak;
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Breaking " + numOfLoopsToBreak + " Levels Up The Tree";
			}
			set
			{
				base.Description = value;
			}
		}

	}

}
