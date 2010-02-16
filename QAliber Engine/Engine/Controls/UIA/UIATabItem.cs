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
	/// UIATab class represents a TabItem control (wrapper to the UI Automation TabItem control)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIATabItem : UIAControl, ISelectable
	{
		/// <summary>
		/// Ctor to initiate a UIATabItem wrapper to the UI automation TabItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation TabItem element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIATabItem(AutomationElement element)
			: base(element)
		{

		}

		#region ISelectable Members
		/// <summary>
		/// Select the current UIATabItem
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    UIATabItem viewTab = tabs["View"] as UIATabItem;
		///    if (!viewTab.IsSelected)
		///		   viewTab.Select();
		/// </code>
		/// </example>
		public void Select()
		{
			PatternsExecutor.Select(this);
		}

		/// <summary>
		/// Not in use for Tabs
		/// </summary>
		public void AddToSelection()
		{
			PatternsExecutor.AddToSelection(this);
		}
		/// <summary>
		/// Not in use for Tabs
		/// </summary>
		public void RemoveFromSelection()
		{
			PatternsExecutor.RemoveFromSelection(this);
		}
		/// <summary>
		/// Verify if the current UIATabItem is selected
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    UIATabItem viewTab = tabs["View"] as UIATabItem;
		///    if (!viewTab.IsSelected)
		///		   viewTab.Select();
		/// </code>
		/// </example>
		/// <returns>true if the current tab is focused, false if not.</returns>
		[Category("List")]
		[DisplayName("Is Selected ?")]
		public bool IsSelected
		{
			get { return PatternsExecutor.IsSeleced(this); }
		}

		#endregion
	}
}
