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
	/// Loop through this tes case's children X times
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	public class LoopTestCase : FolderTestCase
	{
		public LoopTestCase()
		{
			name = "Loop";
			icon = Properties.Resources.Loop;
		}

		protected int numOfLoops = 1;

		/// <summary>
		/// The number of loops to run through all the children
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Number Of Loops")]
		[Description("The number of loops all the descendants will run")]
		public int NumOfLoops
		{
			get { return numOfLoops; }
			set { numOfLoops = value; }
		}

		public override void Body()
		{
			for (int i = 0; i < numOfLoops; i++)
			{
				scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable("CurrentLoopNumber", ((int)(i+1)).ToString(), this));
				Log.Default.IndentIn(name + " - Loop #" + (int)(i + 1));
				base.Body();
				Log.Default.IndentOut();
				if (exitTotally)
					break;
				if (branchesToBreak > 0)
				{
					branchesToBreak--;
					break;
				}
			}
		}

		public override string Description
		{
			get
			{
				return "Looping " + numOfLoops + " Times";
			}
			set
			{
				base.Description = value;
			}
		}

	}

}
