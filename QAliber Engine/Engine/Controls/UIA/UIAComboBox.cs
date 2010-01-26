using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UICombobox class represents a combo (expanding select list control)
	/// in a user-interface under windows OS.
	/// UIComboBox contains UIAListItem controls.
	/// </summary>
	public class UIAComboBox : UIAControl, IExpandable, ISelector, IText
	{
		/// <summary>
		/// Ctor to initiate a UIComboBox wrapper to the UI automation checkbox control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation button element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAComboBox(AutomationElement element)
			: base(element)
		{

		}

		#region IExpandable Members
		/// <summary>
		/// Open the combobox
		/// </summary>
		/// <example>
		/// The example will luanch IE (wrote for IE7, assuming tabs), expand the combo and using keybord will select the 3rd item.
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIComboBox"] as UIComboBox;
		///    addressCombo.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    IEWin.Write("{DOWN}{DOWN}{DOWN}{ENTER}");
		/// </code>
		/// </example>
		public void Expand()
		{
			PatternsExecutor.Expand(automationElement);
		}
		/// <summary>
		/// Close the combobox
		/// </summary>
		/// <example>
		/// The example will luanch IE (wrote for IE7, assuming tabs), expand the combo and using keybord will go down and up the list before collapsing the combobox.
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    addressCombo.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    IEWin.Write("{DOWN}{DOWN}{DOWN}");
		///    System.Threading.Thread.Sleep(1000);
		///    IEWin.Write("{UP}");
		///    addressCombo.Collapse();
		/// </code>
		/// </example>
		public void Collapse()
		{
			PatternsExecutor.Collapse(automationElement);
		}
		/// <summary>
		/// Check if the combobox is open or close
		/// </summary>
		/// <example>
		/// The example will luanch IE (wrote for IE7, assuming tabs), expand the combo and using keybord will go doan and up the list
		///  verify the state before collapsing the combobox.
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    addressCombo.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    IEWin.Write("{DOWN}{DOWN}{DOWN}");
		///    System.Threading.Thread.Sleep(1000);
		///    IEWin.Write("{UP}");
		///    if (addressCombo.ExpandCollapseState == System.Windows.Automation.ExpandCollapseState.Expanded)
		///		addressCombo.Collapse();
		/// </code>
		/// </example>
		[Category("Expand / Collapse")]
		[DisplayName("Expand / Collapse State")]
		public ExpandCollapseState ExpandCollapseState
		{
			get { return PatternsExecutor.GetExpandCollapseState(automationElement); }
		}

		#endregion

		#region IText Members
		/// <summary>
		/// Read the text from the combobox, text of selected item
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    addressCombo.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    IEWin.Write("{DOWN}{DOWN}{DOWN}");
		///    System.Threading.Thread.Sleep(1000);
		///    addressCombo.Collapse();
		///    MessageBox.Show(addressCombo.Text);
		/// </code>
		/// </example>
		/// <returns>The text of the combo selected item as it appear in the combobox when its closes, or null if no text was found</returns>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion

		#region ISelector Members
		/// <summary>
		/// Select an item from the combobox by name
		/// </summary>
		/// <param name="name">String of the item to look for</param>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UAIComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    //if its not one of your last visited sites, change the string
		///    addressCombo.Select("http://www.google.com/");
		/// </code>
		/// </example>
		public void Select(string name)
		{
			PatternsExecutor.Select(this, name);
		}
		/// <summary>
		/// Select an item from the combobox by index.
		/// </summary>
		/// <param name="index">The item inex, 0 is first</param>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    //Select the 5th item
		///    addressCombo.Select(4);
		/// </code>
		/// </example>
		public void Select(int index)
		{
			PatternsExecutor.Select(this, index);
		}
		/// <summary>
		/// Retrieve the list of selected items in the combobox
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(3000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    UIControl[] selectItems;
		///    addressCombo.Expand();
		///    System.Threading.Thread.Sleep(3000);
		///    IEWin.Write("{DOWN}{DOWN}");
		///    selectItems = addressCombo.SelectedItems;
		///    MessageBox.Show(selectItems[0].Name);
		/// </code>
		/// </example>
		/// <remarks>Use CanSelectMultiple to verify the combo allow multiple selection </remarks>
		[Browsable(false)]
		public UIAControl[] SelectedItems
		{
			get { return PatternsExecutor.GetSelectedItems(this); }
		}
		/// <summary>
		/// Retrieve list of all strings (items name) in the combobox
		/// </summary>
		/// <example>
		/// This example open IE7 browser, and goes through all items in the address combobox
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(5000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    addressCombo.Expand();
		///    foreach(string item in addressCombo.Items)
		///    MessageBox.Show(item);
		/// </code>
		/// </example>
		[Category("List")]
		public string[] Items
		{
			get { return PatternsExecutor.GetItems(this); }
		}
		/// <summary>
		/// Does the combo allow slecting multiple items
		/// </summary>
		/// <example>
		/// This shows the address bar on IE (combobox) doesnt support multiple selection
		/// <code>
		///    System.Diagnostics.Process.Start("IExplore");
		///    System.Threading.Thread.Sleep(5000);
		///    UIAPane IEWin = Desktop.UIA[@"Windows Internet Explorer",true][@"", @"ReBarWindow32", @"UIAPane"][@"", @"Address Band Root", @"UIAPane"] as UIAPane;
		///    UIAComboBox addressCombo = IEWin[@"Address", @"ComboBox", @"UIAComboBox"] as UIAComboBox;
		///    MessageBox.Show("Can the address bar on IE have multiple select? " + addressCombo.CanSelectMultiple );
		/// </code>
		/// </example>
		/// <returns>True if allow multiple selection</returns>
		[Category("List")]
		[DisplayName("Can Select Multiple ?")]
		public bool CanSelectMultiple
		{
			get { return PatternsExecutor.CanSelectMultiple(this); }
		}

		#endregion

		/// <summary>
		/// Same as Expand
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.Expand">Expand</see>
		public void OpenDropDown()
		{
			Expand();
		}

		/// <summary>
		/// Same as Collapse
		/// </summary>
		/// <see cref="M:QAliber.Engine.Controls.UIA.Expand">Collapse</see>
		public void CloseDropDown()
		{
			Collapse();
		}
	}
}
