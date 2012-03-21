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
using QAliber.RemotingModel;
using QAliber.Engine.Patterns;

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
		[DisplayName("Start Control")]
		[Editor(typeof(UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CoordinateProperty("Coordinate")]
		public string Control
		{
			get { return control; }
			set { control = value; }
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

		private string _endControl = "";

		[Category("Behavior")]
		[DisplayName("End Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description("The control where the drag will end. This can be blank if you want to end up in the same control.")]
		[CoordinateProperty("BottomRightCoordinate")]
		public string EndControl
		{
			get { return _endControl; }
			set { _endControl = value; }
		}

		private Point _point2;

		[Category("Behavior")]
		[DisplayName("End Point")]
		[Description("The coordinate in pixels, relative to the upper left corner of the control you selected, to end the drag.")]
		public Point BottomRightCoordinate
		{
			get { return _point2; }
			set { _point2 = value; }
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

		private bool _scrollIntoView = true;

		[Category("Behavior")]
		[DisplayName("Scroll Into View")]
		[Description("If either control is a scroll item, set this to true to scroll it into view before trying to click it.")]
		[DefaultValue(true)]
		public bool ScrollIntoView
		{
			get { return _scrollIntoView; }
			set { _scrollIntoView = value; }
		}

		public override void Body()
		{
			ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				Log.Default.Error( "Start control not found." );
				return;
			}

			IScrollItemPattern pattern = c.GetControlInterface<IScrollItemPattern>();

			if( pattern != null && _scrollIntoView ) {
				pattern.ScrollIntoView();
			}

			if( !c.Visible ) {
				Log.Default.Error( "Start control not visible." );
				return;
			}

			Point point2 = _point2;

			if( !string.IsNullOrEmpty( _endControl ) ) {
				UIControlBase endControl = UIControlBase.FindControlByPath( _endControl );

				if( !endControl.Exists ) {
					Log.Default.Error( "End control not found." );
					return;
				}

				pattern = endControl.GetControlInterface<IScrollItemPattern>();

				if( pattern != null && _scrollIntoView ) {
					pattern.ScrollIntoView();
				}

				if( !endControl.Visible ) {
					Log.Default.Error( "End control not visible." );
					return;
				}

				// Figure out end coordinate relative to begin coord
				Vector offset = endControl.Layout.TopLeft - c.Layout.TopLeft;
				point2 += offset;
			}

			c.Drag(button, point1, point2);

			ActualResult = TestCaseResult.Passed;
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
