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
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Mouse")]
	public class MoveMouse : TestCase
	{
		public MoveMouse()
		{
			name = "Move Mouse";
			icon = Properties.Resources.Mouse;
		}

		private string control = "";

		
		[Category("Mouse")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Point point;

		[Category("Mouse")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected")]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}
	
		public override void Body()
		{
			string code = "UIControlBase c = " + control + ";\n";
			code += "c.MoveMouseTo(new Point(" + point.X + ", " + point.Y + "));\n";
			code += "return null;\n"; 
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Moving mouse on path '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
