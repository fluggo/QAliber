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
	public class DragMouse : TestCase, QAliber.Repository.CommonTestCases.UITypeEditors.ICoordinate
	{
		public DragMouse()
		{
			name = "Drag Mouse";
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

		private Point point1;

		[Category("Mouse")]
		[DisplayName("3) Top Left Coordiante")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected, to start the drag from")]
		public Point Coordinate
		{
			get { return point1; }
			set { point1 = value; }
		}

		private Point point2;

		[Category("Mouse")]
		[DisplayName("4) Bottom Right Coordiante")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected, to end the drag")]
		public Point BottomRightCoordinate
		{
			get { return point2; }
			set { point2 = value; }
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
			code += "c.Drag(MouseButtons." + button + ", new Point(" + point1.X + ", " + point1.Y + ")" + ", new Point(" + point2.X + ", " + point2.Y + "));\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Dragging " + button + " button mouse on path '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
