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
using System.Drawing;
using System.ComponentModel;
using QAliber.Logger;
using System.Xml.Serialization;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Patterns;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{

	/// <summary>
	/// Moves a top level window to the specified position
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	[XmlType("MoveWindow", Namespace=Util.XmlNamespace)]
	public class MoveWindow : TestCase
	{
		public MoveWindow() : base( "Move Window" )
		{
			Icon = Properties.Resources.Window;
		}

		private string control = "";

		/// <summary>
		/// The window to move, make sure the 'UIType' in the locator dialog is 'UIAWindow'
		/// </summary>
		[Category("Behavior")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Point point;

		/// <summary>
		/// The coordinate in pixels, relative to the upper left corner of the desktop to move the window to
		/// </summary>
		[Category("Behavior")]
		[DisplayName("Point")]
		[Description("Point where the upper-left corner of the window should be.")]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}
	
	
		public override void Body( TestRun run )
		{
			ActualResult = TestCaseResult.Passed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				ActualResult = TestCaseResult.Failed;
				throw new InvalidOperationException("Control not found");
			}

			ITransformPattern transform = c.GetControlInterface<ITransformPattern>();

			if( transform == null ) {
				ActualResult = TestCaseResult.Failed;
				throw new InvalidOperationException( "Control doesn't appear to be a window" );
			}

			transform.Move( point.X, point.Y );
		}

		public override string Description
		{
			get
			{
				return "Moving window '" + control + "' to point " + point;
			}
		}

	}
}
