using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;



namespace QAliber.Repository.CommonTestCases.Registry
{
	/// <summary>
	/// Generates a random number
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Variables\Randomizers")]
	public class GenerateRandomNumberTestCase : global::QAliber.TestModel.TestCase
	{
		public GenerateRandomNumberTestCase()
		{
			name = "Generate Random Number";
			icon = null;
		}

		public override void Body()
		{
			int range = (int)((maxVal - minVal) / step);
			int rndVal = new Random().Next(0, range);
			generatedNum = minVal + rndVal * step;
			Log.Default.Info("The generated number is " + generatedNum);
		}

		private double minVal = 0;

		/// <summary>
		/// The minimum value of the range to randomize on
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("1) Minimum Value")]
		public double MinVal
		{
			get { return minVal; }
			set { minVal = value; }
		}

		private double maxVal = 100;

		/// <summary>
		/// The maximum value of the range to randomize on
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("2) Maximum Value")]
		public double MaxVal
		{
			get { return maxVal; }
			set { maxVal = value; }
		}

		private double step = 1;

		/// <summary>
		/// The resolution of the return value
		/// <example>if you want to generate a number between 10 - 20 in a 0.1 resolution, fill minimum = 10; maximum = 20; step = 0.1</example>
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("Step")]
		[Description("The resolution in which to return the result")]
		public double Step
		{
			get { return step; }
			set { step = value; }
		}


		private double generatedNum;

		/// <summary>
		/// Read-only - the number that was generated
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("Generated Number")]
		public double GeneratedNumber
		{
			get { return generatedNum; }
		}
	
	

		public override string Description
		{
			get
			{
				return string.Format("Generating number between {0} to {1} in {2} step", minVal, maxVal, step);
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}
