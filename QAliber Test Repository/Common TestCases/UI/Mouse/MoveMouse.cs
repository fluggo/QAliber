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

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Mouse")]
	public class MoveMouse : TestCase, QAliber.Repository.CommonTestCases.UITypeEditors.ICoordinate
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
				return "Moving mouse to path '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
