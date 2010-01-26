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
	public class DisplayMessageBox : TestCase
	{
		public DisplayMessageBox()
		{
			name = "Display Message Box";
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
			dialogResult = MessageBox.Show(text, title, buttons);
			Log.Default.Info("The user pressed '" + dialogResult + "'");
		}

	}


}
