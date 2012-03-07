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
using System.Xml.Serialization;

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
	[XmlType("SendKeystrokes", Namespace=Util.XmlNamespace)]
	public class SendKeystrokes : TestCase
	{
		public SendKeystrokes() : base( "Send Keystrokes" )
		{
			Icon = Properties.Resources.Keyboard;
		}

		private string control = "";


		/// <summary>
		/// The control to send the keys to
		/// </summary>
		[Category("Behavior")]
		[Description("The control to receive the keystrokes.")]
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
		[Category("Behavior")]
		[DisplayName("Keystrokes")]
		[Description("The keys to send. Special keys, such as Enter or Tab, should be wrapped with {}. To press more than one key at a time, use (). For example, pressing Ctrl+s is ({LeftCtrl}s).")]
		public string Keystrokes
		{
			get { return keystrokes; }
			set { keystrokes = value; }
		}
	
		public override void Body()
		{
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			try
			{
				UIControlBase c = UIControlBase.FindControlByPath( control );

				if( !c.Exists ) {
					ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					throw new InvalidOperationException("Control not found");
				}

				c.Write(keystrokes);
			}
			catch (System.Reflection.TargetInvocationException)
			{
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}

		}

		public override string Description
		{
			get
			{
				return "Sending the keys '" + keystrokes + "' to path '" + control + "'";
			}
		}

		

	}


}
