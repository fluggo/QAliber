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
	public class ClickMouse : TestCase
	{
		public ClickMouse()
		{
			name = "Click Mouse";
			icon = Properties.Resources.Mouse;
		}

		private string control = "";

		
		[Category("Mouse")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Point point;

		[Category("Mouse")]
		[DisplayName("3) Coordinate")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected")]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}

		private MouseButtons button = MouseButtons.Left;

		[Category("Mouse")]
		[DisplayName("2) Button")]
		[Description("The mouse button to click")]
		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}
	
	
		public override void Body()
		{
			string code = "UIControlBase c = " + control + ";\n";
			code += "c.Click(MouseButtons." + button + ", new Point(" + point.X + ", " + point.Y + "));\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Clicking " +  button + "button mouse on path '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
