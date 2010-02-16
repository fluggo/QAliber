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
	/// UIAMenuItm represents a MenuItem Gui control under windows.It is child control of
	/// UIAMenu.
	/// A wrapper to UI Automation MenuItem control.
	/// 
	/// UIAMenueItems like UIAMenu implemented as popup and you should make sure they appear on screen before using them.
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
	public class UIAMenuItem : UIAControl, IInvokable, IToggle, ISelectable, IExpandable
	{
		/// <summary>
		/// Ctor to initiate a UIAMenuItem wrapper to the UI automation MenuItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Menu element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAMenuItem(AutomationElement element)
			: base(element)
		{

		}

		#region IExpandable Members
		/// <summary>
		/// Show the menu items
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem fileMenue = notepadWin[@"Application", @"", @"MenuBar"][@"File", @"", @"Item 1"] as UIAMenuItem;
		///    fileMenue.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    fileMenue.Collapse();
		/// </code>
		/// </example>
		public void Expand()
		{
			PatternsExecutor.Expand(automationElement);
		}
		/// <summary>
		/// Close the menu.
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem fileMenue = notepadWin[@"Application", @"", @"MenuBar"][@"File", @"", @"Item 1"] as UIAMenuItem;
		///    fileMenue.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    if (fileMenue.ExpandCollapseState == System.Windows.Automation.ExpandCollapseState.Expanded)
		///			fileMenue.Collapse();
		/// </code>
		/// </example>
		public void Collapse()
		{
			PatternsExecutor.Collapse(automationElement);
		}
		/// <summary>
		/// Get the current expand \ collapse state of the menu
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem fileMenue = notepadWin[@"Application", @"", @"MenuBar"][@"File", @"", @"Item 1"] as UIAMenuItem;
		///    fileMenue.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    if (fileMenue.ExpandCollapseState == System.Windows.Automation.ExpandCollapseState.Expanded)
		///			fileMenue.Collapse();
		/// </code>
		/// </example>
		/// <remarks>
		/// ExpandCollapseState Enum:
		///  Collapsed = 0, No child nodes, controls, or content of the UI Automation element are displayed.
		///  Expanded = 1, All child nodes, controls or content of the UI Automation element are displayed.
		///  PartiallyExpanded = 2, Some, but not all, child nodes, controls, or content of the UI Automation
		///						   element are displayed.
		///  LeafNode = 3 The UI Automation element has no child nodes, controls, or content to display.
		/// </remarks>
		[Category("Expand / Collapse")]
		[DisplayName("Expand / Collapse State")]
		public ExpandCollapseState ExpandCollapseState
		{
			get { return PatternsExecutor.GetExpandCollapseState(automationElement); }
		}

		#endregion

		#region IToggle Members
		/// <summary>
		/// Check this UIAMenuItem.If item is checked dont do anything.
		/// 
		/// Some Menu items implement Check Uncheck actions, If your menu item doesnt implement this
		/// InvalidOperationException is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UICheckBox.Check">
		/// <remarks>Using Check() with checkBoxItems</remarks>
		/// </see>
		public void Check()
		{
			if (CheckState != ToggleState.On)
				Toggle();
		}
		/// <summary>
		/// Uncheck this MenuItem Control. If item is unchecked dont do anything.
		/// Some Menu items implement Check Uncheck actions. If your menu item doesnt implement this
		/// InvalidOperationException is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UICheckBox.UnCheck">
		/// <remarks>Using UnCheck() with checkBoxItems</remarks>
		/// </see>
		public void UnCheck()
		{
			if (CheckState != ToggleState.Off)
				Toggle();
		}
		/// <summary>
		/// Chenge the Check State (Check or UnCheck)
		/// 
		/// Some Menu items implement Check Uncheck actions. If your menu item doesnt implement this
		/// InvalidOperationException is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UICheckBox.Toggle">
		/// <remarks>Using Toggle() with checkBoxItems</remarks>
		/// </see>
		public void Toggle()
		{
			PatternsExecutor.Toggle(automationElement);
		}
		/// <summary>
		/// Retrieve the Check State (Check or UnCheck)
		/// 
		/// Some Menu items implement Check Uncheck actions. If your menu item doesnt implement this
		/// InvalidOperationException is thrown.
		/// </summary>
		/// <see cref="P:QAliber.Engine.Controls.UICheckBox.CheckState">
		/// <remarks>Using CheckState with checkBoxItems</remarks>
		/// </see> 
		[Category("Toggle")]
		[DisplayName("Check State")]
		public ToggleState CheckState
		{
			get { return PatternsExecutor.GetToggleState(automationElement); }
		}

		#endregion

		#region IInvokable Members
		/// <summary>
		/// Use windows invokaing to select the menu item
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem fileMenue = notepadWin[@"Application", @"", @"MenuBar"][@"File", @"", @"Item 1"] as UIAMenuItem;
		///    fileMenue.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    if (fileMenue.ExpandCollapseState == System.Windows.Automation.ExpandCollapseState.Expanded);
		///    UIAMenuItem pageItem = fileMenue[@"File", @"#32768", @"UIAMenu"][@"Page Setup...", @"", @"Item 5"] as UIAMenuItem;
		///    pageItem.Invoke();
		/// </code>
		/// </example>
		public void Invoke()
		{
			PatternsExecutor.Invoke(this);
		}

		#endregion

		#region ISelectable Members
		/// <summary>
		/// Some menuItems allow Select action. If menu doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <seealso cref="M:QAliber.Engine.Controls.UIARadioButton.Select"/>
		public void Select()
		{
			PatternsExecutor.Select(this);
		}
		/// <summary>
		/// Add item to selected radio buttons.
		/// Relevant only when multiple selected items allowed.
		/// </summary>
		public void AddToSelection()
		{
			PatternsExecutor.AddToSelection(this);
		}
		/// <summary>
		/// Remove from selected radio buttons.
		/// Relevant only when multiple selected items allowed.
		/// </summary>
		public void RemoveFromSelection()
		{
			PatternsExecutor.RemoveFromSelection(this);
		}
		/// <summary>
		/// Verify if this MenuItem is Selected in the Menu / selection group.
		/// Some menuItems allow Select action. If menu doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <seealso cref="P:QAliber.Engine.Controls.UIARadioButton.IsSelected"/>
		[Category("List")]
		[DisplayName("Is Selected ?")]
		public bool IsSelected
		{
			get { return PatternsExecutor.IsSeleced(this); }
		}

		#endregion


	  
	}
}
