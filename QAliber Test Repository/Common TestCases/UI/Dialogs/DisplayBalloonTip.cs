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
	/// Displays a ballon tip with an message to the user
	/// <preconditions>None</preconditions>
	/// <postconditions>A balloon tip is being deiplayed</postconditions>
	/// <workflow>
	/// <action>Pop a balloon tip</action>
	/// <verification>None</verification>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Dialogs")]
	public class DisplayBalloonTip : TestCase
	{
		public DisplayBalloonTip()
		{
			name = "Display Balloon Tip";
			icon = Properties.Resources.Dialog;
		}

		private string title = "";

		/// <summary>
		/// The title of the balloon
		/// </summary>
		[Category("Dialog")]
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private string text = "";

		/// <summary>
		/// The text to display inside the balloon
		/// </summary>
		[Category("Dialog")]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		private int timeout = 5000;

		/// <summary>
		/// The text to display inside the balloon
		/// </summary>
		[Category("Dialog")]
		[Description("The time the balloon will appear (in milliseconds)")]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		private ToolTipIcon messageIcon = ToolTipIcon.Info;

		/// <summary>
		/// The icon to display inside the balloon
		/// </summary>
		[Category("Dialog")]
		[Description("The icon type to show")]
		[DisplayName("Message Icon")]
		public ToolTipIcon MessageIcon
		{
			get { return messageIcon; }
			set { messageIcon = value; }
		}

		public override string Description
		{
			get
			{
				return "Showing a balloon tip saying : '" + text + "'";
			}
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			Notifier.Instance.notifyIcon.ShowBalloonTip(timeout, title, text, messageIcon);
		}

	}


}
