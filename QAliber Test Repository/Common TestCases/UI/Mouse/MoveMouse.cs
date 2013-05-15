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
	[XmlType("MoveMouse", Namespace=Util.XmlNamespace)]
	public class MoveMouse : TestCase
	{
		public MoveMouse() : base( "Move mouse" )
		{
			Icon = Properties.Resources.Mouse;
		}

		private string control = "";
		private string _targetName = null;

		[Category("Behavior")]
		[Editor(typeof(UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CoordinateProperty("Coordinate")]
		public string Control
		{
			get { return control; }
			set {
				try {
					_targetName = Util.GetControlNameFromXPath( value );
				}
				catch {
					_targetName = null;
				}

				OnDefaultNameChanged();
				control = value;
			}
		}

		protected override string DefaultName {
			get {
				if( _targetName == null )
					return base.DefaultName;

				return string.Format( "Move mouse over \"{0}\"", _targetName );
			}
		}

		private Point point;

		[Category("Behavior")]
		[DisplayName("Point")]
		[Description("The coordinate in pixels, relative to the upper-left corner of the control you selected.")]
		public Point Coordinate
		{
			get { return point; }
			set { point = value; }
		}
	
		public override void Body( TestRun run )
		{
			ActualResult = TestCaseResult.Passed;

			try
			{
				UIControlBase c = UIControlBase.FindControlByPath( control );

				if( !c.Exists ) {
					ActualResult = TestCaseResult.Failed;
					throw new InvalidOperationException("Control not found");
				}

				c.MoveMouseTo(point);
			}
			catch (System.Reflection.TargetInvocationException)
			{
				ActualResult = TestCaseResult.Failed;
			}

		}

		public override string Description
		{
			get
			{
				return "Moving mouse to path '" + control + "'";
			}
		}

	}


}
