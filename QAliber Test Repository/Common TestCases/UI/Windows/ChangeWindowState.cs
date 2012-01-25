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
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	[XmlType("ChangeWindowState", Namespace=Util.XmlNamespace)]
	public class ChangeWindowState : TestCase
	{
		/// <summary>
		/// Activate, minimize, maximize, restore or close the window 
		/// </summary>
		public ChangeWindowState() : base( "Change Window State" )
		{
			icon = QAliber.Repository.CommonTestCases.Properties.Resources.Window;
		}

		private string control = "";

		/// <summary>
		/// The window to change, make sure the 'UIType' in the locator dialog is 'UIAWindow'
		/// </summary>
		[Category(" Window")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private WindowOperationType opType;

		/// <summary>
		/// The operation you want to perform on the window
		/// </summary>
		[Category(" Window")]
		[Description("Select the operation you want to perform on the window")]
		public WindowOperationType Operation
		{
			get { return opType; }
			set { opType = value; }
		}
	
	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			string code = "UIAWindow w = " + control + " as UIAWindow;\n";
			code += "w." + opType + "();\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return opType + " is performed on window '" + control + "'";
			}
		}

	}

	public enum WindowOperationType
	{
		SetFocus,
		Close,
		Minimize,
		Maximize,
		Restore
	}


}
