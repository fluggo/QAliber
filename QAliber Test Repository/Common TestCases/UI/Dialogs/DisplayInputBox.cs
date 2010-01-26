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
			InputForm inputForm = new InputForm(title, text);
			inputForm.ShowDialog();
			inputString = inputForm.Input;
		}

		

	}


}
