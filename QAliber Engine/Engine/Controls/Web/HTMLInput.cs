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
	/// Represents HTML INPUT element.
	/// An input field can vary , depending on the type attribute (text,button,etc').
	/// </summary>
	public class HTMLInput : WebControl
	{
		public HTMLInput(IHTMLElement element)
			: base(element)
		{

		}

		#region props
		/// <summary>
		/// Get or Set (text,password inputs) value in the control
		/// </summary>
		/// <example>
		/// <code>
		///    //On google page
		///    HTMLInput searchBox = Browser.This.CurrentPage.FindByName("INPUT", "q") as HTMLInput;
		///    searchBox.Value = "Search me";
		///    string searchText = searchBox.Value;
		/// </code>
		/// </example>
		[Category("HTMLInput")]
		public string Value
		{
			get { return ((IHTMLInputElement) htmlElement).value; }
			set 
			{ 
				if (((IHTMLInputElement)htmlElement).type == "text" ||
					((IHTMLInputElement)htmlElement).type == "password")
					((IHTMLInputElement)htmlElement).value = value;		
			}
		}
		/// <summary>
		/// Get the INPUT type (text,submit,checkBox,etc')
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLTd searchRow = Browser.This.CurrentPage.FindByName("FORM", "f")["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 2] as HTMLTd;
		///    foreach (UIControl element in searchRow.Children)
		///    {
		///		   if (element.UIType == "HTMLInput")
		///		   {
		///			   switch (((HTMLInput)element).HtmlInputType)
		///			   {
		///				   case "text":
		///					   MessageBox.Show(element.Name + " is text");
		///						   break;
		///				   case "submit":
		///					   MessageBox.Show(element.Name + " is submit button");
		///					   break;
		///			   }
		///		   }
		///    }
		/// </code>
		/// </example>
		/// <returns>
		/// Specifies the type of the control
		/// button,checkbox,file,hidden,image,password,
		/// radio,reset,submit,text
		/// </returns>
		[Category("HTMLInput")]
		public string HtmlInputType
		{
			get { return ((IHTMLInputElement)htmlElement).type; }
		}

		/// <summary>
		/// Specifies that an input element should be preselected when the page loads (for type="checkbox" or type="radio")
		/// </summary>
		/// <example>
		/// <code>
		///   //Run on Google search preferences page (http://www.google.com/preferences?hl=en)
		///    HTMLInput searchAnyLangRB   = Browser.This.CurrentPage.FindByID("INPUT", "alc") as HTMLInput;
		///    HTMLInput searchLocalLangRB = Browser.This.CurrentPage.FindByID("INPUT", "slc") as HTMLInput;
		///    //by defualt search in any, change it
		///    if (searchAnyLangRB.Checked)
		///		  searchLocalLangRB.Click();
		/// </code>
		/// </example>
		/// <returns>true if the control is checked, otherwise, return false</returns>
		[Category("HTMLInput")]
		public bool Checked
		{
			get
			{
					return ((IHTMLInputElement)htmlElement).@checked;
			}
			set
			{
				((IHTMLInputElement)htmlElement).@checked = true;
			}
		}
		/// <summary>
		/// Retrieve disabled /enabled state of the INPUT control
		/// </summary>
		/// <example>
		/// HTMLInput searchButton = Browser.This.CurrentPage.FindByName("INPUT", "btnG") as HTMLInput;
		///    if (searchButton.Enabled)
		///    {
		///			searchButton.Click();
		///    }
		/// </example>
		/// <retruns> true if the control is enabled, false if disabled</retruns>
		[Category("HTMLInput")]
		public override bool Enabled
		{
			get
			{
				bool res = ((IHTMLInputElement)htmlElement).disabled;

				if (res)
					return false;

				return true;
			}
		}
		#endregion
	}
}
