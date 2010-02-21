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

namespace QAliber.Repository.CommonTestCases.UI.Dialogs
{
	/// <summary>
	/// Displays a message box with an OK button to the user
	/// <preconditions>None</preconditions>
	/// <postconditions>None</postconditions>
	/// <workflow>
	/// <action>Pop a message box</action>
	/// <verification>None</verification>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Dialogs")]
	public class DisplayInputBox : TestCase
	{
		public DisplayInputBox()
		{
			name = "Display Input Box";
			icon = Properties.Resources.Dialog;
		}

		private string title = "";

		/// <summary>
		/// The title of the input box
		/// </summary>
		[Category("Dialog")]
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private string text = "";

		/// <summary>
		/// The text to display inside the input box
		/// </summary>
		[Category("Dialog")]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		
		private string inputString;

		/// <summary>
		/// The input the user entered
		/// </summary>
		[Category("Dialog")]
		[DisplayName("User's Input")]
		public string InputString
		{
			get { return inputString; }
		}
	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			InputForm inputForm = new InputForm(title, text);
			inputForm.ShowDialog();
			inputString = inputForm.Input;
		}

		

	}


}
