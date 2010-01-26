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
	/// UIATree class represents a Tree control (wrapper to the UI Automation Tree control)
	/// in a user-interface under windows OS.
	/// Tree control contains UIATreeItem.
	/// </summary>
	public class UIATree : UIAControl, IScrollable, ISelector
	{
		/// <summary>
		/// Ctor to initiate a UIATree wrapper to the UI automation Tree control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Tree element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIATree(AutomationElement element)
			: base(element)
		{

		}

		#region ISelector Members
		/// <summary>
		/// Select a UIATreeItem by its Name
		/// </summary>
		/// <param name="name">Name of the tree item as it reflect in Name property</param>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    solutionTree.Select("References");
		///    solutionTree.Select(2);
		///    for (int idx = 0 ; idx &lt; solutionTree.SelectedItems.Length ; idx++)
		///		   MessageBox.Show(solutionTree.SelectedItems[idx].Name);
		/// </code>
		/// </example>
		public void Select(string name)
		{
			PatternsExecutor.Select(this, name);
		}
		/// <summary>
		/// Select a UIATreeItem by index
		/// </summary>
		/// <param name="index">Name of the tree item as it reflect in Name property</param>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    solutionTree.Select("References");
		///    solutionTree.Select(2);
		///    for (int idx = 0 ; idx &lt; solutionTree.SelectedItems.Length ; idx++)
		///		   MessageBox.Show(solutionTree.SelectedItems[idx].Name);
		/// </code>
		/// </example>
		public void Select(int index)
		{
			PatternsExecutor.Select(this, index);
		}
		/// <summary>
		/// Retrieve list of UIControl (UIATreeItem) selected in the current treeView
		/// </summary>
		/// <example>
		/// <code>
		///   UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    solutionTree.Select("References");
		///    solutionTree.Select(2);
		///    for (int idx = 0 ; idx &lt; solutionTree.SelectedItems.Length ; idx++)
		///		   MessageBox.Show(solutionTree.SelectedItems[idx].Name);
		/// </code>
		/// </example>
		[Browsable(false)]
		public UIAControl[] SelectedItems
		{
			get { return PatternsExecutor.GetSelectedItems(this); }
		}
		/// <summary>
		/// Retrive list of Name strings for all UIATreeItems control in this tree
		/// </summary>
		/// <example>
		/// <code>
		///  UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///  UIATree solutionTree = vsWin[@"Solution Explorer", true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///  string[] allTreeItems = solutionTree.Items;
		/// </code>
		/// </example>
		[Category("List")]
		public string[] Items
		{
			get { return PatternsExecutor.GetItems(this); }
		}
		/// <summary>
		/// Verify if multiple selection is allowed
		/// </summary>
		[Category("List")]
		[DisplayName("Can Select Multiple ?")]
		public bool CanSelectMultiple
		{
			get { return PatternsExecutor.CanSelectMultiple(this); }
		}

		#endregion

		#region IScrollable Members
		/// <summary>
		/// Scroll vertical and horizental scrollers in % from left and top.
		/// </summary>
		/// <param name="horPercents">% from the top</param>
		/// <param name="verPercents">% from left</param>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    if (solutionTree.CanScrollVertical)
		///		   solutionTree.Scroll(-1, 40);
		///    if (solutionTree.CanScrollHorizontal)
		///		   solutionTree.Scroll(20,-1);
		///    double curVertPos = solutionTree.VericalPercents;
		///    double curHorPos = solutionTree.HorizontalPercents;
		/// </code>
		/// </example>
		public void Scroll(double horPercents, double verPercents)
		{
			PatternsExecutor.Scroll(this, horPercents, verPercents);
		}
		/// <summary>
		/// Verify if horizental scroller visible and can be scrolled.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    if (solutionTree.CanScrollVertical)
		///		   solutionTree.Scroll(-1, 40);
		///    if (solutionTree.CanScrollHorizontal)
		///		   solutionTree.Scroll(20,-1);
		///    double curVertPos = solutionTree.VericalPercents;
		///    double curHorPos = solutionTree.HorizontalPercents;
		/// </code>
		/// </example>
		/// <returns>true is scroller found</returns>
		[Category("Scroll")]
		[DisplayName("Can Scroll Horizontally ?")]
		public bool CanScrollHorizontal
		{
			get { return PatternsExecutor.CanHorScroll(this); }
		}
		/// <summary>
		/// Verify if vertical scroller visible and can be scrolled.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    if (solutionTree.CanScrollVertical)
		///		   solutionTree.Scroll(-1, 40);
		///    if (solutionTree.CanScrollHorizontal)
		///		   solutionTree.Scroll(20,-1);
		///    double curVertPos = solutionTree.VericalPercents;
		///    double curHorPos = solutionTree.HorizontalPercents;
		/// </code>
		/// </example>
		/// <returns>true is scroller found</returns>
		[Category("Scroll")]
		[DisplayName("Can Scroll Vertically ?")]
		public bool CanScrollVertical
		{
			get { return PatternsExecutor.CanVerScroll(this); }
		}
		/// <summary>
		/// Retrive the scroller position in % from the left side
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    if (solutionTree.CanScrollVertical)
		///		   solutionTree.Scroll(-1, 40);
		///    if (solutionTree.CanScrollHorizontal)
		///		   solutionTree.Scroll(20,-1);
		///    double curVertPos = solutionTree.VericalPercents;
		///    double curHorPos = solutionTree.HorizontalPercents;
		/// </code>
		/// </example>
		/// <returns>double % distance from left position</returns>
		[Category("Scroll")]
		[DisplayName("Horizontal Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double HorizontalPercents
		{
			get { return PatternsExecutor.GetHorScrollPercents(this); }
		}
		/// <summary>
		/// Retrive the scroller position in % from the top
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow vsWin = Desktop.UIA["Visual Studio", true] as UIAWindow;
		///    UIATree solutionTree = vsWin[@"Solution Explorer",true][@"", @"VsUIHierarchyBaseWin", @"UIAPane"][@"Solution Explorer", @"SysTreeView32", @"UIATree"] as UIATree;
		///    if (solutionTree.CanScrollVertical)
		///		   solutionTree.Scroll(-1, 40);
		///    if (solutionTree.CanScrollHorizontal)
		///		   solutionTree.Scroll(20,-1);
		///    double curVertPos = solutionTree.VericalPercents;
		///    double curHorPos = solutionTree.HorizontalPercents;
		/// </code>
		/// </example>
		/// <returns>double % distance from top position</returns>
		[Category("Scroll")]
		[DisplayName("Vertical Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double VericalPercents
		{
			get { return PatternsExecutor.GetVerScrollPercents(this); }
		}

		#endregion

		
		
	}

	
}
