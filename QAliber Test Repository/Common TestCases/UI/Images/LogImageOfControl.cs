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
using System.Drawing;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public class LogImageOfControl : TestCase
	{
		public LogImageOfControl()
		{
			name = "Log Image Of Control";
			icon = Properties.Resources.Bitmap;
		}

		private string control = "";


		[Category("Image")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string logDescription;

		[Category("Image")]
		[DisplayName("2) Log Description")]
		[Description("The description that will appear in the log when this image is logged")]
		public string LogDescription
		{
			get { return logDescription; }
			set { logDescription = value; }
		}

	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			string code = "UIControlBase c = " + control + ";\n";
			code += "return c.GetImage();\n";
			Bitmap image = (Bitmap)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);
			Logger.Log.Default.Image(image, logDescription);

		}

		public override string Description
		{
			get
			{
				return "Logging image for path '" + control + "'";
			}
		}

	}


}
