using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	public class MoveWindow : TestCase
	{
		public MoveWindow()
		{
			name = "Move Window";
			icon = Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Window")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Point point;

		[Category("Window")]
		[Description("The coordinate in pixels, relative to the upper left corner of the desktop to move the window to")]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}
	
	
		public override void Body()
		{
			string code = "UIAWindow w = " + control + " as UIAWindow;\n";
			code += "w.Move(" + point.X + ", " + point.Y + ");\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Moving window '" + control + "' to point " + point;
			}
			set
			{
				base.Description = value;
			}
		}

	}
}
