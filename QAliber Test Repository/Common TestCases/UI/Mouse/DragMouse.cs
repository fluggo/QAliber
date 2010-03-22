/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.Engine.Controls;

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
		[TypeConverter(typeof(ExpandableObjectConverter))]
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
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			try
			{
				string code = "UIControlBase c = " + control + ";\nreturn c;\n";
				UIControlBase c = (UIControlBase)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);
				if (c == null)
				{
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					return;
				}
				c.Drag(button, point1, point2);
			}
			catch (System.Reflection.TargetInvocationException)
			{
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}


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
