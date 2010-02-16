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
	/// Represents HTML FORM element. Forms used to hold Input controls and send data to server.
	/// </summary>
	public class HTMLForm : WebControl
	{
		public HTMLForm(IHTMLElement element)
			: base(element)
		{
		   
		}

		/// <summary>
		/// Submit this form. Submit is use to pass data to the server.
		/// </summary>
		/// <example>
		/// In the code below,lets search google by submiting the from instead of clicking Google Search button
		/// <code>
		///    HTMLInput searchBox = Browser.This.CurrentPage.FindByName("INPUT", "q") as HTMLInput;
		///    searchBox.Write("find");
		///    HTMLForm searchForm = Browser.This.CurrentPage.FindByName("FORM", "f") as HTMLForm;
		///    searchForm.Submit();
		/// </code>
		/// </example>
		public void Submit()
		{
			((IHTMLFormElement)htmlElement).submit();
		}

		#region properties
		/// <summary>
		/// Retrieve the form name.
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLForm searchForm = Browser.This.CurrentPage.FindByName("FORM", "f") as HTMLForm;
		///  string formName = searchForm.FormName;
		/// </code>
		/// </example>
		[Category("HTMLForm")]
		public string FormName
		{
			get
			{
				return ((IHTMLFormElement)htmlElement).name;
			}
		}

		#endregion

	}
}
