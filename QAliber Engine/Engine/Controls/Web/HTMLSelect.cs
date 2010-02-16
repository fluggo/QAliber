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
using mshtml;
using System.ComponentModel;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents The select HTML tag.
	/// Select is actually a select list (drop-down list) and contains HTMLOption
	/// inside the select element.
	/// </summary>
	/// <seealso cref="T:QAliber.Engine.Controls.Web.HTMLOption"/>
	public class HTMLSelect : WebControl
	{
		public HTMLSelect(IHTMLElement element) : base(element)
		{
			
	   
		}
		/// <summary>
		/// Select an HTMLOption control in the list.
		/// </summary>
		/// <param name="text">string of the Text propery in the requested HTMLOption</param>
		/// <example>
		/// <code>
		///    //On google preferences page:
		///    HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///    intfaceLang.SelectItem("Greek");
		/// </code>
		/// </example>
		/// <returns>HTMLOption control</returns>
		public HTMLOption SelectItem(string text)
		{
			HTMLOption[] itemsArr = Options;
			for (int idx = 0; idx < itemsArr.Length; idx++)
			{
				if (itemsArr[idx].Text == text)
				{
					SelectedIndex = idx;
					return itemsArr[idx];
				}
			}
			return null;
		}

		#region props
		/// <summary>
		/// Get or Set the selected Index, Set is acting like SelectItem -> sets the HTMLOption in the index as selected.
		/// </summary>
		/// <example>
		/// <code>
		///    //On google preferences page:
		///    HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///    intfaceLang.SelectItem("Greek");
		///    int selectedIdx = intfaceLang.SelectedIndex;
		///    intfaceLang.SelectedIndex = 2;//select albanian
		/// </code>
		/// </example>
		[Category("HTMLSelect")]
		public int SelectedIndex
		{
			get { return ((IHTMLSelectElement)htmlElement).selectedIndex; }
			set { ((IHTMLSelectElement)htmlElement).selectedIndex = value; }
		}
		/// <summary>
		/// Retrieve the number of HTMLOptionelements in the list
		/// </summary>
		/// <example>
		/// <code>
		///  //On google preferences page:
		///    HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///    for (int idx = 0; idx &lt; intfaceLang.NumberOfItems; idx++)
		///    {
		///		   if (intfaceLang.Options[idx].Text == "Greek")
		///			   intfaceLang.SelectedIndex = idx;
		///    }
		/// </code>
		/// </example>
		[Category("HTMLSelect")]
		public int NumberOfItems
		{
			get {return ((IHTMLSelectElement)htmlElement).length; }
		}
		/// <summary>
		/// Current selected item value. This is not the HTMLOption Text but an inside list value to indicate the selected item.
		/// </summary>
		[Category("HTMLSelect")]
		public string Value
		{
			get { return ((IHTMLSelectElement)htmlElement).value; }
			set { ((IHTMLSelectElement)htmlElement).value = Value; }
		}
		/// <summary>
		/// Get all the HTMLOptions inside this list.
		/// </summary>
		/// <example>
		/// <code>
		///  //On google preferences page:
		///    HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///    for (int idx = 0; idx &lt; intfaceLang.NumberOfItems; idx++)
		///    {
		///		   if (intfaceLang.Options[idx].Text == "Greek")
		///			   intfaceLang.SelectedIndex = idx;
		///    } 
		/// </code>
		/// </example>
		[Category("HTMLSelect")]
		[ DisplayName("Options")]
		public HTMLOption [] Options
		{
		 
			get
			{
				HTMLOption[] itemsArr = new HTMLOption[NumberOfItems];
				for (int idx = 0 ; idx < itemsArr .Length ;idx++)
					itemsArr[idx] = new HTMLOption( (IHTMLElement) ((IHTMLSelectElement)htmlElement).item(idx,idx) ) ;

				return itemsArr;
			}
		}

		/// <summary>
		/// Verify the Enabled/Disabled condition of the control.
		/// </summary>
		/// <example>
		/// <code>
		///  //On google preferences page:
		///    HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///    if (intfaceLang.Enabled)
		///		   intfaceLang.Click();
		/// </code>
		/// </example>
		[Category("HTMLSelect")]
		public override bool Enabled
		{
			get
			{
				bool res = ((IHTMLSelectElement)htmlElement).disabled;

				if (res)
					return false;

				return true;
			}
		}
		#endregion
	}
}
