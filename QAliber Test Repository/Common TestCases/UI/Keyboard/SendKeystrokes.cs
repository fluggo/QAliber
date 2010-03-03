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
using QAliber.Engine.Controls;

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
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			try
			{
				string code = "UIControlBase c = " + control + ";\nreturn c;\n";
				UIControlBase c = (UIControlBase)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);
				if (c == null)
				{
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					return;
				}
				c.Write(keystrokes);
			}
			catch (System.Reflection.TargetInvocationException)
			{
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}

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
