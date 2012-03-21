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
using System.Xml.Serialization;
using QAliber.Repository.CommonTestCases.UITypeEditors;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Mouse")]
	[XmlType("DragMouse", Namespace=Util.XmlNamespace)]
	public class DragMouse : TestCase
	{
		public DragMouse() : base( "Drag Mouse" )
		{
			Icon = Properties.Resources.Mouse;
		}

		private string control = "";

		[Category("Behavior")]
		[DisplayName("Control")]
		[Editor(typeof(UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CoordinateProperty("Coordinate")]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private MouseButtons button = MouseButtons.Left;

		[Category("Behavior")]
		[DisplayName("Mouse Button")]
		[Description("The mouse button to click.")]
		[DefaultValue(MouseButtons.Left)]
		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}

		private Point point1;

		[Category("Behavior")]
		[DisplayName("Start Point")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected, to start the drag from.")]
		public Point Coordinate
		{
			get { return point1; }
			set { point1 = value; }
		}

		private Point point2;

		[Category("Behavior")]
		[DisplayName("End Point")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected, to end the drag.")]
		public Point BottomRightCoordinate
		{
			get { return point2; }
			set { point2 = value; }
		}

		public override void Body()
		{
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			try
			{
				UIControlBase c = UIControlBase.FindControlByPath( control );

				if( !c.Exists ) {
					ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					Log.Default.Error( "Control not found." );
				}

				c.Drag(button, point1, point2);
			}
			catch (System.Reflection.TargetInvocationException)
			{
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}


		}

		public override string Description
		{
			get
			{
				return "Dragging " + button + " button mouse on path '" + control + "'";
			}
		}

	   
	}


}
