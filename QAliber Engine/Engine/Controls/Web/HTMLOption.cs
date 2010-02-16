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
	/// Represent the OPTION html tag.
	/// This is an option in select list.
	/// </summary>
	[DisplayName("Select Option")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLOption : WebControl
	{
		public HTMLOption(IHTMLElement element) : base(element) 
		{

		}

		#region props
		/// <summary>
		/// Retrieve the text which identify the option
		/// </summary>
		/// <example>
		/// This sample run on google preferences page (http://www.google.com/preferences?hl=en)
		/// <code>
		///    //On google preferences page:
		///   HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///   HTMLOption op = intfaceLang.Options[3];
		///   string opName = op.Text;
		///   bool isOpSelected = op.IsSelected;
		/// </code>
		/// </example>
		public string Text
		{
			get
			{
				return ((IHTMLOptionElement)htmlElement).text;
			}
			set
			{
				((IHTMLOptionElement)htmlElement).text = value;
			}
		}
		/// <summary>
		/// Verify if this OPTION is selected in the hosted list
		/// </summary>
		/// <example>
		/// This sample run on google preferences page (http://www.google.com/preferences?hl=en)
		/// <code>
		///    //On google preferences page:
		///   HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///   HTMLOption op = intfaceLang.Options[3];
		///   string opName = op.Text;
		///   bool isOpSelected = op.IsSelected;
		/// </code>
		/// </example>
		public bool IsSelected
		{
			get
			{
				return ((IHTMLOptionElement)htmlElement).selected;
			}
			set
			{
				((IHTMLOptionElement)htmlElement).selected = value;
			}
		}



		#endregion
	}
}
