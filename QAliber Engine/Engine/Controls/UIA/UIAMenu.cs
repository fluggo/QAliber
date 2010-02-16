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
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIAMenu represents a Menu and MenuBar Gui control under windows.
	/// A wrapper to UI Automation Menu control.
	/// Menues usually implemented as pop ups and you should make sure they appear on screen before using them.
	/// </summary>
	/// <example>
	/// <code>
	///    Process.Start("calc");
	///    UIAMenu MenuBar = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"][@"Application", @"", @"MenuBar"] as UIAMenu;
	///
	///    UIAMenuItem viewButton = MenuBar["View"] as UIAMenuItem;
	///    //Open menu
	///    viewButton.Click();
	///    //Look for the new menu popup appeared
	///    UIAMenu viewMenu = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"][@"Application", @"", @"MenuBar"][@"View", @"", @"Item 2"][@"View", @"#32768", @"UIAMenu"] as UIAMenu;
	///    UIAMenuItem standardOption = viewMenu[@"Standard", @"", @"Item 305"] as UIAMenuItem;
	///    standardOption.Click(MouseButtons.Left,new Point(10,10));
	/// </code>
	/// </example>
	public class UIAMenu : UIAControl, ISelector
	{
		/// <summary>
		/// Ctor to initiate a UIAMenu wrapper to the UI automation Menu and MenuBar control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Menu element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAMenu(AutomationElement element)
			: base(element)
		{

		}

		#region ISelector Members
		/// <summary>
		/// Some menu allow Select by UIAMenuItem name action. If menu doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <param name="name">Name of UIAMenuItem to select</param>
		/// <seealso cref="M:QAliber.Engine.Controls.UIARadioButton.Select"/>
		public void Select(string name)
		{
			PatternsExecutor.Select(this, name);
		}
		/// <summary>
		/// Some menu allow Select by child index action. If menu doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <param name="index">Index of UIAMenuItem to select, in the current menu</param>
		/// <seealso cref="M:QAliber.Engine.Controls.UIARadioButton.Select"/>
		public void Select(int index)
		{
			PatternsExecutor.Select(this, index);
		}

		[Browsable(false)]
		public UIAControl[] SelectedItems
		{
			get { return PatternsExecutor.GetSelectedItems(this); }
		}
		/// <summary>
		/// Strings list of all menu items
		/// </summary>
		/// <example>
		/// <code>
		///  System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenu menu = notepadWin[@"Application", @"", @"MenuBar"] as UIAMenu;
		///    //Go through all the menue items
		///    if (!menu.CanSelectMultiple)
		///		   foreach (string title in menu.Items)
		///			   MessageBox.Show(title);
		/// </code>
		/// </example>
		[Category("List")]
		public string[] Items
		{
			get { return PatternsExecutor.GetItems(this); }
		}
		/// <summary>
		/// Strings list of all menu items
		/// </summary>
		/// <example>
		/// <code>
		///  System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenu menu = notepadWin[@"Application", @"", @"MenuBar"] as UIAMenu;
		///    //Go through all the menue items
		///    if (!menu.CanSelectMultiple)
		///		   foreach (string title in menu.Items)
		///			   MessageBox.Show(title);
		/// </code>
		/// </example>
		[Category("List")]
		[DisplayName("Can Select Multiple ?")]
		public bool CanSelectMultiple
		{
			get { return PatternsExecutor.CanSelectMultiple(this); }
		}

		#endregion

		
	}
}
