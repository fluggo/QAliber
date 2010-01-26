using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;

namespace QAliber.Repository.CommonTestCases.UI.Keyboard
{
	/// <summary>
	/// Send keyboard keys to a specific control
	/// <preconditions>Control should exists and be enabled</preconditions>
	/// <postconditions>Keys are sent</postconditions>
	/// <workflow>
	/// <verification>Control exists</verification>
	/// <action>Set focus to control</action>
	/// <action>Send keys</action>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Keyboard")]
	public class SendKeystrokes : TestCase
	{
		public SendKeystrokes()
		{
			name = "Send Keystrokes";
			icon = Properties.Resources.Keyboard;
		}

		private string control = "";


		/// <summary>
		/// The control to send the keys to
		/// </summary>
		[Category("Keyboard")]
		[DisplayName("1) Control")]
		[Description("The control to send the keystrokes too")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string keystrokes;

		/// <summary>
		/// A string that represents the keys to be sent.
		/// <remarks>For special keys send them in '{}', for a list of available special keys see &lt;a href="http://msdn.microsoft.com/en-us/library/system.windows.input.key.aspx"&gt;microsoft documentation&lt;/a&gt;. To group keys together use '()' braces
		/// </remarks>
		/// <example>To send Alt+f and then s use '({LeftAlt}f)s'</example>
		/// </summary>
		[Category("Keyboard")]
		[DisplayName("2) Keystrokes")]
		[Description("The keys to send, special keys - e.g Enter, Tab - should be wrapped with {}\nsimulataneous click should be wrapped with ()\nFor example pressing ctrl+s ({LeftCtrl}s)")]
		public string Keystrokes
		{
			get { return keystrokes; }
			set { keystrokes = value; }
		}
	
		public override void Body()
		{
			string code = "UIControlBase c = " + control + ";\n";
			code += "c.Write(\"" + keystrokes + "\");\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Sending the keys '" + keystrokes + "' to path '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
