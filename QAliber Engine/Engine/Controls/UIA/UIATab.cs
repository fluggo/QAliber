using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIATab class represents a Tab control (wrapper to the UI Automation Tab control)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIATab : UIAControl, ISelector, IScrollable
	{
		/// <summary>
		/// Ctor to initiate a UIATab wrapper to the UI automation Tab control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Tab element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIATab(AutomationElement element)
			: base(element)
		{

		}

		#region IScrollable Members
		/// <summary>
		/// Some Tabs allow Scrolling functionality. If tab doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <param name="horPercents">double % distance from left side scrolling</param>
		/// <param name="verPercents">double % distance from top scrolling</param>
		/// <see cref="M:QAliber.Engine.Controls.UIA.UIADocument.Scroll">
		/// Document scrolling:
		/// </see>
		public void Scroll(double horPercents, double verPercents)
		{
			PatternsExecutor.Scroll(this, horPercents, verPercents);
		}
		/// <summary>
		/// Some Tabs allow Scrolling functionality. If tab doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.UIADocument.CanScrollHorizontal">
		/// Document scrolling example:
		/// </see>
		[Category("Scroll")]
		[DisplayName("Can Scroll Horizontally ?")]
		public bool CanScrollHorizontal
		{
			get { return PatternsExecutor.CanHorScroll(this); }
		}
		/// <summary>
		/// Some Tabs allow Scrolling functionality. If tab doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.UIADocument.CanScrollHorizontal">
		/// Document scrolling example:
		/// </see>
		[Category("Scroll")]
		[DisplayName("Can Scroll Vertically ?")]
		public bool CanScrollVertical
		{
			get { return PatternsExecutor.CanVerScroll(this); }
		}
		/// <summary>
		/// Some Tabs allow Scrolling functionality. If tab doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.UIADocument.CanScrollHorizontal">
		/// Document scrolling example:
		/// </see>
		[Category("Scroll")]
		[DisplayName("Horizontal Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double HorizontalPercents
		{
			get { return PatternsExecutor.GetHorScrollPercents(this); }
		}
		/// <summary>
		/// Some Tabs allow Scrolling functionality. If tab doesnt support this action
		/// InvalidOperationExeption is thrown.
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.UIADocument.CanScrollHorizontal">
		/// Document scrolling example:
		/// </see>
		[Category("Scroll")]
		[DisplayName("Vertical Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double VericalPercents
		{
			get { return PatternsExecutor.GetVerScrollPercents(this); }
		}

		#endregion

		#region ISelector Members
		/// <summary>
		/// Select child UIATabItem by	its name
		/// </summary>
		/// <example>
		/// <code>
		///		//Open explorer (by defualt goes to my documents, and open folder options dialog.
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///		//Selecting tabs
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select("View");
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select(3);
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		/// 
		/// </code>
		/// </example>
		/// <param name="name">The string of the tab Name property</param>
		public void Select(string name)
		{
			PatternsExecutor.Select(this, name);
		}
		/// <summary>
		/// Select child UIATabItem by	its index
		/// </summary>
		/// <example>
		/// <code>
		///		//Open explorer (by defualt goes to my documents, and open folder options dialog.
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///		//Selecting tabs
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select("View");
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select(3);
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		/// 
		/// </code>
		/// </example>
		/// <param name="index">int as it reflect in tab Index property</param>
		public void Select(int index)
		{
			PatternsExecutor.Select(this, index);
		}
		/// <summary>
		/// Get current selected (open) tab.
		/// </summary>
		/// <example>
		/// <code>
		///		//Open explorer (by defualt goes to my documents, and open folder options dialog.
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///		//Selecting tabs
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select("View");
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		///    tabs.Select(3);
		///    MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);
		/// </code>
		/// </example>
		/// <remarks>
		/// Since only one UIATabItem can be open, you should refer to selected
		/// tabItem with index 0 like in the example above
		/// <c>(MessageBox.Show("Current tab: " + tabs.SelectedItems[0].Name);)</c>
		/// </remarks>
		[Browsable(false)]
		public UIAControl[] SelectedItems
		{
			get { return PatternsExecutor.GetSelectedItems(this); }
		}
		/// <summary>
		/// Retrieve names of all the UIATabItems in this UIATab group
		/// </summary>
		/// <example>
		/// <code>
		///		 //Open explorer (by defualt goes to my documents, and open folder options dialog.
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("explorer");
		///    UIAWindow explorerWin = Desktop.UIA [@"My Documents", @"ExploreWClass", @"UIAWindow"] as UIAWindow;
		///    UIAMenuItem @tools = explorerWin[@"", @"ReBarWindow32", @"UIAPane"][@"Application", @"ToolbarWindow32", @"MenuBar"][@"Tools", @"", @"Item 32960"] as UIAMenuItem;
		///    @tools.Click(MouseButtons.Left, new Point(23, 6));
		///    UIAMenuItem @folderOptions = Desktop.UIA[@"Menu", @"#32768", @"UIAMenu"][@"Folder Options...", @"", @"Item 41251"] as UIAMenuItem;
		///    @folderOptions.Click(MouseButtons.Left, new Point(37, 5));
		///
		///    UIATab tabs = Desktop.UIA[@"My Documents", @"ExploreWClass", @"UIAWindow"][@"Folder Options", @"#32770", @"UIAWindow"][@"", @"SysTabControl32", @"UIATab"] as UIATab;
		///    foreach(string tabsName in tabs.Items)
		///		   MessageBox.Show(tabsName);
		/// </code>
		/// </example>
		[Category("List")]
		public string[] Items
		{
			get { return PatternsExecutor.GetItems(this); }
		}
		[Browsable(false)]
		[Category("List")]
		[DisplayName("Can Select Multiple ?")]
		public bool CanSelectMultiple
		{
			get { return false; }
		}
		#endregion
		
	}
}
