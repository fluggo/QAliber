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
	/// UIATreeItem class represents a TreeItem control (wrapper to the UI Automation Tree control)
	/// in a user-interface under windows OS.
	/// UIATreeItem is a child of UIATree or another UIATreeItem.
	/// </summary>
	public class UIATreeItem : UIAControl, IExpandable, IInvokable, ISelectable
	{
		/// <summary>
		/// Ctor to initiate a UIATreeItem wrapper to the UI automation TreeItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation TreeItem element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIATreeItem(AutomationElement element)
			: base(element)
		{

		}

		#region IExpandable Members
		/// <summary>
		/// Open to show the tree items under this UIATreeItem
		/// </summary>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem solTreeItem = solutionTree["Solution", true] as UIATreeItem;
		///    if(solTreeItem.ExpandCollapseState = System.Windows.Automation.ExpandCollapseState.Collapsed)
		///			solTreeItem.Expand();
		/// </code>
		/// </example>
		public void Expand()
		{
			PatternsExecutor.Expand(automationElement);
		}
		/// <summary>
		/// Close the tree items under this UIATreeItem
		/// </summary>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem solTreeItem = solutionTree["Solution", true] as UIATreeItem;
		///		if (solTreeItem.ExpandCollapseState = System.Windows.Automation.ExpandCollapseState.Expanded)
		///			solTreeItem.Collapse();
		/// </code>
		/// </example>
		public void Collapse()
		{
			PatternsExecutor.Collapse(automationElement);
		}
		/// <summary>
		/// Retrieve the state of the control, if expanded or collapsed
		/// </summary>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem solTreeItem = solutionTree["Solution", true] as UIATreeItem;
		///		if (solTreeItem.ExpandCollapseState = System.Windows.Automation.ExpandCollapseState.Expanded)
		///			solTreeItem.Collapse();
		/// </code>
		/// </example>
		[Category("Expand / Collapse")]
		[DisplayName("Expand / Collapse State")]
		public ExpandCollapseState ExpandCollapseState
		{
			get { return PatternsExecutor.GetExpandCollapseState(automationElement); }
		}

		#endregion

		#region IInvokable Members
		/// <summary>
		/// Use windows invoking to act on this controll
		/// </summary>
		public void Invoke()
		{
			PatternsExecutor.Invoke(this);
		}

		#endregion

		#region ISelectable Members
		/// <summary>
		/// Select the current UIATreeItem
		/// </summary>
		/// <example>
		/// <code>
		///  UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem refTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants,FindProperties.Name,"References") as UIATreeItem;
		///    refTreeItem.Select();
		///    UIATreeItem propTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants, FindProperties.Name, "Properties") as UIATreeItem;
		///    propTreeItem.AddToSelection();
		///    refTreeItem.RemoveFromSelection();
		/// </code>
		/// </example>
		public void Select()
		{
			PatternsExecutor.Select(this);
		}
		/// <summary>
		/// Add the current tree item to the selected items.
		/// </summary>
		/// <example>
		/// <code>
		///  UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem refTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants,FindProperties.Name,"References") as UIATreeItem;
		///    refTreeItem.Select();
		///    UIATreeItem propTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants, FindProperties.Name, "Properties") as UIATreeItem;
		///    propTreeItem.AddToSelection();
		///    refTreeItem.RemoveFromSelection();
		/// </code>
		/// </example>
		public void AddToSelection()
		{
			PatternsExecutor.AddToSelection(this);
		}
		/// <summary>
		/// Remove the current tree item from the selection
		/// </summary>
		/// <example>
		/// <code>
		///  UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem refTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants,FindProperties.Name,"References") as UIATreeItem;
		///    refTreeItem.Select();
		///    UIATreeItem propTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants, FindProperties.Name, "Properties") as UIATreeItem;
		///    propTreeItem.AddToSelection();
		///    refTreeItem.RemoveFromSelection();
		/// </code>
		/// </example>
		public void RemoveFromSelection()
		{
			PatternsExecutor.RemoveFromSelection(this);
		}
		/// <summary>
		/// Verify if the current tree item is selected.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    UIATreeItem refTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants,FindProperties.Name,"References") as UIATreeItem;
		///    refTreeItem.Select();
		///    UIATreeItem propTreeItem = solutionTree.Find(System.Windows.Automation.TreeScope.Descendants, FindProperties.Name, "Properties") as UIATreeItem;
		///    propTreeItem.AddToSelection();
		///    refTreeItem.RemoveFromSelection();
		///    bool res = refTreeItem.IsSelected;
		/// </code>
		/// </example>
		/// <returns> true is tree item is selected, false if not</returns>
		[Category("List")]
		[DisplayName("Is Selected ?")]
		public bool IsSelected
		{
			get { return PatternsExecutor.IsSeleced(this); }
		}

		#endregion
	}

	
}
