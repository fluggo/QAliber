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

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Mouse")]
	[XmlType("DoubleClickMouse", Namespace=Util.XmlNamespace)]
	public class DoubleClickMouse : TestCase, QAliber.Repository.CommonTestCases.UITypeEditors.ICoordinate
	{
		public DoubleClickMouse() : base( "Double-Click Mouse" )
		{
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
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}

		private MouseButtons button = MouseButtons.Left;

		[Category("Mouse")]
		[DisplayName("2) Button")]
		[Description("The mouse button to double click")]
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
				UIControlBase c = UIControlBase.FindControlByPath( control );

				if( !c.Exists ) {
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					throw new InvalidOperationException("Control not found");
				}

				c.DoubleClick(button, point);
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
				return "Double clicking " +  button + " button mouse on path '" + control + "'";
			}
		}

		

	}


}
