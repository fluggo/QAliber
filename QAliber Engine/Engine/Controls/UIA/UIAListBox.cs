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
	/// UIAListBox class represents a list containing list items 
	/// in a user-interface under windows OS.
	/// UIAListItem GUI controll is contained withing UIAListBox
	/// </summary>
	public class UIAListBox : UIAControl, ISelector, IScrollable
	{
		/// <summary>
		/// Ctor to initiate a UIAListBox wrapper to the UI automation List control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation List element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAListBox(AutomationElement element)
			: base(element)
		{

		}

		#region ISelector Members
		/// <summary>
		/// Select a list item by its name
		/// </summary>
		/// <param name="name">Name of the item as it appears on the list</param>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox sizeList = fontWin[@"Size:", @"ComboBox", @"1138"][@"Size:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    sizeList.Select("20");
		///    fontWin[@"OK", @"Button", @"1"].Click();
		///    doc.Write("Text size is 20");  
		/// </code>
		/// </example>
		public void Select(string name)
		{
			PatternsExecutor.Select(this, name);
		}
		/// <summary>
		/// Select a list item by its name
		/// </summary>
		/// <param name="index">The index to select, first item is 0</param>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox sizeList = fontWin[@"Size:", @"ComboBox", @"1138"][@"Size:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    sizeList.Select(8);//select size 20
		///    fontWin[@"OK", @"Button", @"1"].Click();
		///    doc.Write("Text size is 20");  
		/// </code>
		/// </example>
		public void Select(int index)
		{
			PatternsExecutor.Select(this, index);
		}
		/// <summary>
		/// Get the list of selected fonts
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox fontsList = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    MessageBox.Show("Selected font: " + fontsList.SelectedItems[0].Name);
		/// </code>
		/// </example>
		/// <returns>List of UIControls, or null if did not find selected items</returns>
		[Browsable(false)]
		public UIAControl[] SelectedItems
		{
			get { return PatternsExecutor.GetSelectedItems(this); }
		}
		/// <summary>
		/// Return the string names of each item in the list
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///
		///    UIAListBox fontsStyle = fontWin[@"Font style:", @"ComboBox", @"1137"][@"Font style:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    foreach (string style in fontsStyle.Items)
		///		   MessageBox.Show(style);
		/// </code>
		/// </example>
		[Category("List")]
		public string[] Items
		{
			get { return PatternsExecutor.GetItems(this); }
		}
		/// <summary>
		/// Verify if list allow multiple items to be selected.
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
		///    UIAListBox fontsStyle = fontWin[@"Font style:", @"ComboBox", @"1137"][@"Font style:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    MessageBox.Show("Allow multiple select: " + fontsStyle.CanSelectMultiple);
		/// </code>
		/// </example>
		/// <returns>True if multiple items can be selected, false if not.</returns>
		[Category("List")]
		[DisplayName("Can Select Multiple ?")]
		public bool CanSelectMultiple
		{
			get { return PatternsExecutor.CanSelectMultiple(this); }
		}

		#endregion

		#region IScrollable Members

		public void Scroll(double horPercents, double verPercents)
		{
			PatternsExecutor.Scroll(this, horPercents, verPercents);
		}
		/// <summary>
		/// Check if horizental scroller exists
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox fontsList = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    if (fontsList.CanScrollVertical)
		///    {
		///		   MessageBox.Show("Before scroll: Vertical % : " + fontsList.VericalPercents);
		///		   fontsList.Scroll(-1, 30);
		///		   MessageBox.Show("After: Vertical % : " + fontsList.VericalPercents);
		///    }
		///    if (!fontsList.CanScrollHorizontal)
		///    {
		///		   MessageBox.Show("No horizental scroller, see the % is : " + fontsList.HorizontalPercents + " negative");
		///    }
		/// </code>
		/// </example>
		/// <returns>True if horizental scroller found, false if not</returns>
		[Category("Scroll")]
		[DisplayName("Can Scroll Horizontally ?")]
		public bool CanScrollHorizontal
		{
			get { return PatternsExecutor.CanHorScroll(this); }
		}
		/// <summary>
		/// Check if verital scroller exists
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox fontsList = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    if (fontsList.CanScrollVertical)
		///    {
		///		   MessageBox.Show("Before scroll: Vertical % : " + fontsList.VericalPercents);
		///		   fontsList.Scroll(-1, 30);
		///		   MessageBox.Show("After: Vertical % : " + fontsList.VericalPercents);
		///    }
		///    if (!fontsList.CanScrollHorizontal)
		///    {
		///		   MessageBox.Show("No horizental scroller, see the % is : " + fontsList.HorizontalPercents + " negative");
		///    }
		/// </code>
		/// </example>
		/// <returns>True if vertical scroller found, false if not</returns>
		[Category("Scroll")]
		[DisplayName("Can Scroll Vertically ?")]
		public bool CanScrollVertical
		{
			get { return PatternsExecutor.CanVerScroll(this); }
		}

		/// <summary>
		/// Check if horizental scroller exists
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox fontsList = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    if (fontsList.CanScrollVertical)
		///    {
		///		   MessageBox.Show("Before scroll: Vertical % : " + fontsList.VericalPercents);
		///		   fontsList.Scroll(-1, 30);
		///		   MessageBox.Show("After: Vertical % : " + fontsList.VericalPercents);
		///    }
		///    if (!fontsList.CanScrollHorizontal)
		///    {
		///		   MessageBox.Show("No horizental scroller, see the % is : " + fontsList.HorizontalPercents + " negative");
		///    }
		/// </code>
		/// </example>
		/// <returns>The % distance from left corner in double, or -1 if scroller not found</returns>
		[Category("Scroll")]
		[DisplayName("Horizontal Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double HorizontalPercents
		{
			get { return PatternsExecutor.GetHorScrollPercents(this); }
		}

		/// <summary>
		/// Check if horizental scroller exists
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIDocument doc = notepadWin[@"", @"Edit", @"15"] as UIDocument;
		///    doc.Write("{LeftAlt}of");
		///    
		///    UIAWindow fontWin = notepadWin[@"Font", @"#32770", @"UIAWindow"] as UIAWindow;
		///    UIAListBox fontsList = fontWin[@"Font:", @"ComboBox", @"1136"][@"Font:", @"ComboLBox", @"ListBox"] as UIAListBox;
		///    if (fontsList.CanScrollVertical)
		///    {
		///		   MessageBox.Show("Before scroll: Vertical % : " + fontsList.VericalPercents);
		///		   fontsList.Scroll(-1, 30);
		///		   MessageBox.Show("After: Vertical % : " + fontsList.VericalPercents);
		///    }
		///    if (!fontsList.CanScrollHorizontal)
		///    {
		///		   MessageBox.Show("No horizental scroller, see the % is : " + fontsList.HorizontalPercents + " negative");
		///    }
		/// </code>
		/// </example>
		/// <returns>The % distance from upper corner in double, or -1 if scroller not found</returns>
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
