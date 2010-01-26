using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIAListItem class represents an item in UIAListBox or UIComboBox
	/// in a user-interface under windows OS. It is a wrapper to the UI Automation ListItem control.
	/// </summary>
	public class UIAListItem : UIAControl, IInvokable, IExpandable, ISelectable, IToggle, IText
	{
		/// <summary>
		/// Ctor to initiate a UIAListBox wrapper to the UI automation ListItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation ListItem element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAListItem(AutomationElement element)
			: base(element)
		{

		}

		#region IText Members
		/// <summary>
		/// Retrieve the text string on the list item.
		/// </summary>
		/// <example>
		/// <code>
		///  UIAListBox shortcutFolder = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    shortcutFolder.Select(0);
		///    UIAListItem sc = (UIAListItem)shortcutFolder.SelectedItems[0];
		///    MessageBox.Show("Selected shortcut is: " + sc.Text);
		/// </code>
		/// </example>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion

		#region IExpandable Members
		/// <summary>
		/// Expand if list item has sub selections
		/// </summary>
		/// <seealso cref="M:QAliber.Engine.Controls.UIA.UIAComboBox.Expand">
		///  See example for expand action
		/// </seealso>
		public void Expand()
		{
			PatternsExecutor.Expand(automationElement);
		}
		/// <summary>
		/// Collapse up to the list item.
		/// </summary>
		/// <seealso cref="M:QAliber.Engine.Controls.UIA.UIAComboBox.Collapse">
		///  See example for Collapse action
		/// </seealso>
		public void Collapse()
		{
			PatternsExecutor.Collapse(automationElement);
		}
		/// <summary>
		/// Return the collapse / Expand status
		/// </summary>
		/// <returns>
		/// Collapsed = 0,	No child nodes, controls, or content of the UI Automation element are displayed.
		/// Expanded = 1, All child nodes, controls or content of the UI Automation element are displayed.
		/// PartiallyExpanded = 2, Some, but not all, child nodes, controls, or content of the UI Automation
		///						 element are displayed.
		/// LeafNode = 3,The UI Automation element has no child nodes, controls, or content to display.
		/// </returns>
		/// <seealso cref="P:QAliber.Engine.Controls.UIA.UIAComboBox.ExpandCollapseState">
		///  See example for ExpandCollapseState property
		/// </seealso>
		[Category("Expand / Collapse")]
		[DisplayName("Expand / Collapse State")]
		public ExpandCollapseState ExpandCollapseState
		{
			get { return PatternsExecutor.GetExpandCollapseState(automationElement); }
		}

		#endregion

		#region IToggle Members
		/// <summary>
		/// Check (without click) the checkbox control
		/// </summary>
		/// <seealso cref="P:QAliber.Engine.Controls.UIA.UIACheckBox.Check">
		///  See example for  Check() action
		/// </seealso>
		/// <remarks>Note the check action is done without click (you can verify check state and click)
		/// so click events wont be fired only onCheck events.
		/// </remarks>
		public void Check()
		{
			if (CheckState != ToggleState.On)
				Toggle();
		}
		/// <summary>
		/// UnCheck (without click) the checkbox control
		/// </summary>
		/// <seealso cref="P:QAliber.Engine.Controls.UIA.UIACheckBox.UnCheck">
		///  See example for UnCheck() action
		/// </seealso>
		/// <remarks>Note the check action is done without click (you can verify check state and click)
		/// so click events wont be fired only onCheck events.
		/// </remarks>
		public void UnCheck()
		{
			if (CheckState != ToggleState.Off)
				Toggle();
		}

		public void Toggle()
		{
			PatternsExecutor.Toggle(automationElement);
		}
		/// <summary>
		/// Verify the check state of the checkbox.
		/// </summary>
		/// <seealso cref="M:QAliber.Engine.Controls.UIA.UIACheckBox.CheckState">
		///  See example for CheckState property
		/// </seealso>
		/// <remarks>note the check state is retrieved by using the windows UI Automation</remarks>
		[Category("Toggle")]
		[DisplayName("Check State")]
		public ToggleState CheckState
		{
			get { return PatternsExecutor.GetToggleState(automationElement); }
		}

		#endregion

		#region IInvokable Members
		/// <summary>
		/// Use windows invoking, to select the item.
		/// </summary>
		/// <example>
		/// <code>
		/// UIAListBox shortcutFolder = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    shortcutFolder.Select(0);
		///    UIAListItem sc = (UIAListItem)shortcutFolder.SelectedItems[0];
		///    //The invoke is selecting the shortcut (luanch the first short cut on dektop
		///    sc.Invoke();
		/// </code>
		/// </example>
		public void Invoke()
		{
			PatternsExecutor.Invoke(this);
		}

		#endregion

		#region ISelectable Members
		/// <summary>
		/// Select the item in the list as single selection
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///
		///    UIAListBox fontsStyle = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    UIAListItem selectedItem = (UIAListItem) fontsStyle.SelectedItems[0];
		///
		///    MessageBox.Show("Selected item: " + selectedItem.Name + ". Click OK to Change selection...");
		///    fontsStyle.Select(4);
		///    MessageBox.Show("Click OK to change selection back to: " + selectedItem.Name);
		///    selectedItem.Select();
		/// </code>
		/// </example>
		public void Select()
		{
			PatternsExecutor.Select(this);
		}
		/// <summary>
		/// Add the item to list of selected items.
		/// </summary>
		/// <example>
		/// <code>
		///		UIAListBox shortcutFolder = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    shortcutFolder.Select(0);
		///    UIAListItem sc = (UIAListItem)shortcutFolder.SelectedItems[0];
		///    MessageBox.Show(sc.Name + " was selected, save reference, and select the next item.");
		///    shortcutFolder.Select(1);
		///   
		///    MessageBox.Show("Click OK to add " + sc.Name + " to selection: " );
		///    sc.AddToSelection();
		///    MessageBox.Show("Click OK to remove from selection: " + sc.Name);
		///    sc.RemoveFromSelection();
		///    MessageBox.Show("Click OK to select only " + sc.Name);
		///    sc.Select();
		/// </code>
		/// </example>
		public void AddToSelection()
		{
			PatternsExecutor.AddToSelection(this);
		}
		/// <summary>
		/// Remove the item from list of selected items.
		/// </summary>
		/// <example>
		/// <code>
		///		UIAListBox shortcutFolder = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    shortcutFolder.Select(0);
		///    UIAListItem sc = (UIAListItem)shortcutFolder.SelectedItems[0];
		///    MessageBox.Show(sc.Name + " was selected, save reference, and select the next item.");
		///    shortcutFolder.Select(1);
		///   
		///    MessageBox.Show("Click OK to add " + sc.Name + " to selection: " );
		///    sc.AddToSelection();
		///    MessageBox.Show("Click OK to remove from selection: " + sc.Name);
		///    sc.RemoveFromSelection();
		///    MessageBox.Show("Click OK to select only " + sc.Name);
		///    sc.Select();
		/// </code>
		/// </example>
		public void RemoveFromSelection()
		{
			PatternsExecutor.RemoveFromSelection(this);
		}

		[Category("List")]
		[DisplayName("Is Selected ?")]
		public bool IsSelected
		{
			get { return PatternsExecutor.IsSeleced(this); }
		}

		#endregion
	}
}
