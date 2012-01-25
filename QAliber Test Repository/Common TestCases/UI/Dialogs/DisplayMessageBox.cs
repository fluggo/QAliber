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
	[XmlType("DisplayMessageBox", Namespace=Util.XmlNamespace)]
	public class DisplayMessageBox : TestCase
	{
		public DisplayMessageBox() : base( "Display Message Box" )
		{
			icon = Properties.Resources.Dialog;
		}

		private string title = "";

		/// <summary>
		/// The title of the message box
		/// </summary>
		[Category("Dialog")]
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private string text = "";

		/// <summary>
		/// The text to display inside the message box
		/// </summary>
		[Category("Dialog")]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		private MessageBoxButtons buttons = MessageBoxButtons.OK;

		/// <summary>
		/// The buttons to show on the message box
		/// </summary>
		[Category("Dialog")]
		public MessageBoxButtons Buttons
		{
			get { return buttons; }
			set { buttons = value; }
		}

		private DialogResult dialogResult;

		/// <summary>
		/// The button the user hit
		/// </summary>
		[Category("Dialog")]
		[DisplayName("Dialog Result")]
		public DialogResult DialogResult
		{
			get { return dialogResult; }
		}
	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			dialogResult = MessageBox.Show(text, title, buttons);
			Log.Default.Info("The user pressed '" + dialogResult + "'");
		}

	}


}
