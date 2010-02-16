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
using System.Windows.Automation;
using System.Windows.Forms;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIAEditBox class represents an Edit control (wrapper to the UI Automation Edit control)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIAEditBox : UIAControl, IText
	{
		/// <summary>
		/// Ctor to initiate a UIAEditBox wrapper to the UI automation Edit control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Edit element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAEditBox(AutomationElement element)
			: base(element)
		{

		}

		#region IText Members
		/// <summary>
		/// Retrieve text string from the control
		/// </summary>
		/// <example>
		/// This code uses windows address book application. 
		/// <code>
		///    Log.Default.Filename = @"C:\testLog.txt";
		///    System.Diagnostics.Process.Start("wab");
		///    System.Threading.Thread.Sleep(2000);
		///
		///		//if not default, dialog appears, handle it
		///		UIAWindow defaultDialog = Desktop.UIA.WaitForControl(@"Address Book",3000) as UIAWindow;
		///		UIAButton noButton =null;
		///		if (defaultDialog != null)
		///		{
		///			noButton = defaultDialog[@"No", @"Button", @"2"] as UIAButton;
		///			noButton.Click();
		///    }
		///
		///    //read write clear the search name edit box
		///   UIAWindow wabWin = Desktop.UIA[@"Address Book - Main Identity", @"WABBrowseView", @"UIAWindow"] as UIAWindow;
		///   UIAEditBox nameEB = wabWin[@"Type name or select from list:", @"Edit", @"9004"] as UIAEditBox;
		///   MessageBox.Show("Before Text: " + nameEB.Text);
		///   nameEB.Write("Barak Obama");
		///   MessageBox.Show("After Text: " + nameEB.Text);
		///   nameEB.Clear();
		///   MessageBox.Show("After Clear: " + nameEB.Text);
		/// </code>
		/// </example>
		/// <returns>string text or empty sting if no text on the control</returns>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion
		/// <summary>
		/// Clear text from the control
		/// </summary>
		/// <example>
		/// This code uses windows address book application. 
		/// <code>
		///    Log.Default.Filename = @"C:\testLog.txt";
		///    System.Diagnostics.Process.Start("wab");
		///    System.Threading.Thread.Sleep(2000);
		///
		///		//if not default, dialog appears, handle it
		///		UIAWindow defaultDialog = Desktop.UIA.WaitForControl(@"Address Book",3000) as UIAWindow;
		///		UIAButton noButton =null;
		///		if (defaultDialog != null)
		///		{
		///			noButton = defaultDialog[@"No", @"Button", @"2"] as UIAButton;
		///			noButton.Click();
		///    }
		///
		///    //read write clear the search name edit box
		///   UIAWindow wabWin = Desktop.UIA[@"Address Book - Main Identity", @"WABBrowseView", @"UIAWindow"] as UIAWindow;
		///   UIAEditBox nameEB = wabWin[@"Type name or select from list:", @"Edit", @"9004"] as UIAEditBox;
		///   MessageBox.Show("Before Text: " + nameEB.Text);
		///   nameEB.Write("Barak Obama");
		///   MessageBox.Show("After Text: " + nameEB.Text);
		///   nameEB.Clear();
		///   MessageBox.Show("After Clear: " + nameEB.Text);
		/// </code>
		/// </example>
		public void Clear()
		{
			SetFocus();
			SendKeys.SendWait("{Home}+{End}{BS}");
		}
	}

	
}
