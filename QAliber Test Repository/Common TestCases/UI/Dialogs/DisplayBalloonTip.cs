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
			set
			{
				base.Description = value;
			}
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			Notifier.Instance.notifyIcon.ShowBalloonTip(timeout, title, text, messageIcon);
		}

	}


}
